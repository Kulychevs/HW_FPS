using System.Collections.Generic;
using UnityEngine;

namespace Geekbrains
{
	public abstract class Weapon : BaseObjectScene, ISelectObj
	{

		#region Fields

		[SerializeField] protected Transform _barrel;
		[SerializeField] protected int _maxCountAmmunition = 40;
		[SerializeField] protected int _minCountAmmunition = 20;
		[SerializeField] protected float _reloadDelay;		
		[SerializeField] protected float _force = 999;
		[SerializeField] protected float _rechergeTime = 0.2f;

		public AmmunitionType[] AmmunitionTypes = { AmmunitionType.Bullet };
		public Ammunition Ammunition;
		public Clip Clip;

		protected bool _isReady = true;
		protected ITimeRemaining _timeRemaining;

		private Queue<Clip> _clips = new Queue<Clip>();
		private int _countClip = 5;

		#endregion


		#region Properties

		public int CountClip => _clips.Count;
		public float ReloadDelay { get => _reloadDelay; }

        #endregion


        #region UnityMethods

        protected void Start()
		{
			var creator = FindObjectOfType<AmmunitionCreator>();
			PoolsLocator.SetPool(new ObjectPool(creator, AmmunitionTypes[0]), AmmunitionTypes[0]);

			_timeRemaining = new TimeRemaining(ReadyShoot, _rechergeTime);
			for (var i = 0; i <= _countClip; i++)
			{
				AddClip(new Clip { CountAmmunition = Random.Range(_minCountAmmunition, _maxCountAmmunition) });
			}
			
			ReloadClip();
		}

        #endregion


        #region Methods

        public virtual void Fire()
		{			
			SetAmmunition(0);
		}

		private void SetAmmunition(int index)
		{
			var temAmmunition = PoolsLocator.Resolve(AmmunitionTypes[index]).GetObject();
			temAmmunition.IsInPool = false;
			temAmmunition.transform.position = _barrel.position;
			temAmmunition.transform.rotation = _barrel.rotation;
			temAmmunition.SetActive(true);
			temAmmunition.AddForce(_barrel.forward * _force);
			temAmmunition.DestroyAmmunitionWithDelay();
		}

		protected void ReadyShoot()
		{
			_isReady = true;
		}

		protected void AddClip(Clip clip)
		{
			_clips.Enqueue(clip);
		}

		public void ReloadClip()
		{
			if (CountClip <= 0) return;
			Clip = _clips.Dequeue();
		}

        #endregion


        #region ISelectObj

        public string GetMessage()
		{
			return Name;
		}

        #endregion
    }
}