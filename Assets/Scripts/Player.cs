using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Animator heroiAnim;
    [SerializeField] private bool morto = false;
    [SerializeField] private Collider areaPegarItemCollider; // Collider da área onde o jogador pode pegar o item
    private bool estaDentroDaArea = false;

    private bool estaPendurado = false;
    private Transform rootAlvo;

    private bool gatilho;
    public Transform parede;
    bool pegarTriggerAtivado = false;

    [SerializeField] private Transform item;
    [SerializeField] private Transform mao;

    [SerializeField] private float velRot = 100;

    public float moveX, moveY;

    private bool botaoDireitoPressionado = false;
    private bool estavaMovendo = false; 

    // Variáveis para o sistema de tiro
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform muzzleTransform; // Referência ao pivô de tiro
    public float shootForce = 700f;

    void Start()
    {
        rootAlvo = null;
        gatilho = true;
    }

    void AjustaRotacao()
    {
        if (Vector3.Distance(transform.position, parede.position) <= 3.1f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, parede.rotation, 1f);
        }
    }

    void FixedUpdate()
    {
        if (!rootAlvo) return;
        if (estaPendurado && gatilho)
        {
            transform.position = new Vector3(transform.position.x, rootAlvo.position.y, rootAlvo.position.z);
            gatilho = false;
        }
    }

    void Update()
    {
        moveY = Input.GetAxis("Vertical");
        moveX = Input.GetAxis("Horizontal");
        heroiAnim.SetFloat("X", moveX, 0.1f, Time.deltaTime);
        heroiAnim.SetFloat("Y", moveY, 0.1f, Time.deltaTime);

        float move = Input.GetAxis("Vertical");
        float rotacao = Input.GetAxis("Horizontal") * velRot;

        // Lógica de tiro
        if (Input.GetMouseButtonDown(1))
        {
            botaoDireitoPressionado = true;
            estavaMovendo = heroiAnim.GetBool("Andar"); // Armazena se estava em movimento
            heroiAnim.SetBool("Andar", false); // Para o movimento
            heroiAnim.SetBool("MovimentoTiro", true); // Ativa o movimento de tiro
        }

        if (Input.GetMouseButtonUp(1))
        {
            botaoDireitoPressionado = false;
        }

        if (botaoDireitoPressionado && Input.GetMouseButtonDown(0)) // Botão esquerdo do mouse
        {
            Shoot();
        }

        if (!botaoDireitoPressionado && !estavaMovendo)
        {
            heroiAnim.SetBool("Andar", false);
            heroiAnim.SetBool("MovimentoTiro", false);
        }

        // Corrida
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            heroiAnim.SetBool("Running", true);
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            heroiAnim.SetBool("Running", false);
        }

        if (!morto && !estaPendurado)
        {
            rotacao *= Time.deltaTime;
            transform.Rotate(0, rotacao, 0);
        }

        if (estaPendurado)
        {
            if (rotacao > 1)
            {
                heroiAnim.SetBool("HangingRight", true);
            }
            else if (rotacao < -1)
            {
                heroiAnim.SetBool("HangingLeft", true);
            }
            else
            {
                heroiAnim.SetBool("HangingRight", false);
                heroiAnim.SetBool("HangingLeft", false);
            }
        }

        if (move != 0)
        {
            heroiAnim.SetBool("Andar", true);
        }
        else
        {
            heroiAnim.SetBool("Andar", false);
        }

        if (morto && Input.GetKeyDown(KeyCode.Z))
        {
            heroiAnim.SetTrigger("StandUp");
            morto = false;
        }

        if (estaPendurado && Input.GetKeyDown(KeyCode.Z))
        {
            StartCoroutine("Subindo");
        }

        if (Input.GetKeyDown(KeyCode.E) && estaDentroDaArea)
        {
            pegarTriggerAtivado = true;
            heroiAnim.SetTrigger("Pegar");
        }
    }

    void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, muzzleTransform.position, muzzleTransform.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.AddForce(muzzleTransform.forward * shootForce);
    }

    public void Pendurado(Transform alv)
    {
        if (estaPendurado) return;

        heroiAnim.SetTrigger("Hanging");
        GetComponent<Rigidbody>().isKinematic = true;
        estaPendurado = true;
        rootAlvo = alv;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other == areaPegarItemCollider)
        {
            estaDentroDaArea = true;
        }

        if (other.gameObject.CompareTag("caixa"))
        {
            heroiAnim.SetTrigger("Death");
            morto = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other == areaPegarItemCollider)
        {
            estaDentroDaArea = false;
        }
    }

    IEnumerator Subindo()
    {
        heroiAnim.SetTrigger("subindo");
        yield return new WaitForSeconds(3.24f);
        estaPendurado = false;
        gatilho = true;
        GetComponent<Rigidbody>().isKinematic = false;
    }

    private void OnAnimatorIK(int layerIndex)
    {
        heroiAnim.SetLookAtWeight(heroiAnim.GetFloat("IK_Val"));
        heroiAnim.SetLookAtPosition(item.position);

        if (pegarTriggerAtivado && heroiAnim.GetFloat("IK_Val") > 0.9f)
        {
            item.parent = mao;
            item.localPosition = new Vector3(-0.0003f, 0.1112f, 0.0518f);
            pegarTriggerAtivado = false; // Desativa o trigger após pegar o item
        }

        heroiAnim.SetIKPositionWeight(AvatarIKGoal.RightHand, heroiAnim.GetFloat("IK_Val"));
        heroiAnim.SetIKPosition(AvatarIKGoal.RightHand, item.position);
    }
}
