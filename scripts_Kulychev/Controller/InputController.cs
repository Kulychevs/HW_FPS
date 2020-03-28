using System;
using UnityEngine;

namespace Geekbrains
{
    public sealed class InputController : BaseController, IExecute
    {
        public event Action Act = delegate { };

        #region Fields

        private const int WEAPON_INDEX_INCREMENT = 1;
        private const int WEAPON_INDEX_DECREMENT = -1;

        private KeyCode _activeFlashLight = KeyCode.F;
        private KeyCode _cancel = KeyCode.Escape;
        private KeyCode _reloadClip = KeyCode.R;
        private KeyCode _removeWeapon = KeyCode.G;
        private KeyCode _pickUpWeapon = KeyCode.E;
        private int _mouseButton = (int)MouseButton.LeftButton;

        #endregion

        #region Properties

        public bool IsWeapon { private get; set; }

        #endregion

        #region ClassLifeCycles

        public InputController()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        #endregion


        #region IExecute

        public void Execute()
        {
            if (!IsActive) return;
            if (Input.GetKeyDown(_activeFlashLight))
            {
                ServiceLocator.Resolve<FlashLightController>().Switch(ServiceLocator.Resolve<Inventory>().FlashLight);
            }

            if (Input.GetAxisRaw("Mouse ScrollWheel") > 0)
            {
                SelectWeapon(WEAPON_INDEX_INCREMENT);
            }
            else if (Input.GetAxisRaw("Mouse ScrollWheel") < 0)
            {
                SelectWeapon(WEAPON_INDEX_DECREMENT);
            }

            if (Input.GetMouseButton(_mouseButton))
            {
                if (ServiceLocator.Resolve<WeaponController>().IsActive)
                {
                    ServiceLocator.Resolve<WeaponController>().Fire();
                }
            }

            if (Input.GetKeyDown(_cancel))
            {
                ServiceLocator.Resolve<WeaponController>().Off();
                ServiceLocator.Resolve<FlashLightController>().Off();
            }

            if (Input.GetKeyDown(_reloadClip))
            {
                if (ServiceLocator.Resolve<WeaponController>().IsActive)
                {
                    ServiceLocator.Resolve<WeaponController>().ReloadClip();
                }
            }

            if (Input.GetKeyDown(_removeWeapon))
            {
                RemoveWeapon();
            }

            if (Input.GetKeyDown(_pickUpWeapon))
            {
                Act?.Invoke();
            }
        }

        #endregion


        #region Methods

        public void SelectWeapon(int weaponIndex)
        {
            ServiceLocator.Resolve<WeaponController>().Off();
            var currentWeaponIndex = ServiceLocator.Resolve<Inventory>().CurrentWeaponIndex;            
            var tempWeapon = ServiceLocator.Resolve<Inventory>().GetWeapon(currentWeaponIndex + weaponIndex);  
            
            if (tempWeapon != null)
            {
                ServiceLocator.Resolve<WeaponController>().On(tempWeapon);
            }
        }

        private void RemoveWeapon()
        {
            ServiceLocator.Resolve<WeaponController>().Off();
            ServiceLocator.Resolve<Inventory>().RemoveWeapon();
        }

        #endregion
    }
}
