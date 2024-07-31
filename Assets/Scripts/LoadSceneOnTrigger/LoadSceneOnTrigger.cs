using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LoadSceneOnTrigger : MonoBehaviour
{
    [SerializeField]
    private string _sceneName;
    private bool _isPlayerInRange = false;

    public UnityEvent PlayerIsInRange;
    public UnityEvent PlayerOutOfRange;

    void Update()
    {
        if (_isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            DataManager.SavePlayerData(DataManager.playerData);
            SceneManager.LoadScene(_sceneName);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _isPlayerInRange = true;
            PlayerIsInRange?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _isPlayerInRange = false;
            PlayerOutOfRange?.Invoke();
        }
    }
}
