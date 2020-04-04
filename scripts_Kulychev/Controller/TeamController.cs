using System.Collections.Generic;


namespace ARPGFrame
{
    public sealed class TeamController
    {
        #region Fields

        private Dictionary<TeamColor, HashSet<BaseUnit>> _teams;

        #endregion


        #region ClassLifeCycles

        public TeamController()
        {
            _teams = new Dictionary<TeamColor, HashSet<BaseUnit>>();
        }

        #endregion


        #region Methods

        public void AddToTeam(BaseUnit unit)
        {
            if (!_teams.ContainsKey(unit.GetTeam()))
            {
                HashSet<BaseUnit> hashSet = new HashSet<BaseUnit>();
                _teams.Add(unit.GetTeam(), hashSet);
            }
            _teams[unit.GetTeam()].Add(unit);
            ShowInfo();
        }

        public void RemoveFromTeam(BaseUnit unit)
        {
            _teams[unit.GetTeam()].Remove(unit);
            ShowInfo();
        }

        public void ShowInfo()
        {
            string info = null;

            foreach(var team in _teams)
            {
                switch (team.Key)
                {
                    case TeamColor.None:
                        info += "Free: ";
                        break;
                    case TeamColor.Blue:
                        info += "Blue: ";
                        break;
                    case TeamColor.Red:
                        info += "Red: ";
                        break;
                    default:
                        info += "Free: ";
                        break;
                }
                info += $"{team.Value.Count} \n";
            }
            UiInterface.TeamUiText.ShowData(info);
        }

        #endregion
    }
}
