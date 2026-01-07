using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    
    public GameObject BulletPrefab => _bulletPrefab;
}