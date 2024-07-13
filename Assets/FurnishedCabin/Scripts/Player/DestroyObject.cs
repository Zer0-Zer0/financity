using UnityEngine;
using UnityEngine.UI;

public class DestroyObject : MonoBehaviour
{
    public float activationDistance = 3f;
    public KeyCode activationKey = KeyCode.E;
    public GameObject player;

    public ObjectiveSystem objetivo;

    public AnimatedText texto;

    public CharacterDialogue dublagem;

    public RawImage phone;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {

        if (Input.GetKeyDown(activationKey))
        {

            if (IsPlayerNear())
            {
                Destroy(gameObject);
                objetivo.CompleteObjective(2);
                texto.ShowText("Agora você pode abrir o celular apertando 'T'!");
                dublagem.Speak(4);
                phone.gameObject.SetActive(true);

            }
        }
    }

    private bool IsPlayerNear()
    {
        return Vector3.Distance(transform.position, player.transform.position) <= activationDistance;
    }
}
