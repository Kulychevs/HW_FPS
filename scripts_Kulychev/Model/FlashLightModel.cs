using System;
using UnityEngine;


namespace Geekbrains
{
    public sealed class FlashLightModel : BaseObjectScene
    {
        #region Fields

        [SerializeField] private float _speed = 11;
        [SerializeField] private float _batteryChargeMax;
        [SerializeField] private float _batteryChargingSpeed = 0.5f;

        private Light _light;
        private Transform _goFollow;
        private Vector3 _vecOffset;

        #endregion


        #region Properties

        public float BatteryChargeCurrent { get; private set; }

        public float BatteryChargeMax { get => _batteryChargeMax; }

        #endregion


        #region UnityMethods

        protected override void Awake()
        {
            base.Awake();
            _light = GetComponent<Light>();
            _goFollow = Camera.main.transform;
            _vecOffset = Transform.position - _goFollow.position;
            BatteryChargeCurrent = _batteryChargeMax;
        }

        #endregion


        #region Methods

        public void Switch(FlashLightActiveType value)
        {
            switch (value)
            {
                case FlashLightActiveType.On:
                    _light.enabled = true;
                    Transform.position = _goFollow.position + _vecOffset;
                    Transform.rotation = _goFollow.rotation;
                    break;
                case FlashLightActiveType.Off:
                    _light.enabled = false;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }
        }

        public void Rotation()
        {
            Transform.position = _goFollow.position + _vecOffset;
            Transform.rotation = Quaternion.Lerp(Transform.rotation,
                _goFollow.rotation, _speed * Time.deltaTime);
        }

        public bool EditBatteryCharge(bool isFlashLigthActive)
        {
            if (isFlashLigthActive && BatteryChargeCurrent > 0)
            {
                BatteryChargeCurrent -= Time.deltaTime;
                return true;
            }
            else if(!isFlashLigthActive && BatteryChargeCurrent < _batteryChargeMax)
            {
                BatteryChargeCurrent += _batteryChargingSpeed * Time.deltaTime;
                if (BatteryChargeCurrent > _batteryChargeMax)
                    BatteryChargeCurrent = _batteryChargeMax;
            }
            return false;
        }

        #endregion
    }
}
