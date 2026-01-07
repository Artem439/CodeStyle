using UnityEngine;

public class TargetMover : MonoBehaviour
{
    [SerializeField] private Transform _parentsOfTargets;
    [SerializeField] private Transform[] _targetPoints;
    
    [SerializeField] private float _speed;
    
    private int _targetPositionIndex;

    public void Update()
    {
        Transform pointByNumberInArray = _targetPoints[_targetPositionIndex];
        transform.position = Vector3.MoveTowards(transform.position , pointByNumberInArray.position, _speed * Time.deltaTime);

        if (transform.position == pointByNumberInArray.position)
            ChangePoint();
    }
    
    private Vector3 ChangePoint()
    {
        _targetPositionIndex++;

        if (_targetPositionIndex == _targetPoints.Length)
            _targetPositionIndex  = 0;

        Vector3 pointPosition = _targetPoints[_targetPositionIndex].transform.position;
        transform.forward = pointPosition - transform.position;
        
        return pointPosition;
    }
    
#if UNITY_EDITOR
    [ContextMenu("Refresh Target Points")]
    private void RefreshTargetPoints()
    {
        _targetPoints = new Transform[_parentsOfTargets.childCount];

        for (int i = 0; i < _parentsOfTargets.childCount; i++)
            _targetPoints[i] = _parentsOfTargets.GetChild(i).GetComponent<Transform>();
    }
#endif
}