using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintSomethingOnEvent : MonoBehaviour
{
    public void PrintTheEvent(Component sender, object data)
    {
        if (data is not null)
            Debug.Log(data.ToString());
    }
}
