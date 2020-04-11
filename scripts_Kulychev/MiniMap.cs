using UnityEngine;


namespace ARPGFrame
{
    public class MiniMap : MonoBehaviour
    {
        #region Fields

        private float _positionY;

        #endregion


        #region Methods

        public void Initialization()
        {
            var camera = GetComponent<Camera>();
            var renderTextur = Resources.Load<RenderTexture>("Minimap");

            camera.targetTexture = renderTextur;
            _positionY = camera.transform.position.y;        
        }

        public void Tick(Transform player)
        {
            var newPosition = player.position;
            newPosition.y = _positionY;
            transform.position = newPosition;
            transform.rotation = Quaternion.Euler(90, player.eulerAngles.y, 0);
        }

        #endregion
    }
}
