using UnityEngine;

namespace ARPGFrame
{
	public static class UiInterface
	{
        #region Fields

        private static FlashLightUi _flashLightUiText;
		private static WeaponUiText _weaponUiText;
		private static AimUi _aimUi;
		private static SelectionObjMessageUi _selectionObjMessageUi;
		private static PlayerUiText _playerUiText;
		private static TeamUiText _teamUiText;

        #endregion


        #region Properties

        public static FlashLightUi LigthChargeImage
		{
			get
			{
				if (!_flashLightUiText)
					_flashLightUiText = Object.FindObjectOfType<FlashLightUi>();
				return _flashLightUiText;
			}
		}		

		public static WeaponUiText WeaponUiText
		{
			get
			{
				if (!_weaponUiText)
					_weaponUiText = Object.FindObjectOfType<WeaponUiText>();
				return _weaponUiText;
			}
		}		

		public static AimUi AimUi
		{
			get
			{
				if (!_aimUi)
					_aimUi = Object.FindObjectOfType<AimUi>();
				return _aimUi;
			}
		}		

		public static SelectionObjMessageUi SelectionObjMessageUi
		{
			get
			{
				if (!_selectionObjMessageUi)
					_selectionObjMessageUi = Object.FindObjectOfType<SelectionObjMessageUi>();
				return _selectionObjMessageUi;
			}
		}
	
		public static PlayerUiText PlayerUiText
		{
			get
			{
				if (!_playerUiText)
					_playerUiText = Object.FindObjectOfType<PlayerUiText>();
				return _playerUiText;
			}
		}		

		public static TeamUiText TeamUiText
		{
			get
			{
				if (!_teamUiText)
					_teamUiText = Object.FindObjectOfType<TeamUiText>();
				return _teamUiText;
			}
		}

        #endregion
    }
}