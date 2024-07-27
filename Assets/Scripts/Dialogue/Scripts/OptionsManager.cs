using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class OptionsManager : MonoBehaviour
{
    public Button[] Buttons;
    public TMP_Text[] ButtonTexts;

    public UnityEvent AllListenersCleared;

    public void OnOptionsPopup(DialoguePhrase dialoguePhrase)
    {
        int index = 0;
        foreach (Button button in Buttons)
        {
            if (index + 1 > dialoguePhrase.Routes.Length)
            {
                button.gameObject.SetActive(false);
            }
            else
            {
                button.gameObject.SetActive(true);
                ButtonTexts[index].text = dialoguePhrase.Routes[index].Text;
                button.onClick.AddListener(dialoguePhrase.Routes[index].InvokeRoute);
                index++;
            }
        }
    }

    public void AssignToEveryButton(UnityAction call)
    {
        foreach (Button button in Buttons)
        {
            button.onClick.AddListener(call);
        }
    }

    public void ClearListenersFromEveryButton()
    {
        foreach (Button button in Buttons)
        {
            button.onClick.RemoveAllListeners();
        }
        AllListenersCleared?.Invoke();
    }

}