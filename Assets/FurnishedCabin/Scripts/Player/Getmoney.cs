using UnityEngine;

public class Getmoney : MonoBehaviour
{
    public float activationDistance = 3f;
    public KeyCode activationKey = KeyCode.E;
    public GameObject player;
    public AnimatedText texto;
    public CharacterDialogue dublagem;
    public DayNightCycle saldo;

    private bool moneyCollected = false;
    private int fala;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (!moneyCollected && Input.GetKeyDown(activationKey))
        {
            if (IsPlayerNear())
            {
                // Destruir o objeto depois de coletar o dinheiro
                Destroy(gameObject);

                // Gerar valor aleatório de dinheiro entre 50 e 200 reais
                float dinheiro = Random.Range(5000, 20001) * 0.01f; // Gera números entre 5000 e 20000 e divide por 100

                // Arredondar o valor para o centavo mais próximo múltiplo de 0.05
                dinheiro = Mathf.Round(dinheiro / 0.05f) * 0.05f;

                // Converter para valor monetário com duas casas decimais
                string dinheiroFormatado = dinheiro.ToString("F2");

                // Gerar fala aleatória
                fala = Random.Range(14, 18);

                // Exibir mensagem e fala
                texto.ShowText("Você achou R$" + dinheiroFormatado + "!");
                dublagem.Speak(fala);

                // Adicionar dinheiro ao saldo
                saldo.AddMoney(dinheiro);

                // Marcar que o dinheiro foi coletado para evitar interações repetidas
                moneyCollected = true;
            }
        }
    }

    private bool IsPlayerNear()
    {
        return Vector3.Distance(transform.position, player.transform.position) <= activationDistance;
    }
}
