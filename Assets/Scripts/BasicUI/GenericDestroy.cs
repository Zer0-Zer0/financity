using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GenericDestroy : MonoBehaviour
{
    [SerializeField]
    private GameObject _target;
    [SerializeField]
    private float _time;
    [SerializeField]
    private UnityEvent TargetDestroyed;

    public void DestroyTarget()
    {
        Destroy(_target, _time);
        TargetDestroyed?.Invoke();
    }
}
