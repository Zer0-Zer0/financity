using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Linq;

public class RankedListManager : MonoBehaviour
{
    private string username = "V1ctorroff";
    private string token = "fc2221eabfb8ecccabbaa7fa0c36fbb06da43a13";

    private List<string> rankedList = new List<string>();

    private void Start()
    {
        StartCoroutine(GetRankedList());
    }

    private IEnumerator GetRankedList()
    {
        using (UnityWebRequest request = UnityWebRequest.Get($"https://www.pythonanywhere.com/api/v0/user/{username}/files/path/home/V1ctorroff/Ranked.txt"))
        {
            request.SetRequestHeader("Authorization", $"Token {token}");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string[] lines = request.downloadHandler.text.Split('\n');
                rankedList = lines.Where(line => !string.IsNullOrEmpty(line.Trim())).Select(line => line.Trim().Substring(4)).ToList();
            }
            else
            {
                Debug.LogError($"Erro ao obter o conteúdo: {request.responseCode}");
            }
        }
    }

    private void UpdateRankedList(string newPerson, string newValue)
    {
        string newEntry = $"{newPerson} R${newValue}";
        if (!rankedList.Contains(newEntry))
        {
            rankedList.Add(newEntry);
        }
        rankedList.Sort((x, y) => ExtractValue(y).CompareTo(ExtractValue(x)));
        if (rankedList.Count > 9)
        {
            rankedList = rankedList.GetRange(0, 9);
        }

        StartCoroutine(PostRankedList());
    }

    private float ExtractValue(string line)
    {
        string[] parts = line.Split(new string[] { "R$" }, System.StringSplitOptions.None);
        if (parts.Length == 2 && float.TryParse(parts[1].Trim(), out float result))
        {
            return result;
        }
        return 0.0f;
    }

    private IEnumerator PostRankedList()
    {
        string rankedContent = string.Join("\n", rankedList);
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("content", rankedContent));

        using (UnityWebRequest request = UnityWebRequest.Post($"https://www.pythonanywhere.com/api/v0/user/{username}/files/path/home/V1ctorroff/Ranked.txt", formData))
        {
            request.SetRequestHeader("Authorization", $"Token {token}");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Rank atualizado e enviado com sucesso!");
            }
            else
            {
                Debug.LogError($"Falha ao enviar o novo rank: {request.responseCode}");
            }
        }
    }

    private void CheckAndUpdateRankedList(string name, string value)
    {
        if (rankedList.Count == 9)
        {
            Debug.Log("O rank está cheio, vamos substituir.");
        }

        UpdateRankedList(name, value);
    }

    private void PrintRankedList()
    {
        foreach (string line in rankedList)
        {
            Debug.Log(line);
        }
    }
}
