using UnityEngine;


namespace Geekbrains
{
    public sealed class ArmoredInfantryman : BaseEnemy
    {
        #region Fields

        private const float PUSH_REDUCTOIN_RPG = 10;
        private const float PUSH_REDUCTOIN_BULLET = 100;
        private const float BULLET_DAMAGE = 0;

        #endregion


        #region BaseEnemy

        protected override void CalcDmgAndPushDir(InfoCollision info, out float dmg, out Vector3 push)
        {
            if (info.Ammunition == AmmunitionType.Rpg)
            {
                push = info.Dir / PUSH_REDUCTOIN_RPG;
                dmg = info.Damage;
            }
            else
            {
                push = info.Dir / PUSH_REDUCTOIN_BULLET;
                dmg = BULLET_DAMAGE;
            }
        }

        #endregion
    }
}
