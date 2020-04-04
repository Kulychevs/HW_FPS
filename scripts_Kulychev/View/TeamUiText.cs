using UnityEngine;
using UnityEngine.UI;


namespace ARPGFrame
{
	public sealed class TeamUiText : MonoBehaviour
	{

		private Text _text;

		public float PlayerMaxHP { private get; set; }

		private void Awake()
		{
			_text = GetComponent<Text>();
		}

		public void ShowData(string teamInfo)
		{
			_text.text = teamInfo;
		}

		public void SetActive(bool value)
		{
			_text.gameObject.SetActive(value);
		}
	}
}
