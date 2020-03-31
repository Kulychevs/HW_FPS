using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathDisplayer : MonoBehaviour
{
    #region Fields

    [SerializeField] private Transform _agent;
    [SerializeField] private Transform _checkPoint;
     
    private Camera _camera;
    private NavMeshPath _path;
    private Transform tempPoint;
    private Queue<Transform> _points;
    private LineRenderer _lineRenderer;
    private readonly Color _color = Color.red;

    #endregion


    #region UnityMethods

    private void Start()
    {
        _camera = FindObjectOfType<Camera>();
        _path = new NavMeshPath();
        tempPoint = _agent;
        _points = new Queue<Transform>();
        LineRendererInicialization();                   
    }

    private void Update()
    {
        if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out var hit))
        {
            DrawPath(hit.point);
        }

        if (Input.GetMouseButtonDown(0))
        {
            AddCheckPoint();
        }
    }

    #endregion


    #region Methods

    private void LineRendererInicialization()
    {
        _lineRenderer = new GameObject("LineRenderer").AddComponent<LineRenderer>();
        _lineRenderer.startWidth = 0.5F;
        _lineRenderer.endWidth = 0.5F;
        _lineRenderer.startColor = _color;
        _lineRenderer.endColor = _color;
    }

    private void DrawPath( Vector3 position)
    {
        NavMesh.CalculatePath(tempPoint.position, position, NavMesh.AllAreas, _path);
        _lineRenderer.positionCount = _path.corners.Length;
        _lineRenderer.SetPositions(_path.corners);
    }

    private void AddCheckPoint()
    {
        if (_path.corners.Length > 0)
        {
            var p = Instantiate(_checkPoint, _path.corners[_path.corners.Length - 1], Quaternion.identity);
            _points.Enqueue(p);
            tempPoint = p;
        }
    }

    public Transform GetDestination()
    {
        if (_points.Count > 0)
        {
            if (_points.Count > 1)
            {
                Destroy(_points.Peek().gameObject);
                _points.Dequeue();
            }
            return _points.Peek();
        }
        else
            return null;
    }

    #endregion
}
