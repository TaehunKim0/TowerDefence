using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Guardian : MonoBehaviour
{
    private List<GameObject> _targetEnemys = new List<GameObject>();

    public GameObject Projectile;
    public float AttackCycleTime = 1f;
    public float AttackRadius = 5f;
    public int Damage = 1;
    public int MaxTargetCount = 1;

    void Start()
    {
        StartCoroutine(Attack());
        GetComponent<SphereCollider>().radius = AttackRadius;
    }

    IEnumerator Attack()
    {
        if (_targetEnemys.Count > 0)
        {
            SearchEnemy();
            foreach (GameObject target in _targetEnemys)
            {
                SetRotationByDirection();

                GameObject projectileInst = Instantiate(Projectile, transform.position, Quaternion.identity);
                if (projectileInst != null)
                {
                    projectileInst.GetComponent<Projectile>().Damage = Damage;
                    projectileInst.GetComponent<Projectile>().Target = target;
                }
            }
        }

        yield return new WaitForSeconds(AttackCycleTime);

        StartCoroutine(Attack());
    }
    private void SetRotationByDirection()
    {
        Vector3 targetPos = _targetEnemys[0].transform.position;
        targetPos.y = transform.position.y;

        Vector3 dir = targetPos - transform.position;
        dir.Normalize();
        transform.rotation = Quaternion.LookRotation(dir, Vector3.up);
    }

    void SearchEnemy()
    {
        int count = 0;

        List<GameObject> tempList = new List<GameObject>();
        foreach (GameObject target in _targetEnemys)
        {
            if (target != null)
            {
                tempList.Add(target);
                count++;
            }

            if(count >= MaxTargetCount)
            {
                break;
            }
        }

        _targetEnemys = tempList;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (false == _targetEnemys.Contains(other.gameObject))
            {
                _targetEnemys.Add(other.gameObject);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (true == _targetEnemys.Contains(other.gameObject))
            {
                _targetEnemys.Remove(other.gameObject);
            }
        }
    }
}
