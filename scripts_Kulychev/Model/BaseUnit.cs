using System;
using UnityEngine;

namespace ARPGFrame
{
    public abstract class BaseUnit : BaseObjectScene
    {
        #region Fields

        [SerializeField] protected float _maxHP;
        protected float _currentHP;

        protected TeamColor _teamColor;

        #endregion


        #region UnityMethods

        protected override void Awake()
        {
            base.Awake();
            _currentHP = _maxHP;
        }

        #endregion


        #region Methods

        public float GetMaxHP()
        {
            return _maxHP;
        }

        public float GetCurrentHP()
        {
            return _currentHP;
        }

        public TeamColor GetTeam()
        {
            return _teamColor;
        }

        public abstract void SetTeam(TeamColor teamColor);

        #endregion
    }
}
