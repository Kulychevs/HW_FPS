using UnityEngine;
using UnityEngine.UI;


namespace Geekbrains
{
    public sealed class AimUi : MonoBehaviour
    {
        #region Fields

        private Image _image;

        #endregion


        #region UnityMethods

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        #endregion


        #region Methods

        public void SetColor(AimColor aimColor)
        {
            if(aimColor == AimColor.Blue)
            {
                _image.color = Color.blue;
            }
            else
            {
                _image.color = Color.red;
            }
        }

        #endregion
    }
}