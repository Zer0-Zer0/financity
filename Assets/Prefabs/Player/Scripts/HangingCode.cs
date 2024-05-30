using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangingCode : MonoBehaviour
{
    public GameObject rootPoint;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("hand"))
        {
            col.GetComponentInParent<Player>().Hang(rootPoint.transform);
        }
    }
}