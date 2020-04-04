using System;
using UnityEngine;


namespace ARPGFrame
{
    public sealed class PlayerUnit : BaseUnit, IDamageable
    {
        public event Action OnDieChange = delegate { };
        public event Action<float> OnHpChange = delegate { };

        #region Fields

        private bool _isDead;

        #endregion


        #region UnityMethods

        private void OnEnable()
        {
            var body = GetComponentInChildren<PlayerBody>();
            if (body != null) body.OnApplyDamageChange += SetDamage;

            var head = GetComponentInChildren<PlayerHead>();
            if (head != null) head.OnApplyDamageChange += SetDamage;
        }

        private void OnDisable()
        {
            var body = GetComponentInChildren<PlayerBody>();
            if (body != null) body.OnApplyDamageChange -= SetDamage;

            var head = GetComponentInChildren<PlayerHead>();
            if (head != null) head.OnApplyDamageChange -= SetDamage;
        }

        #endregion

        #region Methods

        public override void SetTeam(TeamColor teamColor)
        {
            _teamColor = teamColor;
        }

        #endregion

        #region IDamageable

        public void SetDamage(InfoCollision info)
        {
            if (!_isDead)
            {
                _currentHP -= info.Damage;
                OnHpChange.Invoke(_currentHP);
                if (_currentHP <= 0)
                {
                    OnDieChange.Invoke();
                    _isDead = true;
                }
            }
        }

        #endregion
    }
}
