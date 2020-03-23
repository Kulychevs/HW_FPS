using UnityEngine;


namespace Geekbrains
{
    public sealed class AimModel : BaseObjectScene
    {
        #region Fields

        [SerializeField] private LayerMask _mask;
        [SerializeField] private string _hitTag;
        [SerializeField] private float _distance = 3;     

        private RaycastHit _raycastHit;

        #endregion


        #region Properties

        public AimColor AimColor { get; private set; }

        #endregion


        #region Methods

        public void TakeAim()
        {
            ChangeColor(Physics.Raycast(transform.position, transform.forward, out _raycastHit, _distance, _mask));
        }

        private void ChangeColor(bool isHit)
        {
            if(isHit && _raycastHit.transform.CompareTag(_hitTag))
            {
                AimColor = AimColor.Blue;
            }
            else
            {
                AimColor = AimColor.Red;
            }
        }

        #endregion
    }
}
