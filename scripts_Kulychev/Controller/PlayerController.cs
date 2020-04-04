using UnityEngine;


namespace ARPGFrame
{
	public sealed class PlayerController : BaseController, IInitialization ,IExecute
	{
        #region Fields

        public TeamColor TeamColor = TeamColor.Blue;

		private readonly IMotor _motor;
		private PlayerUnit _playerUnit;

        #endregion


        #region ClassLifeCycles

        public PlayerController(IMotor motor)
		{
			_motor = motor;
		}

        #endregion


        #region Methods

        private void ShowHP(float playerCurrentHP)
		{
			UiInterface.PlayerUiText.ShowData(playerCurrentHP);
		}

		private void PlayerDie()
		{
			Off();
			ServiceLocator.Resolve<TeamController>().RemoveFromTeam(_playerUnit);
		}

		#endregion


        #region IInitializatiaon

        public void Initialization()
		{
			_playerUnit = Object.FindObjectOfType<PlayerUnit>();
			_playerUnit.OnDieChange += PlayerDie;
			_playerUnit.OnHpChange += ShowHP;

			UiInterface.PlayerUiText.PlayerMaxHP = _playerUnit.GetMaxHP();
			ShowHP(_playerUnit.GetCurrentHP());

			_playerUnit.SetTeam(TeamColor);
			ServiceLocator.Resolve<TeamController>().AddToTeam(_playerUnit);
		}

		#endregion


		#region IExecute

		public void Execute()
		{
			if (!IsActive) { return; }
			_motor.Move();
		}

		#endregion
	}
}