using UnityEngine;
using UnityEngine.UI;


namespace Geekbrains
{
    public sealed class FlashLightUi : MonoBehaviour
    {
        #region Fiealds

        private Image _image;

        #endregion


        #region Properties

        public float MaxCharge { private get; set; }

        #endregion


        #region UnityMethods

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        #endregion


        #region Methods

        public void SetChargeBar(float currentCharge)
        {
            _image.fillAmount = currentCharge / MaxCharge;
        }

        public void SetActive(bool value)
        {
            _image.gameObject.SetActive(value);
        }

        #endregion
    }
}
