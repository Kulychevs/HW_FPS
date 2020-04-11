using UnityEngine;


namespace ARPGFrame
{
    class MiniMapController : BaseController, IInitialization, IExecuteLate
    {
        #region Fields

        private const int UNIT_LAYER = 9;

        private MiniMap _miniMap;
        private Radar _radar;
        private Transform _player;               
        private TeamColor _playerTeamColor;
        private LayerMask _mask;
        private float _radarRadius = 10;

        #endregion


        #region Methods

        private void DetectRadarObjects()
        {
            var units = Physics.OverlapSphere(_player.position, _radarRadius, _mask);
            foreach (var unit in units)
            {
                if (unit.TryGetComponent<BaseEnemy>(out var enemy))
                {
                    if (enemy.StateBot != StateBot.Inspection)
                    {
                        var isAlly = _playerTeamColor == enemy.GetTeam();                        
                        _radar.AddObject(enemy, isAlly);
                    }
                }
            }
        }

        #endregion


        #region IExecuteLate

        public void ExecuteLate()
        {
            if (Time.frameCount % 2 == 0)
            {
                _miniMap.Tick(_player);
                if (Time.frameCount % 60 == 0)
                    _radar.RemoveObjects();
                DetectRadarObjects();
                _radar.DrawRadarDots(_player);
            }
        }

        #endregion


        #region IInitialization

        public void Initialization()
        {
            _mask = 1 << UNIT_LAYER;
            var player = Object.FindObjectOfType<PlayerUnit>();
            _player = player.transform;
            _playerTeamColor = player.GetTeam();
            _miniMap = Object.FindObjectOfType<MiniMap>();
            _radar = Object.FindObjectOfType<Radar>();

            _miniMap.Initialization();
            _radar.Initialization();
        }

        #endregion
    }
}
