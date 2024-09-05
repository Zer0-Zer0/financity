using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DeathChecker : MonoBehaviour
{
    [SerializeField] UnityEvent OnDeath;
    public void CheckAlive()
    {
        float health = DataManager.playerData.GetCurrentHealth();
        if (health <= 0)
            OnDeath?.Invoke();
    }
}
