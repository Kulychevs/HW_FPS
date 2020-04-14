using UnityEngine;


namespace ARPGFrame
{
    public sealed class GameController : MonoBehaviour
    {
        #region Fields

        private Controllers _controllers;
        private BattlePreparaionController _preparaionController;
        private GameStatus _gameStatus;

        #endregion


        #region UnityMethods

        private void Start()
        {
            _gameStatus = GameStatus.Preparation;

            _controllers = new Controllers();

            _preparaionController = new BattlePreparaionController();
            _preparaionController.BattleStart += StartBattle;

            ServiceLocator.Resolve<TeamController>().TeamLose += EndGame;
        }

        private void Update()
        {
            if (_gameStatus == GameStatus.Battle)
            {
                for (var i = 0; i < _controllers.Length; i++)
                {
                    _controllers[i].Execute();
                }
            }
            else if (_gameStatus == GameStatus.Preparation)
            {
                _preparaionController.Execute();
            }
        }

        private void LateUpdate()
        {
            if (_gameStatus == GameStatus.Battle)
            {
                for (var i = 0; i < _controllers.ExecuteLateConrollers.Length; i++)
                {
                    _controllers.ExecuteLateConrollers[i].ExecuteLate();
                }
            }
        }

        #endregion


        #region Methods

        private void StartBattle()
        {
            _gameStatus = GameStatus.Battle;
            _controllers.Initialization();           
        }

        private void EndGame( TeamColor loseTeamColor)
        {
            _gameStatus = GameStatus.End;
            ServiceLocator.Resolve<EndGameController>().GameEnd(loseTeamColor);
        }

        #endregion
    }
}
