using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace ARPGFrame
{
    public sealed class BotController : BaseController, IInitialization, IExecute
    {

        #region Fields

        private readonly int _countBot = 10;
        private readonly HashSet<BaseEnemy> _botList  = new HashSet<BaseEnemy>();

        #endregion


        #region Methods

        private void AddBotToList(BaseEnemy bot)
        {
            if (!_botList.Contains(bot))
            {
                _botList.Add(bot);
                bot.OnDieChange += RemoveBotToList;
                ServiceLocator.Resolve<TeamController>().AddToTeam(bot);
            }
        }

        private void RemoveBotToList(BaseEnemy bot)
        {
            if (!_botList.Contains(bot))
            {
                return;
            }

            bot.OnDieChange -= RemoveBotToList;
            _botList.Remove(bot);
            ServiceLocator.Resolve<TeamController>().RemoveFromTeam(bot);
        }

        #endregion


        #region IInitialization

        public void Initialization()
        {
            for (var index = 0; index < _countBot; index++)
            {
                var tempBot = Object.Instantiate(ServiceLocatorMonoBehaviour.GetService<Reference>().Bot,
                    Patrol.GenericPoint(ServiceLocatorMonoBehaviour.GetService<CharacterController>().transform),
                    Quaternion.identity);

                tempBot.Agent.avoidancePriority = index;
                if (index % 2 == 0 || index == 0)
                    tempBot.SetTeam(TeamColor.Red);
                else
                    tempBot.SetTeam(TeamColor.Blue);
                AddBotToList(tempBot);
            }
        }

        #endregion


        #region IExecute

        public void Execute()
        {
            if (!IsActive)
            {
                return;
            }

            for (var i = 0; i < _botList.Count; i++)
            {
                var bot = _botList.ElementAt(i);
                bot.Tick();
            }
        }

        #endregion
    }
}
