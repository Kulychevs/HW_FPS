using UnityEngine;
using UnityEngine.AI;

public class Agent : MonoBehaviour
{
    #region Fields

    [SerializeField] private PathDisplayer _pathDisplayer;

    [HideInInspector] public Transform target;

    private NavMeshAgent _agent;
    private float _timer;

    private const float TIME_WITHOUT_PATH = 1.0f;

    #endregion


    #region UnityMethods

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        _timer += Time.deltaTime;
       
        if (!_agent.hasPath && _timer > TIME_WITHOUT_PATH)
        {
            target = _pathDisplayer.GetDestination();
            _timer = 0;
            if (target != null)
            {
                _agent.SetDestination(target.position);
            }
        }
    }

    #endregion
}
