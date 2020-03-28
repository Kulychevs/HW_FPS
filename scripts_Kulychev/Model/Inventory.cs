using UnityEngine;

namespace Geekbrains
{
	public sealed class Inventory : IInitialization
	{
        #region Fields

        private Weapon[] _weapons = new Weapon[5];

        #endregion


        #region Properties

        public FlashLightModel FlashLight { get; private set; }
		public int CurrentWeaponIndex { get; private set; }

		#endregion


		#region IInitialization

		public void Initialization()
		{
			_weapons.Initialize();
			CurrentWeaponIndex = 0;
			var weapons = ServiceLocatorMonoBehaviour.GetService<CharacterController>().
				GetComponentsInChildren<Weapon>();
			
			foreach (var weapon in weapons)
			{
				weapon.IsVisible = false;
			}

			for (int i = 0; i < weapons.Length; i++)
			{
				_weapons[i] = weapons[i];
			}			

			FlashLight = Object.FindObjectOfType<FlashLightModel>();
			FlashLight.Switch(FlashLightActiveType.Off);
		}

        #endregion


        #region Methods

        public Weapon GetWeapon(int i)
		{
			if (i >= _weapons.Length)
			{
				i -= _weapons.Length;
			}
			else if (i < 0)
			{
				i += _weapons.Length;
			}
			CurrentWeaponIndex = i;
			return _weapons[i];
		}
		
		public void AddWeapon(Weapon weapon)
		{
			RemoveWeapon();
			_weapons[CurrentWeaponIndex] = weapon;
		}

        public void RemoveWeapon()
        {
			if (_weapons[CurrentWeaponIndex] != null)
				Object.Destroy(_weapons[CurrentWeaponIndex].gameObject);
			_weapons[CurrentWeaponIndex] = null;
        }

        #endregion
    }
}