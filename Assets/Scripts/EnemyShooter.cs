using System.Collections;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;
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
            
            Spawn(_bulletPrefab);
        }
    }

    private void Spawn(Bullet bullet)
    {
        Vector3 direction = (_target.position - transform.position).normalized;
        
        Bullet newBullet = Instantiate(bullet, transform.position + direction, Quaternion.identity);
        
        Rigidbody rigidbody = newBullet.GetComponent<Rigidbody>();
            
        rigidbody.transform.up = direction;
        rigidbody.velocity = direction * _speed;
    }
}