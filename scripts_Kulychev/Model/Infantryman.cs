using UnityEngine;


namespace Geekbrains
{
    public sealed class Infantryman : BaseEnemy
    {
        #region Fields

        private const float PUSH_REDUCTOIN = 10;

        #endregion


        #region BaseEnemy

        protected override void CalcDmgAndPushDir(InfoCollision info, out float dmg, out Vector3 push)
        {
            if (info.Ammunition == AmmunitionType.Rpg)
            {
                push = info.Dir;
            }
            else
            {
                push = info.Dir / PUSH_REDUCTOIN;
            }
            dmg = info.Damage;
        }

        #endregion
    }
}
