using UnityEngine;


namespace Geekbrains
{
	public class AmmunitionCreator : MonoBehaviour, IPoolObjectCreator
	{
        #region Fields

        [SerializeField] private Ammunition _bullet;
		[SerializeField] private Ammunition _missile;

		#endregion


		#region IPoolObjectCreator

		public Ammunition Create(AmmunitionType type)
		{
			Ammunition ammunition;
			switch (type)
			{
				case AmmunitionType.Rpg:
					ammunition = Instantiate(_missile);
					break;
				case AmmunitionType.Bullet:
					ammunition = Instantiate(_bullet);
					break;
				default:
					ammunition = null;
					break;
			}
			return ammunition;
		}

        #endregion
    }
}
