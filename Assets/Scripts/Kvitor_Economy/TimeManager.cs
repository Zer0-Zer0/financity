using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] public float dayDuration = 60f;
    [SerializeField] float initialTime = 0f;
    public float time;
    void Start()
    {
        time = initialTime;    
    }
    void Update()
    {
        time += Time.deltaTime / dayDuration;
    }
}