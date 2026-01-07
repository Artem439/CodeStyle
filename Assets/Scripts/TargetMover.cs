using UnityEngine;

public class TargetMover : MonoBehaviour
{
    private const float PositionEpsilon = 0.0001f;
    
    [SerializeField] private Transform _parentsOfTargets;
    [SerializeField] private Transform[] _targetPoints;
    
    [SerializeField] private float _speed;
    
    private int _targetPositionIndex;

    public void Update()
    {
        Transform targetPoint = _targetPoints[_targetPositionIndex];
        transform.position = Vector3.MoveTowards(transform.position , targetPoint.position, _speed * Time.deltaTime);

        if ((transform.position - targetPoint.position).sqrMagnitude < PositionEpsilon)
            ChangePoint();
    }
    
    private void ChangePoint()
    {
        _targetPositionIndex = (_targetPositionIndex + 1) % _targetPoints.Length;
        
        Vector3 pointPosition = _targetPoints[_targetPositionIndex].transform.position;
        transform.forward = pointPosition - transform.position;
    }
    
#if UNITY_EDITOR
    [ContextMenu("Refresh Target Points")]
    private void RefreshTargetPoints()
    {
        _targetPoints = new Transform[_parentsOfTargets.childCount];

        for (int i = 0; i < _parentsOfTargets.childCount; i++)
            _targetPoints[i] = _parentsOfTargets.GetChild(i);
    }
#endif
}