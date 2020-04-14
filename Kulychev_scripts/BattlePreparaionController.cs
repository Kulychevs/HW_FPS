using System;
using UnityEngine;


namespace ARPGFrame
{
    public sealed class BattlePreparaionController : BaseController, IExecute
    {
        public event Action BattleStart = delegate { };

        #region Fields

        public TeamColor PlayerTeamColor;


        private const string HOME_TAG = "Home";
        private const string BOT_PREFAB_PATH = "Prefabs/Infantryman";

        private Camera _camera;
        private BaseEnemy _bot;
        private int _botCounter;
        private int _botMaxNumber;

        #endregion


        #region ClassLifeCyrcles

        public BattlePreparaionController()
        {
            PlayerTeamColor = TeamColor.Blue;
            _camera = UnityEngine.Object.FindObjectOfType<Reference>().PreparationCamera;
            _bot = Resources.Load<BaseEnemy>(BOT_PREFAB_PATH);
            _botCounter = 0;
            _botMaxNumber = 5;           
        }

        #endregion


        #region Methods

        public void SetBotMaxNumber(int number)
        {
            _botMaxNumber = number;
        }

        #endregion


        #region IExecute

        public void Execute()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out var hit))
                {
                    if (hit.transform.CompareTag(HOME_TAG))
                    {
                        var unit = UnityEngine.Object.Instantiate(_bot, hit.point, Quaternion.identity);
                        unit.SetTeam(PlayerTeamColor);
                        ServiceLocator.Resolve<BotController>().AddBotToList(unit);
                        _botCounter++;
                    }
                }
            }

            if (_botCounter == _botMaxNumber)
            {
                BattleStart();
                _camera.enabled = false;
            }           
        }

        #endregion
    }
}
