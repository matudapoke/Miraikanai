using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Spawn : MonoBehaviour
{
    [SerializeField] List<GameObject> SpawnPrefab = new List<GameObject>();
    [SerializeField] List<GameObject> SpawnObj = new List<GameObject>();
    [SerializeField] float MoveSpeedX;
    [SerializeField] float MoveSpeedY;
    [SerializeField] float SpawnIntervalMax;
    [SerializeField] float SpawnIntervalMin;
    [SerializeField] float SpawnPositionXMax;
    [SerializeField] float SpawnPositionXMin;
    [SerializeField] float SpawnPositionY;
    [SerializeField] float DestroyPositionY;
    void Start()
    {
        StartCoroutine(RandomSpawn());
    }
    void Update()
    {
        for (int i = 0; i < SpawnObj.Count; i++)
        {
            SpawnObj[i].transform.position += new Vector3(MoveSpeedX * Time.deltaTime, MoveSpeedY * Time.deltaTime, 0);
            if (SpawnObj[i].transform.position.y >= DestroyPositionY)
            {
                Destroy(SpawnObj[i]);
                SpawnObj.RemoveAt(i);
            }
        }
    }
    IEnumerator RandomSpawn()
    {
        yield return new WaitForSeconds(Random.Range(SpawnIntervalMin, SpawnIntervalMax));
        SpawnObj.Add(Instantiate(SpawnPrefab[Random.Range(0, SpawnPrefab.Count)], new Vector3(Random.Range(SpawnPositionXMin, SpawnPositionXMax), SpawnPositionY, 0), Quaternion.identity, transform));
        StartCoroutine("RandomSpawn");
    }
}
