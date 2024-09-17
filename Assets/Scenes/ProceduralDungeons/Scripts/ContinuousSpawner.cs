using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinuousSpawner : SpawnRandomLoot
{
    [Header("Continuous Spawn")]
    [SerializeField] float MinTime = 120f;
    [SerializeField] float MaxTime = 240f;

    private bool isSpawning;

    void Update()
    {
        if (isSpawning)
            return;

        StartCoroutine(TimedSpawn());
    }

    IEnumerator TimedSpawn()
    {
        isSpawning = true;
        yield return new WaitForSeconds(Random.Range(MinTime, MaxTime));
        SpawnRandomLootFromList();
        isSpawning = false;
    }
}