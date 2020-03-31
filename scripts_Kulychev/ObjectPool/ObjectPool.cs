using System.Collections.Concurrent;


namespace Geekbrains
{
    public class ObjectPool
    {
        #region Fields

        private readonly ConcurrentBag<Ammunition> _container = new ConcurrentBag<Ammunition>();
        private readonly IPoolObjectCreator _objectCreator;
        private readonly AmmunitionType _type;

        #endregion


        #region Properties

        public int Count => _container.Count;

        #endregion


        #region ClassLifeCycles

        public ObjectPool(IPoolObjectCreator creator, AmmunitionType type)
        {
            if (creator != null)
            {
                _objectCreator = creator;
                _type = type;
            }
        }

        #endregion


        #region Methods

        public Ammunition GetObject()
        {
            Ammunition obj;
            if (_container.TryTake(out obj))
            {
                return obj;
            }

            return _objectCreator.Create(_type);
        }

        public void PutObject(ref Ammunition obj)
        {
            obj.ResetState();
            _container.Add(obj);
        }

        #endregion
    }
}
