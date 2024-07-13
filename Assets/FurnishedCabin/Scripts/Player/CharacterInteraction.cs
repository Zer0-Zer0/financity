using UnityEngine;

public class CharacterInteraction : MonoBehaviour
{
    public CharacterDialogue characterDialogue; // Referência ao script CharacterDialogue
    public int dialogueID; // O ID do diálogo que você deseja reproduzir ao interagir com o personagem
    public float interactionRadius = 2f; // O raio de interação com o personagem
    public KeyCode interactionKey = KeyCode.E; // A tecla para interagir com o personagem
    public GameObject player; // Referência ao GameObject do jogador

    void Update()
    {
        // Verifica se o jogador está dentro do raio de interação e pressionou a tecla de interação
        if (Input.GetKeyDown(interactionKey) && IsPlayerInRange())
        {
            // Chama o método Speak do script CharacterDialogue com o ID de diálogo especificado
            characterDialogue.Speak(dialogueID);
        }
    }

    // Método para verificar se o jogador está dentro do raio de interação com o personagem
    bool IsPlayerInRange()
    {
        if (player != null)
        {
            return Vector3.Distance(transform.position, player.transform.position) <= interactionRadius;
        }
        else
        {
            Debug.LogWarning("Referência ao jogador não definida em CharacterInteraction!");
            return false;
        }
    }

    // Método para desenhar o gizmo do raio de interação na cena para facilitar a visualização
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }
}
