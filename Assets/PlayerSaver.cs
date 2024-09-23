using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSaver : MonoBehaviour
{
    [SerializeField] Transform Spawnpoint;
    Transform Player;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    public void SavePlayer()
    {
        Player.position = Spawnpoint.position;
    }
}
