using UnityEngine;
using UnityEngine.UI;


namespace ARPGFrame
{
	public sealed class PlayerUiText : MonoBehaviour
	{

		private Text _text;

		public float PlayerMaxHP {private get; set; }

		private void Awake()
		{
			_text = GetComponent<Text>();
		}

		public void ShowData(float playerCurrentHP)
		{
			_text.text = $"{playerCurrentHP}/{PlayerMaxHP}";
		}

		public void SetActive(bool value)
		{
			_text.gameObject.SetActive(value);
		}
	}
}
