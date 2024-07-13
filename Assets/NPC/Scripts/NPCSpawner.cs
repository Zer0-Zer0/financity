using UnityEngine;
using System.Collections.Generic;

public class NPCSpawner : MonoBehaviour
{
    public List<GameObject> npcPrefabs;
    public LayerMask groundLayer;
    public float spawnRadius = 50f;
    public float spawnInterval = 5f;
    public int maxNPCs = 10;
    public float minDistanceBetweenNPCs = 5f;
    public Transform player;

    private List<Vector3> npcPositions = new List<Vector3>();
    private float spawnTimer = 0f;
    private Camera mainCamera;

    private void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    private void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            spawnTimer = 0f;
            int currentNPCCount = GetNPCCountAroundPlayer();
            if (currentNPCCount < maxNPCs)
            {
                SpawnNPC();
            }
        }
    }

    private void SpawnNPC()
    {
        Vector3 spawnPoint = GetRandomSpawnPoint();

        if (spawnPoint != Vector3.zero)
        {
            npcPositions.Add(spawnPoint);
            GameObject npcPrefab = npcPrefabs[Random.Range(0, npcPrefabs.Count)];
            GameObject newNPC = Instantiate(npcPrefab, spawnPoint, Quaternion.identity);
            newNPC.tag = "npc";
        }
    }

    private Vector3 GetRandomSpawnPoint()
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomDirection = Random.insideUnitSphere * spawnRadius;
            randomDirection.y = 0;
            Vector3 spawnPosition = transform.position + randomDirection + Vector3.up * 10f;

            if (Physics.Raycast(spawnPosition, Vector3.down, out RaycastHit hit, Mathf.Infinity, groundLayer))
            {
                if (!IsPointTooCloseToOtherNPCs(hit.point) && !IsPointInView(hit.point))
                {
                    return hit.point;
                }
            }
        }

        return Vector3.zero;
    }

    private bool IsPointTooCloseToOtherNPCs(Vector3 point)
    {
        foreach (Vector3 npcPosition in npcPositions)
        {
            if (Vector3.Distance(npcPosition, point) < minDistanceBetweenNPCs)
            {
                return true;
            }
        }
        return false;
    }

    private bool IsPointInView(Vector3 point)
    {
        Vector3 viewportPoint = mainCamera.WorldToViewportPoint(point);
        bool inView = viewportPoint.x >= 0 && viewportPoint.x <= 1 && viewportPoint.y >= 0 && viewportPoint.y <= 1 && viewportPoint.z > 0;
        return inView;
    }

    private int GetNPCCountAroundPlayer()
    {
        int count = 0;
        GameObject[] npcs = GameObject.FindGameObjectsWithTag("npc");
        npcPositions.Clear();

        foreach (GameObject npc in npcs)
        {
            npcPositions.Add(npc.transform.position);
            if (Vector3.Distance(player.position, npc.transform.position) <= spawnRadius)
            {
                count++;
            }
        }
        return count;
    }
}
