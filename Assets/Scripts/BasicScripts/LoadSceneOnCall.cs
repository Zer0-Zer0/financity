using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnCall : MonoBehaviour
{
    public IEnumerator LoadSceneSave(string scene)
    {
        yield return DataManager.SavePlayerData(DataManager.playerData);

        SceneManager.LoadScene(scene);
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }


    public void LoadSceneClear(string scene)
    {
        DataManager.ClearPlayerData();
        SceneManager.LoadScene(scene);
    }
}
