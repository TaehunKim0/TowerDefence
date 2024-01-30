using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform SpawnPosition;
    public GameObject[] WayPoints;
    public GameObject EnemyPrefab;
    public float SpawnCycleTime = 1f;

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
            yield return new WaitForSeconds(SpawnCycleTime);

            GameObject EnemyInst = Instantiate(EnemyPrefab, SpawnPosition);
            Enemy EnemyCom = EnemyInst.GetComponent<Enemy>();
            if (EnemyCom)
            {
                EnemyCom.WayPoints = WayPoints;
            }
        }
    }

}
