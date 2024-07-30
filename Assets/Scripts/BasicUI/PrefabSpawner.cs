using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject prefabToSpawn;

    [SerializeField]
    private Transform spawnPoint;

    [SerializeField]
    private Transform parentObject;

    [SerializeField]
    private bool _onlyOnce = true;

    public void SpawnPrefab()
    {
        bool _isAnythingNull = prefabToSpawn != null && spawnPoint != null && parentObject != null;

        if (_isAnythingNull)
        {
            GameObject spawnedPrefab = Instantiate(
                prefabToSpawn,
                spawnPoint.position,
                spawnPoint.rotation,
                parentObject
            );

            if (_onlyOnce)
            {
                Destroy(this);
            }
        }
        else
        {
            Debug.LogWarning(
                "PrefabToSpawn, SpawnPoint, or ParentObject is not assigned in the inspector."
            );
        }
    }
}
