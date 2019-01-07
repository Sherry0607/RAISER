using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{

    public GameObject hazard;

    public float startWait = 0.0f; //刷新时间间隔
    public int hazardCount = 5;

    private Quaternion spawnRotation;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(SpawnWaves());

    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);

        while (true)
        {
            for (int i = 0; i < hazardCount; ++i)
            {
                spawnRotation = Quaternion.identity;
                Instantiate(hazard, transform.position, transform.rotation);
                yield return new WaitForSeconds(1);
            }

            yield return new WaitForSeconds(startWait);
        }
    }

}