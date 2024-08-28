using Deniz.TiberiumSunEditor.Gui.Utils.Exceptions;

namespace Deniz.TiberiumSunEditor.Gui.Utils
{
    public class AnimationsAsyncLoader
    {
        private static AnimationsAsyncLoader? _instance;
        public static AnimationsAsyncLoader Instance => _instance ??= new AnimationsAsyncLoader();

        private Task? _loadTask;
        private SynchronizationContext _uiSyncContext = null!;
        private bool _isRunning;
        private readonly Queue<LoadingQueueItem> _loadingQueue = new();

        public void InitialiseUiSyncContext()
        {
            _uiSyncContext = SynchronizationContext.Current 
                             ?? throw new RuntimeException("Fatal: no Current SynchronizationContext");
        }

        public void Stop(bool wait, bool clearQueue)
        {
            _isRunning = false;
            if (wait)
            {
                while (_loadTask?.Status == TaskStatus.Running)
                {
                    Thread.Sleep(50);
                }
            }
            if (clearQueue)
            {
                _loadingQueue.Clear();
            }
        }

        public void Start()
        {
            Stop(true, false);
            _isRunning = true;
            if (_loadingQueue.Any())
            {
                _loadTask = Task.Run(LoadTaskEntry);
            }
        }

        public AnimationRequirementToken LoadAnimation(string keys, Action<Image> afterLoad, float opacity = 1)
        {
            var requirementToken = new AnimationRequirementToken();
            _loadingQueue.Enqueue(new LoadingQueueItem(keys, afterLoad, requirementToken, opacity));
            if (_loadTask?.Status != TaskStatus.Running 
                && _isRunning)
            {
                _loadTask = Task.Run(LoadTaskEntry);
            }
            return requirementToken;
        }

        private void LoadTaskEntry()
        {
            while (_loadingQueue.TryDequeue(out var loadAnimation))
            {
                if (loadAnimation.Requirement.StillNeeded)
                {
                    var keys = loadAnimation.Keys.Split(",").Distinct();
                    var animationImage = CCGameRepository.Instance.GetAnimationsImage(string.Join(",", keys), opacity:loadAnimation.Opacity);
                    if (animationImage != null)
                    {
                        _uiSyncContext.Post(img =>
                        {
                            try
                            {
                                loadAnimation.AfterLoad((Image)img!);
                            }
                            catch (Exception)
                            {
                                // ignore this error
                            }
                        }, animationImage);
                        loadAnimation.Requirement.Delivered = true;
                    }
                }
                if (!_isRunning)
                {
                    break;
                }
            }
            _loadTask = null;
        }

        private class LoadingQueueItem
        {
            public LoadingQueueItem(string keys, 
                Action<Image> afterLoad, 
                AnimationRequirementToken requirement,
                float opacity)
            {
                Keys = keys;
                AfterLoad = afterLoad;
                Requirement = requirement;
                Opacity = opacity;
            }
            public string Keys { get; }
            public Action<Image> AfterLoad { get; }
            public AnimationRequirementToken Requirement { get; }
            public float Opacity { get; }
        }
    }

    public class AnimationRequirementToken
    {
        public bool StillNeeded { get; set; } = true;

        public bool Delivered { get; set; }
    }
}
