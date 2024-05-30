using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 2000f;
    public float lifeTime = 2f;
    public GameObject Flash;
    public float flashLifeTime = 0.016f;
    void Start()
    {
        Destroy(gameObject, lifeTime);
        Destroy(gameObject, 0.016f);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Aqui você pode adicionar lógica para o que acontece quando o projétil colide com algo
        Destroy(gameObject);
    }
}
