using UnityEngine;


namespace Geekbrains
{
    public sealed class FlashLightController : BaseController, IInitialization, IExecute
    {
        #region Fields

        private FlashLightModel _flashLightModel;
        private FlashLightUi _flashLightUi;

        #endregion


        #region Methods

        public override void On()
        {
            if(IsActive) return;
            if (_flashLightModel.BatteryChargeCurrent <= 0) return;
            base.On();
            _flashLightModel.Switch(FlashLightActiveType.On);
        }

        public override void Off()
        {
            if (!IsActive) return;
            base.Off();
            _flashLightModel.Switch(FlashLightActiveType.Off);
        }

        #endregion


        #region IInitialization

        public void Initialization()
        {
            _flashLightModel = Object.FindObjectOfType<FlashLightModel>();
            _flashLightUi = Object.FindObjectOfType<FlashLightUi>();
            _flashLightUi.MaxCharge = _flashLightModel.BatteryChargeMax;
        }

        #endregion


        #region IExecute

        public void Execute()
        {
            if (_flashLightModel.EditBatteryCharge(IsActive))
            {
                _flashLightModel.Rotation();
            }
            else
            {
                Off();
            }
            _flashLightUi.SetChargeBar(_flashLightModel.BatteryChargeCurrent);
        }

        #endregion
    }
}
