using UnityEngine;


namespace Geekbrains
{
    public abstract class Ammunition : BaseObjectScene, IPoolable
    {
        #region Fields

        [SerializeField] private float _timeToDestruct = 10;
        [SerializeField] private float _baseDamage = 10;

        [HideInInspector] public AmmunitionType Type = AmmunitionType.Bullet;
        [HideInInspector] public bool IsInPool;

        protected float _curDamage;
        private float _lossOfDamageAtTime = 0.2f;
        private ITimeRemaining _timeRemaining;

        #endregion


        #region UnityMethods

        protected override void Awake()
        {
            base.Awake();
            _curDamage = _baseDamage;
            CreatePool();
        }

        private void Start()
        {
            _timeRemaining = new TimeRemaining(LossOfDamage, 1.0f, true);
            _timeRemaining.AddTimeRemaining();           
        }

        #endregion


        #region Methods

        public void AddForce(Vector3 dir)
        {
            if (!Rigidbody) return;
            Rigidbody.AddForce(dir);
        }

        private void LossOfDamage()
        {
            _curDamage -= _lossOfDamageAtTime;
        }

        protected void DestroyAmmunition()
        {
            if (!IsInPool)
            {
                Ammunition am = gameObject.GetComponent<Ammunition>();
                _timeRemaining.RemoveTimeRemaining();
                PoolsLocator.Resolve(Type).PutObject(ref am);
            }
        }

        public void DestroyAmmunitionWithDelay()
        {
            Invoke("DestroyAmmunition", _timeToDestruct);
        }
        private void CreatePool()
        {
            var creator = FindObjectOfType<AmmunitionCreator>();
            PoolsLocator.SetPool(new ObjectPool(creator, Type), Type);
        }

        #endregion


        #region IPoolable

        public void ResetState()
        {
            Rigidbody.velocity = Vector3.zero;
            SetActive(false);
            IsInPool = true;
        }

        #endregion
    }
}
