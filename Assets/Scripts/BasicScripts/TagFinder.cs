using System;
using UnityEngine;

public class TagFinder
{
    public static GameObject FindObjectWithTag(string tag)
    {
        GameObject _result = GameObject.FindGameObjectWithTag(tag);

        if (_result == null)
        {
            throw new Exception("Error: Game object with tag '" + tag + "' not found! add it to the scene.");
        }
        return _result;
    }
}