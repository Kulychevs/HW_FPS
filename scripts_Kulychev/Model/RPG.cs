namespace Geekbrains
{
    public sealed class RPG : Weapon
    {
        #region Weapon

        public override void Fire()
        {
            if (!_isReady) return;
            if (Clip.CountAmmunition <= 0) return;
            var temAmmunition = Instantiate(Ammunition, _barrel.position, _barrel.rotation);
            temAmmunition.AddForce(_barrel.forward * _force);
            Clip.CountAmmunition--;
        }

        #endregion
    }
}
