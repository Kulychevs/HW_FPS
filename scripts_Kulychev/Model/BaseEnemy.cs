using System;
using UnityEngine;

namespace Geekbrains
{
    public abstract class BaseEnemy : BaseObjectScene, ICollision, ISelectObj
    {
        public event Action OnPointChange = delegate { };

        #region Fields

        [SerializeField] protected float _timeToDestroy = 10.0f;
        [SerializeField] protected float Hp = 150;

        private bool _isDead;
        private float _dmg;
        private Vector3 _pushDir;

        #endregion


        #region Methods

        private void Push(Vector3 dir)
        {
            Rigidbody.AddForce(dir, ForceMode.Impulse);
        }

        private void Hurt(float damage)
        {
            if (_isDead) return;
            if (Hp > 0)
            {
                Hp -= damage;
            }

            if (Hp <= 0)
            {
                Destroy(gameObject, _timeToDestroy);

                OnPointChange.Invoke();
                _isDead = true;
            }
        }

        protected abstract void CalcDmgAndPushDir(InfoCollision info, out float dmg, out Vector3 push);

        #endregion


        #region ICollision

        public void CollisionEnter(InfoCollision info)
        {
            CalcDmgAndPushDir(info, out _dmg, out _pushDir);
            Push(_pushDir);
            Hurt(_dmg);
        }

        #endregion


        #region ISelectObj

        public string GetMessage()
        {
            return gameObject.name;
        }

        #endregion
    }
}
