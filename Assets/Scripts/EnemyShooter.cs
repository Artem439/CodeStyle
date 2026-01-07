using System.Collections;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _shootingDelay;
    [SerializeField] private float _speed;
    
    private Transform _target;
    
    private void Start()
    {
        StartCoroutine(ShootingRoutine());
    }
    
    public void SetTarget(Transform target)
    {
        _target = target;
    }
    
    private IEnumerator ShootingRoutine()
    {
        WaitForSeconds delay = new WaitForSeconds(_shootingDelay);
        
        while (enabled)
        {
            yield return delay;
            
            Vector3 direction = (_target.position - transform.position).normalized;
            GameObject newBullet = Instantiate(_bulletPrefab, transform.position + direction, Quaternion.identity);

            Rigidbody rigidbody = newBullet.GetComponent<Rigidbody>();
            
            rigidbody.transform.up = direction;
            rigidbody.velocity = direction * _speed;
        }
    }
}