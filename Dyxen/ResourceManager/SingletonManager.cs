namespace ResourceManager
{
    public static class SingletonManager
    {
        private static Dictionary<Type, object> _resources = new();
        public static T? Get<T>()
        {
            if(_resources.ContainsKey(typeof(T)))
                return (T)_resources[typeof(T)];
            return default;
        }
        public static bool Add(object obj)
        {
            Type t = obj.GetType();
            if (_resources.ContainsKey(t))
                return false;
            _resources.Add(t, obj);
            return true;
        }
        public static void AddOrReplace(object obj)
        {
            Type t = obj.GetType();
            if (_resources.ContainsKey(t))
            {
                _resources[t] = obj;
                return;
            }
            _resources.Add(t, obj);
        }
    }
}
