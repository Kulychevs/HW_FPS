using System;
using UnityEngine;
using UnityEngine.AI;

namespace ARPGFrame
{
    public abstract class BaseEnemy : BaseUnit, ISelectObj, IDamageable
    {
        public event Action<BaseEnemy> OnDieChange = delegate { };

        #region Fields       

        [HideInInspector] public Transform Target;

        [SerializeField] protected float _timeToDestroy = 10.0f;
        [SerializeField] protected Vision Vision;
        [SerializeField] protected Weapon Weapon;        

        private bool _isDead;
        private float _damage;
        private float _waitTime = 3;
        private float _stoppingDistance = 2.0f;
        private float _reSeekTime = 0;
        private Vector3 _pushDir;
        private Vector3 _point;
        private StateBot _stateBot;
        private ITimeRemaining _timeRemaining;
        private Transform _target = null;

        #endregion


        #region Properties
        
        public NavMeshAgent Agent { get; private set; }

        private StateBot StateBot
        {
            get => _stateBot;
            set
            {
                _stateBot = value;              
            }
        }

        #endregion


        #region UnityMethods

        protected override void Awake()
        {
            base.Awake();
            Agent = GetComponent<NavMeshAgent>();
            _timeRemaining = new TimeRemaining(ResetStateBot, _waitTime);
        }

        private void OnEnable()
        {
            var bodyBot = GetComponentInChildren<BodyBot>();
            if (bodyBot != null) bodyBot.OnApplyDamageChange += SetDamage;

            var headBot = GetComponentInChildren<HeadBot>();
            if (headBot != null) headBot.OnApplyDamageChange += SetDamage;
        }

        private void OnDisable()
        {
            var bodyBot = GetComponentInChildren<BodyBot>();
            if (bodyBot != null) bodyBot.OnApplyDamageChange -= SetDamage;

            var headBot = GetComponentInChildren<HeadBot>();
            if (headBot != null) headBot.OnApplyDamageChange -= SetDamage;
        }

        #endregion


        #region Methods

        public void MovePoint(Vector3 point)
        {
            Agent.SetDestination(point);
        }

        private void Push(Vector3 dir)
        {
            foreach(var rigidbody in transform.GetComponentsInChildren<Rigidbody>())
                rigidbody.AddForce(dir, ForceMode.Impulse);
        }

        private void Hurt(float damage)
        {
            if (_isDead) return;
            if (_currentHP > 0)
            {
                _currentHP -= damage;
            }

            if (_currentHP <= 0)
            {
                StateBot = StateBot.Died;
                Agent.enabled = false;
                foreach (var child in GetComponentsInChildren<Transform>())
                {
                    child.parent = null;
                    var tempRbChild = child.GetComponent<Rigidbody>();
                    if (!tempRbChild)
                    {
                        tempRbChild = child.gameObject.AddComponent<Rigidbody>();
                    }
                    Destroy(child.gameObject, _timeToDestroy);
                }

                OnDieChange.Invoke(this);
                _isDead = true;
            }
        }

        public void Tick()
        {
            if (StateBot == StateBot.Died) return;

            _reSeekTime += Time.deltaTime;

            if (_reSeekTime > 0.5f)
            {
                _reSeekTime = 0;
                SeekForTarget();
            }

            if (StateBot != StateBot.Detected)
            {
                if (!Agent.hasPath)
                {
                    if (StateBot != StateBot.Inspection)
                    {
                        if (StateBot != StateBot.Patrol)
                        {
                            StateBot = StateBot.Patrol;
                            _point = Patrol.GenericPoint(transform);
                            MovePoint(_point);
                            Agent.stoppingDistance = 0;
                        }
                        else
                        {
                            if ((_point - transform.position).sqrMagnitude <= 1)
                            {
                                StateBot = StateBot.Inspection;
                                _timeRemaining.AddTimeRemaining();
                            }
                        }
                    }
                }

                if (_target != null && Vision.Sence(transform, _target))
                {
                    StateBot = StateBot.Detected;
                }
            }
            else if (_target != null)
            {
                /*if (Math.Abs(Agent.stoppingDistance - _stoppingDistance) > Mathf.Epsilon)
                {
                    Agent.stoppingDistance = _stoppingDistance;
                }*/
                Agent.stoppingDistance = 4;
                if (Vision.VisionM(transform, _target))
                {
                    Weapon.transform.LookAt(_target);
                    Weapon.Fire();
                }
                else if (Vision.Sence(transform, _target))
                {
                    transform.LookAt(_target);
                    MovePoint(_target.position);
                }
                else
                {
                    StateBot = StateBot.Inspection;
                    _timeRemaining.AddTimeRemaining();
                    Agent.ResetPath();
                    _target = null;
                }
            }
        }

        private void SeekForTarget()
        {
            var colliders = Physics.OverlapSphere(transform.position, 10);
            foreach (var collider in colliders)
            {
                if (collider.TryGetComponent<BaseUnit>(out var unit))
                {
                    if (unit.GetTeam() != _teamColor)
                    {
                        if (_target == null || (transform.position - collider.transform.position).sqrMagnitude < (transform.position - _target.position).sqrMagnitude)
                        {
                            _target = collider.transform;
                        }
                    }
                }
            }
        }

        private void ResetStateBot()
        {
            StateBot = StateBot.None;
        }

        public override void SetTeam(TeamColor teamColor)
        {
            _teamColor = teamColor;
            switch (_teamColor)
            {
                case TeamColor.None:
                    Color = Color.white;
                    break;
                case TeamColor.Red:
                    Color = Color.red;
                    break;
                case TeamColor.Blue:
                    Color = Color.blue;
                    break;
                default:
                    Color = Color.white;
                    break;
            }
        }
        protected abstract void CalcDmgAndPushDir(InfoCollision info, out float dmg, out Vector3 push);

        #endregion


        #region ISelectObj

        public string GetMessage()
        {
            return gameObject.name;
        }

        public AimColor GetAimColor()
        {
            return AimColor.Red;
        }

        #endregion


        #region IDamageable

        public void SetDamage(InfoCollision info)
        {
            CalcDmgAndPushDir(info, out _damage, out _pushDir);
            Push(_pushDir);
            Hurt(_damage);
        }

        #endregion
    }
}
