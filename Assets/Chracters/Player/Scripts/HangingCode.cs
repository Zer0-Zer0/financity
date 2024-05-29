using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangingCode : MonoBehaviour
{

    public GameObject rootP;

    void OnTriggerEnter (Collider col ){

            if (col.gameObject.CompareTag("mao")){

                col.GetComponentInParent<Player> ().Pendurado(rootP.transform);
            }
    }

}
