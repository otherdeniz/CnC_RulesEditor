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

        public Queue<(string Keys, Action<Image> AfterLoad, AnimationRequirementToken Requirement)> LoadingQueue { get; } = new();

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
                LoadingQueue.Clear();
            }
        }

        public void Start()
        {
            Stop(true, false);
            _isRunning = true;
            if (LoadingQueue.Any())
            {
                _loadTask = Task.Run(LoadTaskEntry);
            }
        }

        public AnimationRequirementToken LoadAnimation(string keys, Action<Image> afterLoad)
        {
            var requirementToken = new AnimationRequirementToken();
            LoadingQueue.Enqueue((keys, afterLoad, requirementToken));
            if (_loadTask?.Status != TaskStatus.Running 
                && _isRunning)
            {
                _loadTask = Task.Run(LoadTaskEntry);
            }
            return requirementToken;
        }

        private void LoadTaskEntry()
        {
            while (LoadingQueue.TryDequeue(out var loadAnimation))
            {
                if (loadAnimation.Requirement.StillNeeded)
                {
                    var keys = loadAnimation.Keys.Split(",").Distinct();
                    var animationImage = CCGameRepository.Instance.GetAnimationsImage(string.Join(",", keys));
                    if (animationImage != null)
                    {
                        _uiSyncContext.Send(img =>
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
    }

    public class AnimationRequirementToken
    {
        public bool StillNeeded { get; set; } = true;

        public bool Delivered { get; set; }
    }
}
