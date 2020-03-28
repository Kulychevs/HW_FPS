using System;
using UnityEngine;

namespace Geekbrains
{
    public sealed class SelectionController : BaseController, IExecute
    {
        #region Fields

        private GameObject _dedicatedObj;
        private readonly Camera _mainCamera;
        private ISelectObj _selectedObj;
        private Vector3 _weaponPosition;
        private readonly Vector2 _center;
        private readonly float _dedicateDistance = 20.0f;                
        private bool _nullString;
        private bool _isSelectedObj;
        
        private const float WEAPON_POSITION_X = 0.25f;
        private const float WEAPON_POSITION_Y = -0.36f;
        private const float WEAPON_POSITION_Z = 0.42f;

        #endregion


        #region ClassLifeCycles

        public SelectionController()
        {
            _mainCamera = Camera.main;
            _center = new Vector2(Screen.width / 2.0f, Screen.height / 2.0f);

            ServiceLocator.Resolve<InputController>().Act += UseSelectedObject;

            _weaponPosition.x = WEAPON_POSITION_X;
            _weaponPosition.y = WEAPON_POSITION_Y;
            _weaponPosition.z = WEAPON_POSITION_Z;
        }

        #endregion


        #region Methods

        private void SelectObject(GameObject obj)
        {
            if (obj == _dedicatedObj) return;
            _selectedObj = obj.GetComponent<ISelectObj>();
            if (_selectedObj != null)
            {
                UiInterface.SelectionObjMessageUi.Text = _selectedObj.GetMessage();
                _isSelectedObj = true;
            }
            else
            {
                UiInterface.SelectionObjMessageUi.Text = String.Empty;
                _isSelectedObj = false;
            }
            _dedicatedObj = obj;
        }

        private void UseSelectedObject()
        {
            if (_isSelectedObj)
            {
                switch (_selectedObj)
                {
                    case Weapon aim:
                        PickUpWeapon(aim);
                        break;
                    default:
                        break;
                }
            }
        }

        private void PickUpWeapon(Weapon weapon)
        {
            weapon.GetComponent<Collider>().enabled = false;
            weapon.transform.SetParent(_mainCamera.transform);
            weapon.transform.rotation = _mainCamera.transform.rotation;
            weapon.transform.localPosition = _weaponPosition;

            ServiceLocator.Resolve<Inventory>().AddWeapon(weapon);
            ServiceLocator.Resolve<InputController>().SelectWeapon(0);
        }

        #endregion


        #region IExecute

        public void Execute()
        {
            if (!IsActive) return;
            if (Physics.Raycast(_mainCamera.ScreenPointToRay(_center),
                out var hit, _dedicateDistance))
            {
                SelectObject(hit.collider.gameObject);
                _nullString = false;
            }
            else if (!_nullString)
            {
                UiInterface.SelectionObjMessageUi.Text = String.Empty;
                _nullString = true;
                _dedicatedObj = null;
                _isSelectedObj = false;
            }
        }

        #endregion
    }
}
