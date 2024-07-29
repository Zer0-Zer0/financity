using UnityEngine;
using TMPro;

public class NPCDialogue : MonoBehaviour
{
    public string[] dialogueLines;
    public float interactionRadius = 5f;
    public float chanceToSpeak = 0.5f;
    public TextMeshProUGUI dialogueText;

    private Transform playerTransform;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    void Update()
    {
        if (playerTransform != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

            if (distanceToPlayer <= interactionRadius)
            {
                TrySpeak();
            }
            else
            {
                HideDialogue();
            }
        }
    }

    private void TrySpeak()
    {
        if (Random.value <= chanceToSpeak)
        {
            if (dialogueLines.Length > 0)
            {
                int randomIndex = Random.Range(0, dialogueLines.Length);
                ShowDialogue(dialogueLines[randomIndex]);
            }
        }
    }

    private void ShowDialogue(string dialogue)
    {
        dialogueText.text = dialogue;
        dialogueText.gameObject.SetActive(true);
    }

    private void HideDialogue()
    {
        dialogueText.gameObject.SetActive(false);
    }
}
