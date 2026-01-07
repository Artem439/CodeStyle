using System.Collections.Generic;
using UnityEngine;

public class TargetMover : MonoBehaviour
{
    [SerializeField] private List<Transform> _targetPoints;
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _reachDistance = 0.2f;
    
    private int _targetPositionIndex;
    private Vector3 _targetPosition;
    
     private void Start()
    {
        if (_targetPoints == null || _targetPoints.Count == 0)
        {
            Debug.LogError($"{name}: Target points list is empty");
            enabled = false;
            return;
        }

        _targetPositionIndex = 0;
        SetTargetPosition();
    }
    
    private void Update()
    {
        UpdateTarget();
        
        Move();
    }

    private void Move()
    {
        Vector3 nextPosition = Vector3.MoveTowards(transform.position , _targetPosition, _speed * Time.deltaTime);
        
        nextPosition.y = transform.position.y;
        transform.position = nextPosition;
    }

    private void UpdateTarget()
    {
        Vector2 currentXZ = new Vector2(transform.position.x, transform.position.z);
        Vector2 targetXZ = new Vector2(_targetPosition.x, _targetPosition.z);

        if (Vector2.Distance(currentXZ, targetXZ) <= _reachDistance)
        {
            _targetPositionIndex = (_targetPositionIndex + 1) % _targetPoints.Count;
            SetTargetPosition();
        }
    }
    
    private void SetTargetPosition()
    {
        Vector3 pointPosition = _targetPoints[_targetPositionIndex].position;

        _targetPosition = new Vector3(pointPosition.x, transform.position.y, pointPosition.z);
    }
}