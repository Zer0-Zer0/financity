using System;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using TMPro;

public class YourClassName : MonoBehaviour
{
    private string username = "V1ctorroff";
    private string token = "fc2221eabfb8ecccabbaa7fa0c36fbb06da43a13";
    public string filePath = "/home/V1ctorroff/Ranked.txt";
    public TMP_Text displayText;
    public float delay = 5f;

    public void Init()
    {
        gameObject.SetActive(true);
        displayText.text = "Recebendo do servidor...";
        StartCoroutine(GetFileAfterDelay(delay));
    }

    IEnumerator GetFileAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        string url = "https://www.pythonanywhere.com/api/v0/user/" + username + "/files/path" + filePath;
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            www.SetRequestHeader("Authorization", "Token " + token);

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                displayText.text = www.downloadHandler.text;
            }
            else
            {
                displayText.text = "Erro ao buscar " + www.error;
            }
        }
    }
}
