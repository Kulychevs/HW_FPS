using System.Collections.Generic;


namespace Geekbrains
{
    public static class PoolsLocator
    {
        #region Fields

        private static readonly Dictionary<AmmunitionType, ObjectPool> _poolsContainer =
            new Dictionary<AmmunitionType, ObjectPool>();

        #endregion


        #region Methods

        public static void SetPool(ObjectPool value, AmmunitionType t)
        {
            if (!_poolsContainer.ContainsKey(t))
            {
                _poolsContainer[t] = value;
            }
        }

        public static ObjectPool Resolve(AmmunitionType t)
        {
            return _poolsContainer[t];
        }

        #endregion
    }
}
