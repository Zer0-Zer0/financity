using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;

public static class Waiters
{
    public static IEnumerator InputWaiter(KeyCode key)
    {
        while (!Input.GetKeyDown(key))
        {
            yield return null; // Wait for the next frame update
        }
    }
}