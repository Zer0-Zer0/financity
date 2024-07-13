using UnityEngine;

public class Radio : MonoBehaviour
{
    public bool isOn = false;
    public AudioClip firstClip;
    public AudioClip secondClip;
    public AudioSource audioSource;
    public CharacterDialogue characterDialogue;
    private bool canTurnOn = true;
    public float activationDistance = 3f;
    public KeyCode activationKey = KeyCode.E;
    public GameObject player;

    public ObjectiveSystem objetivos;
    public AnimatedText objtvcentral;

    public CutsceneUm cut;

    private bool finalizado = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (Input.GetKeyDown(activationKey) && IsPlayerNear())
        {
            TryTurnOnRadio();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!isOn && canTurnOn && other.gameObject == player)
        {
            if (IsPlayerNear())
            {
                isOn = true;
                PlayFirstClip();
            }
        }
    }

    void PlayFirstClip()
    {
        canTurnOn = false;
        audioSource.clip = firstClip;
        audioSource.Play();
        Invoke("PlaySecondClip", firstClip.length);
    }

    void PlaySecondClip()
    {
        audioSource.clip = secondClip;
        audioSource.Play();
        Invoke("PlayFirstClipAgain", secondClip.length);
    }

    void PlayFirstClipAgain()
    {
        audioSource.clip = firstClip;
        audioSource.Play();
        characterDialogue.Speak(0);
        finalizado = true;
        objtvcentral.ShowText("Objetivo:\nLigue para P.E.E.B!");
        objetivos.CompleteObjective(1);
        objetivos.AddObjective(3, "Ligue para P.E.E.B!");
        cut.objetivo = true;
    }

    public void TurnOffRadio()
    {
        isOn = false;
        audioSource.Stop();
    }

    public void TryTurnOnRadio()
    {
        if (canTurnOn)
        {
            isOn = true;
            PlayFirstClip();
        }
        else
        {
            if (finalizado)
            {
                characterDialogue.Speak(1);
            }
        }
    }

    private bool IsPlayerNear()
    {
        return Vector3.Distance(transform.position, player.transform.position) <= activationDistance;
    }
}
