using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform SpawnPosition;
    public GameObject[] WayPoints;
    public GameObject EnemyPrefab;

    private bool _bCanSpawn = true;

    private void Start()
    {
        Activate();
    }

    private void Update()
    {
        
    }

    public void Activate()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while (_bCanSpawn)
        {
            yield return new WaitForSeconds(5);

            GameObject EnemyInst = Instantiate(EnemyPrefab, SpawnPosition);
            Enemy EnemyCom = EnemyInst.GetComponent<Enemy>();
            if (EnemyCom)
            {
                EnemyCom.WayPoints = WayPoints;
            }
        }
    }

}