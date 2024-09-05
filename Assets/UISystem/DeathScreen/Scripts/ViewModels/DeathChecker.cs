using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DeathChecker : MonoBehaviour
{
    [SerializeField] UnityEvent OnDeath;
    float health;
    bool canDie = false;
    IEnumerator Start()
    {
        health = DataManager.playerData.GetCurrentHealth();
        yield return 1f;
        canDie = true;
    }

    public void CheckAlive()
    {
        if (!canDie) return;

        health = DataManager.playerData.GetCurrentHealth();
        if (health <= 0)
        {
            Debug.Log("Ded");
            OnDeath?.Invoke();
        }
    }
}
