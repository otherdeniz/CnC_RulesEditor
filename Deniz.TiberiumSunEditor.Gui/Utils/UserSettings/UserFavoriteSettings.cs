namespace Deniz.TiberiumSunEditor.Gui.Utils.UserSettings
{
    public class UserFavoriteSettings
    {
        private readonly List<string> _favoriteList;
        private HashSet<string>? _favoritesCache;

        public UserFavoriteSettings(List<string> favoriteList)
        {
            _favoriteList = favoriteList;
        }

        public bool IsFavorite(string entry)
        {
            if (_favoritesCache == null)
            {
                _favoritesCache = new HashSet<string>();
                foreach (var s in _favoriteList)
                {
                    if (!_favoritesCache.Contains(s))
                    {
                        _favoritesCache.Add(s);
                    }
                }
            }
            return _favoritesCache.Contains(entry);
        }

        public void SetFavorite(string entry, bool addFavorite)
        {
            if (addFavorite)
            {
                if (!IsFavorite(entry))
                {
                    _favoriteList.Add(entry);
                    _favoritesCache = null;
                }
            }
            else
            {
                if (IsFavorite(entry))
                {
                    _favoriteList.Remove(entry);
                    _favoritesCache = null;
                }
            }
        }
    }
}
