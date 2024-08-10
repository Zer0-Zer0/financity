using System;
using UnityEngine;

public class RandomEnum : MonoBehaviour
{
    public static T GetRandomEnumValue<T>()
    {
        // Get all values of the enum
        Array values = System.Enum.GetValues(typeof(T));

        // Select a random index
        int randomIndex = UnityEngine.Random.Range(0, values.Length);

        // Return the random enum value
        return (T)values.GetValue(randomIndex);
    }
}