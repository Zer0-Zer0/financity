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
    private bool _spawned = false;

    public void SpawnPrefab()
    {
        bool _canSpawn;
        
        if (_onlyOnce && !_spawned || !_onlyOnce)
        {
            _canSpawn = true;
        }
        else
        {
            _canSpawn = false;
        }

        bool _isAnythingNull = prefabToSpawn != null && spawnPoint != null && parentObject != null;

        if (_isAnythingNull && _canSpawn)
        {
            GameObject spawnedPrefab = Instantiate(
                prefabToSpawn,
                spawnPoint.position,
                spawnPoint.rotation,
                parentObject
            );
            _spawned = true;
        }
        else
        {
            Debug.LogWarning(
                "PrefabToSpawn, SpawnPoint, or ParentObject is not assigned in the inspector."
            );
        }
    }
}
