using UnityEngine;
using UnityEngine.AI;

public class CarController : MonoBehaviour
{
    NavMeshAgent agent;
    Vector3 currentTarget;
    float obstacleAvoidanceRange = 8f; // Distância em que o carro começa a considerar desviar de obstáculos
    float maxAvoidanceAngle = 30f; // Ângulo máximo para considerar desviar de obstáculos

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false; // Desativa o freio automático do NavMeshAgent
        SetNewRandomTarget();
    }

    void Update()
    {
        // Verifica se o carro está próximo o suficiente do destino atual
        if (Vector3.Distance(transform.position, currentTarget) < 3f)
        {
            SetNewRandomTarget(); // Define um novo destino próximo
        }

        // Verifica se há obstáculos à frente
        if (CheckObstacleAhead())
        {
            AvoidObstacle(); // Desvia do obstáculo se necessário
        }
        else
        {
            agent.isStopped = false; // Continua o movimento se não há obstáculos
        }
    }

    void SetNewRandomTarget()
    {
        float coneAngle = 45f;
        float distance = Random.Range(20f, 50f);

        Vector3 randomDirection = Quaternion.Euler(0, Random.Range(-coneAngle, coneAngle), 0) * transform.forward;
        randomDirection.Normalize();

        Vector3 randomPoint = transform.position + randomDirection * distance;

        NavMeshHit hit;
        NavMesh.SamplePosition(randomPoint, out hit, distance, NavMesh.AllAreas);
        currentTarget = hit.position;
        agent.SetDestination(hit.position);
    }

    bool CheckObstacleAhead()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, obstacleAvoidanceRange))
        {
            return true; // Qualquer coisa na frente é considerada um obstáculo
        }
        return false;
    }

    void AvoidObstacle()
    {
        // Freia o carro
        agent.isStopped = true;

        // Calcula um vetor de desvio para a esquerda ou direita
        Vector3 rightVector = Quaternion.Euler(0, 90, 0) * transform.forward; // Vetor para a direita
        Vector3 leftVector = Quaternion.Euler(0, -90, 0) * transform.forward; // Vetor para a esquerda

        // Escolhe a direção de desvio com base no espaço disponível
        if (!CheckObstacleDirection(rightVector) && CheckObstacleDirection(leftVector))
        {
            agent.Move(leftVector * agent.speed * Time.deltaTime);
        }
        else if (CheckObstacleDirection(rightVector) && !CheckObstacleDirection(leftVector))
        {
            agent.Move(rightVector * agent.speed * Time.deltaTime);
        }
        // Se não for possível desviar nem para direita nem para esquerda, apenas freia
        // Neste caso, o carro deve continuar parado até que o obstáculo seja removido
    }

    bool CheckObstacleDirection(Vector3 direction)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, obstacleAvoidanceRange))
        {
            if (Vector3.Angle(transform.forward, direction) < maxAvoidanceAngle)
            {
                return true;
            }
        }
        return false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(currentTarget, 0.5f); // Desenha uma esfera vermelha no destino atual para visualização
    }
}
