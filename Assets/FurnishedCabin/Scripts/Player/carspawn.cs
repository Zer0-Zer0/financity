using UnityEngine;
using System.Collections.Generic;

public class CarSpawner : MonoBehaviour
{
    public List<GameObject> carPrefabs; // Lista de prefabs de carros
    public LayerMask groundLayer;
    public float spawnRadius = 50f;
    public float spawnInterval = 10f;
    public int maxCars = 5;
    public float minDistanceBetweenCars = 10f;
    public Transform player;

    private List<GameObject> spawnedCars = new List<GameObject>(); // Lista de carros instanciados
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
            int currentCarCount = GetCarCountAroundPlayer();
            if (currentCarCount < maxCars)
            {
                SpawnCar();
            }
        }
    }

    private void SpawnCar()
    {
        Vector3 spawnPoint = GetRandomSpawnPoint();

        if (spawnPoint != Vector3.zero)
        {
            GameObject carPrefab = carPrefabs[Random.Range(0, carPrefabs.Count)]; // Escolhe um prefab de carro aleatório
            GameObject newCar = Instantiate(carPrefab, spawnPoint, Quaternion.identity);
            newCar.tag = "car";
            spawnedCars.Add(newCar); // Adiciona o novo carro à lista de carros instanciados
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
                if (!IsPointTooCloseToOtherCars(hit.point) && !IsPointInView(hit.point))
                {
                    return hit.point;
                }
            }
        }

        return Vector3.zero;
    }

    private bool IsPointTooCloseToOtherCars(Vector3 point)
    {
        foreach (GameObject car in spawnedCars)
        {
            if (car != null && Vector3.Distance(car.transform.position, point) < minDistanceBetweenCars)
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

    private int GetCarCountAroundPlayer()
    {
        int count = 0;
        foreach (GameObject car in spawnedCars)
        {
            if (car != null && Vector3.Distance(player.position, car.transform.position) <= spawnRadius)
            {
                count++;
            }
        }
        return count;
    }
}
