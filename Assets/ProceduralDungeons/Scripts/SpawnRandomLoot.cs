using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRandomLoot : MonoBehaviour
{
    [SerializeField] GameObject[] randomLoot;
    [SerializeField, Range(0f, 100f)] float spawnChance = 50f;
    private bool hasSpawned;

    void OnEnable()
    {
        if (!hasSpawned)
            SpawnRandomLootFromList();
    }

    void SpawnRandomLootFromList()
    {
        if (randomLoot.Length <= 0)
        {
            Debug.LogWarning("Random loot array is empty!");
            return;
        }

        float spawnValue = Random.value * 100f;

        if (spawnValue <= spawnChance)
        {
            int randomIndex = Random.Range(0, randomLoot.Length);
            Quaternion randomRotation = Quaternion.Euler(0, Random.Range(-360f, 360f), 0);
            GameObject loot = Instantiate(randomLoot[randomIndex], transform.position, randomRotation);
            loot.transform.parent = transform;
            hasSpawned = true;

            Debug.Log($"Sucessfully spawned loot \"{loot.name}\"!");
        }
    }
}