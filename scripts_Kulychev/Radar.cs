using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


namespace ARPGFrame
{
	public class Radar : MonoBehaviour
	{
        #region Fields

        private float mapScale = 4;
		private Dictionary<BaseEnemy, Image> _radarObjects;

        #endregion


        #region Methods
        public void Initialization()
		{
			_radarObjects = new Dictionary<BaseEnemy, Image>();
		}

		public void AddObject(BaseEnemy unit, bool isAlly)
		{
			if (!_radarObjects.ContainsKey(unit))
			{
				Image image;
				if (isAlly)
					image = ImagePoolLocator.Resolve<Image>(ImageType.Ally).GetObject();
				else
					image = ImagePoolLocator.Resolve<Image>(ImageType.Enemy).GetObject();
				image.enabled = true;
				_radarObjects.Add(unit, image);
			}
		}

		public void RemoveObjects()
		{
			foreach (var radObject in _radarObjects)
			{
				radObject.Value.enabled = false;
				if (radObject.Value.color == Color.red)
					ImagePoolLocator.Resolve<Image>(ImageType.Enemy).PutObject(radObject.Value);
				else
					ImagePoolLocator.Resolve<Image>(ImageType.Ally).PutObject(radObject.Value);
			}
				_radarObjects.Clear();
		}

		public void DrawRadarDots(Transform player)
		{
			foreach (var radObject in _radarObjects)
			{
				if (radObject.Key)
				{
					Vector3 radarPos = (radObject.Key.transform.position - player.position);

					float distToObject = Vector3.Distance(player.position, radObject.Key.transform.position) * mapScale;

					float deltay = Mathf.Atan2(radarPos.x, radarPos.z) * Mathf.Rad2Deg - 270 - player.eulerAngles.y;

					radarPos.x = distToObject * Mathf.Cos(deltay * Mathf.Deg2Rad) * -1;
					radarPos.z = distToObject * Mathf.Sin(deltay * Mathf.Deg2Rad);

					radObject.Value.transform.SetParent(transform);

					radObject.Value.transform.position = new Vector3(radarPos.x, radarPos.z, 0) + transform.position;
				}
			}
		}

        #endregion
    }
}
