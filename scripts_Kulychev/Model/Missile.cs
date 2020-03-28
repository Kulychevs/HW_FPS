using UnityEngine;


namespace Geekbrains
{
    class Missile : Ammunition
    {
        #region Fields

        [SerializeField] private float _pushPower = 2;
        [SerializeField] private float _range = 10;
        [SerializeField] private LayerMask _mask;

        private Vector3 _pushDirection;
        private float _force;

        #endregion


        #region UnityMethods

        private void OnCollisionEnter(Collision collision)
        {
            Explosion();
            DestroyAmmunition();
        }

        #endregion


        #region Methods

        private void Explosion()
        {
            var colliders = Physics.OverlapSphere(transform.position, _range, _mask);
            foreach (var collider in colliders)
            {
                var setDamage = collider.gameObject.GetComponent<ICollision>();

                if (setDamage != null)
                {
                    CalcExplosionInfo(collider.gameObject);
                    setDamage.CollisionEnter(new InfoCollision(_curDamage, Type, _pushDirection));
                }              
            }
        }

        private void CalcExplosionInfo(GameObject gameObject)
        {
            var vector = gameObject.transform.position - transform.position;
            _force = _range - vector.magnitude;
            _pushDirection = vector.normalized * _force * _pushPower;

            _curDamage *= _force;
        }

        #endregion
    }
}
