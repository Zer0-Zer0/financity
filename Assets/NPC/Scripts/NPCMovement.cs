using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : MonoBehaviour
{
    public Transform[] waypoints;
    private NavMeshAgent agent;
    private Animator animator;
    private int waypointIndex = 0;
    private bool isWalking = false;
    public float rotationSpeed = 2.0f; 


    void Start()
    {
        

        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        animator.Play("Idle");
        
    }

    void Update()
    {
        // adicone o movimento do npc
         // Verifica se o NPC está se movendo (considerando velocidade mínima)
    bool isMoving = agent.velocity.magnitude > 0.1f;

    // Atualiza a animação "walk" baseado no movimento
    animator.SetBool("walk", isMoving);

    if (agent.remainingDistance < 0.5f && !agent.pathPending)
    {
      SetNextDestination();
    }
    

        if (agent.remainingDistance < 0.5f && !agent.pathPending)
        {
            SetNextDestination();
        }

       
        


        if (agent.remainingDistance < 0.5f && !agent.pathPending) {
    isWalking = false;
    animator.Play("Idle");
} else {
    isWalking = true;
    //animator.Play("Walk");
}

if (isWalking) {
  // Calcule o vetor de direção
  Vector3 direction = agent.destination - transform.position;
  direction.y = 0; // Ignore o movimento vertical

  // Gire o NPC para encarar a direção (rotação suave opcional)
  transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotationSpeed);
}


    void SetNextDestination()
    {
        if (waypoints.Length == 0)
            return;

        agent.destination = waypoints[waypointIndex].position;
        waypointIndex = (waypointIndex + 1) % waypoints.Length;
    }
}
}
