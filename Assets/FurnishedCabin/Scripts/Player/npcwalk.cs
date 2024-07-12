using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class NPCController : MonoBehaviour
{
    private NavMeshAgent agent;
    private Vector3[] directions;
    private Quaternion desiredRotation;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        directions = new Vector3[]
        {
            Vector3.forward,
            Vector3.back,
            Vector3.left,
            Vector3.right,
            new Vector3(1, 0, 1),
            new Vector3(1, 0, -1),
            new Vector3(-1, 0, 1),
            new Vector3(-1, 0, -1)
        };

        SetNextWaypoint();
    }

    void Update()
    {
        if (agent.remainingDistance < 0.5f && !agent.pathPending)
        {
            SetNextWaypoint();
        }

        // Calcular a direção do movimento com base na velocidade do agente
        Vector3 moveDirection = agent.velocity.normalized;

        if (moveDirection != Vector3.zero)
        {
            // Suavizar a rotação na direção do movimento
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection), Time.deltaTime * 5f);
        }
    }

    void SetNextWaypoint()
    {
        List<Vector3> allPoints = new List<Vector3>();
        NavMeshTriangulation triangulation = NavMesh.CalculateTriangulation();
        for (int i = 0; i < triangulation.vertices.Length; i++)
        {
            allPoints.Add(triangulation.vertices[i]);
        }

        Vector3 startingPoint = transform.position;
        Vector3 farthestPoint = FindFarthestPointInRandomDirection(startingPoint, allPoints);

        agent.SetDestination(farthestPoint);
    }

    Vector3 FindFarthestPointInRandomDirection(Vector3 startPoint, List<Vector3> points)
    {
        Vector3 randomDirection = directions[Random.Range(0, directions.Length)];
        Vector3 farthestPoint = startPoint;
        float maxDistance = 0f;

        foreach (Vector3 point in points)
        {
            Vector3 directionToPoint = (point - startPoint).normalized;
            if (Vector3.Dot(randomDirection, directionToPoint) > 0.5f)
            {
                NavMeshPath path = new NavMeshPath();
                if (NavMesh.CalculatePath(startPoint, point, NavMesh.AllAreas, path))
                {
                    float pathDistance = GetPathDistance(path);
                    if (pathDistance > maxDistance)
                    {
                        maxDistance = pathDistance;
                        farthestPoint = point;
                    }
                }
            }
        }

        return farthestPoint;
    }

    float GetPathDistance(NavMeshPath path)
    {
        float distance = 0f;

        if (path.corners.Length < 2)
            return distance;

        for (int i = 0; i < path.corners.Length - 1; i++)
        {
            distance += Vector3.Distance(path.corners[i], path.corners[i + 1]);
        }

        return distance;
    }
}
