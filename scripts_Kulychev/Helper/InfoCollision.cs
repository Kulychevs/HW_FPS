using UnityEngine;

namespace ARPGFrame
{
    public readonly struct InfoCollision
    {
        #region Fields

        private readonly Vector3 _dir;
        private readonly AmmunitionType _ammunition;
        private readonly float _damage;

        #endregion


        #region ClassLifeCycles

        public InfoCollision(float damage, AmmunitionType ammunition, Vector3 dir = default)
        {            
            _dir = dir;
            _ammunition = ammunition;
            _damage = damage;
        }

        #endregion


        #region Properties

        public Vector3 Dir => _dir;

        public float Damage => _damage;

        public AmmunitionType Ammunition => _ammunition;

        #endregion
    }
}
