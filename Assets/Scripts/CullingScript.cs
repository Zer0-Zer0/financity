using UnityEngine;

public class CullingScript : MonoBehaviour
{
    public Transform player;
    public float distanciaculling = 1000f; //recomendada pro mapa 

    void Update()
    {
        foreach (Transform child in transform)
        {
            float distanciajogador = Vector3.Distance(child.position, player.position); // geta a posição do personagem em vector3

            if (distanciajogador > distanciaculling) // se longe desativa
            {
                child.gameObject.SetActive(false);
            }
            else
            {
                child.gameObject.SetActive(true);
            }
        }
    }
}
