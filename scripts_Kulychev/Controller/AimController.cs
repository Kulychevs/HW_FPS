using UnityEngine;


namespace Geekbrains
{
    public sealed class AimController : BaseController, IInitialization, IExecute
    {
        #region Fields

        private AimModel _aimModel;
        private AimUi _aimUi;

        #endregion


        #region IInitialization

        public void Initialization()
        {
            _aimModel = Object.FindObjectOfType<AimModel>();
            _aimUi = Object.FindObjectOfType<AimUi>();
        }

        #endregion


        #region IExecute

        public void Execute()
        {
            _aimModel.TakeAim();
            _aimUi.SetColor(_aimModel.AimColor);
        }

        #endregion
    }
}
