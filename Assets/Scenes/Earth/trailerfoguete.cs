using UnityEngine;

public class VerticalAccelerator : MonoBehaviour
{
    public float acceleration = 5f; // Aceleração em unidades por segundo
    private float currentSpeed = 0f; // Velocidade atual do objeto
    public bool inanim = false;

    void Update()
    {

        if (inanim) {
                    // Aumenta a velocidade com base na aceleração
        currentSpeed += acceleration * Time.deltaTime;

        // Move o objeto para cima com a velocidade atual
        transform.Translate(Vector3.up * currentSpeed * Time.deltaTime);
        }
    }
}
