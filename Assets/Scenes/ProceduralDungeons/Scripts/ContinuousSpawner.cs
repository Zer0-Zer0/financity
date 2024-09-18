using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinuousSpawner : SpawnRandomLoot
{
    [Header("Continuous Spawn")]
    [SerializeField] float MinTime = 120f;
    [SerializeField] float MaxTime = 240f;
    [Header("Spawnpoint Visibility")]

    [SerializeField] Renderer renderReference;
    private bool isSpawning;

    protected override void OnEnable() {}

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
        if (renderReference == null || renderReference.isVisible == false)
            SpawnRandomLootFromList();
        isSpawning = false;
    }
}