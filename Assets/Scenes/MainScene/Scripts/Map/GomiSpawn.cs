using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GomiSpawn : MonoBehaviour
{
    [SerializeField] List<GameObject> SpawnPrefabList = new List<GameObject>();
    [SerializeField] Vector2 spawnPositionA;
    [SerializeField] Vector2 spawnPositionB;
    [SerializeField] float SpawnIntervalMin;
    [SerializeField] float SpawnIntervalMax;
    [SerializeField] int SpawnCountMax;

    ReiziValue reiziValue;

    void Start()
    {
        reiziValue = GameObject.FindWithTag("Player").GetComponent<ReiziValue>();
        StartCoroutine(Spawn());
    }
    IEnumerator Spawn()
    {
        if (transform.childCount < SpawnCountMax && !reiziValue.isFishBook)
        {
            Instantiate(SpawnPrefabList[Random.Range(0, SpawnPrefabList.Count)],
                        new Vector3(Random.Range(spawnPositionA.x, spawnPositionB.x), Random.Range(spawnPositionA.y, spawnPositionB.y), 0),
                        Quaternion.identity, transform);
        }
        yield return new WaitForSeconds(Random.Range(SpawnIntervalMin, SpawnIntervalMax));
        StartCoroutine(Spawn());
    }
}
