using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject _currentWayPoint;
    private int _wayPointCount = 0;
    private Vector3 _moveDirection = Vector3.zero;
    private int _hp = 5;

    [HideInInspector]
    public GameObject[] WayPoints;
    public int MaxHp = 5;
    public float MoveSpeed = 10;

    private void Start()
    {
        _currentWayPoint = WayPoints[0];
        _hp = MaxHp;
        SetRotationByDirection();
    }

    private void Update()
    {
        transform.position += _moveDirection * MoveSpeed * Time.deltaTime;

        if (Vector3.Distance(transform.position , _currentWayPoint.transform.position) <= 0.01f)
        {
            if(_wayPointCount >= WayPoints.Length - 1)
            {
                Destroy(gameObject);
                return;
            }

            _wayPointCount = Mathf.Clamp(_wayPointCount + 1, 0, WayPoints.Length);
            _currentWayPoint = WayPoints[_wayPointCount];

            SetRotationByDirection();
        }
    }

    private void SetRotationByDirection()
    {
        _moveDirection = _currentWayPoint.transform.position - transform.position;
        _moveDirection.Normalize();
        transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(_moveDirection.y, _moveDirection.x) * Mathf.Rad2Deg);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            _hp = Mathf.Clamp(_hp - 1, 0, MaxHp);

            if (_hp <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
