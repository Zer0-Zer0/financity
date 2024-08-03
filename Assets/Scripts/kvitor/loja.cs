using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class Activator : MonoBehaviour
{
    public GameObject[] rawImageObjects;
    public GameObject[] textMeshObjects;
    public ButtonValue[] buttons;

    public TextMeshProUGUI totalText;
    public Button payButton; // Botão de pagamento

    private int totalValue = 0;
    private List<PurchasedItem> purchasedItems = new List<PurchasedItem>();

    void Start()
    {
        Debug.Log("Activator script started.");
        DeactivateAllObjects();
        payButton.onClick.AddListener(Pay); // Adiciona o listener para o botão de pagamento
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered trigger area.");
            ActivateObjects(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited trigger area.");
            ActivateObjects(false);
        }
    }

    private void DeactivateAllObjects()
    {
        foreach (GameObject rawImageObject in rawImageObjects)
        {
            if (rawImageObject != null && rawImageObject.GetComponent<RawImage>() != null)
            {
                rawImageObject.SetActive(false);
            }
        }

        foreach (GameObject textMeshObject in textMeshObjects)
        {
            if (textMeshObject != null && textMeshObject.GetComponent<TextMeshProUGUI>() != null)
            {
                textMeshObject.SetActive(false);
            }
        }

        foreach (ButtonValue buttonValue in buttons)
        {
            if (buttonValue.button != null)
            {
                buttonValue.button.gameObject.SetActive(false);
                buttonValue.button.onClick.RemoveAllListeners();
            }
        }

        totalValue = 0;
        totalText.text = "Total: " + totalValue;
    }

    private void ActivateObjects(bool activate)
    {
        foreach (GameObject rawImageObject in rawImageObjects)
        {
            rawImageObject.SetActive(activate);
        }

        foreach (GameObject textMeshObject in textMeshObjects)
        {
            textMeshObject.SetActive(activate);
        }

        foreach (ButtonValue buttonValue in buttons)
        {
            buttonValue.button.gameObject.SetActive(activate);
            if (activate)
            {
                AddListenerToButton(buttonValue);
            }
            else
            {
                buttonValue.button.onClick.RemoveAllListeners();
            }
        }
    }

    private void AddListenerToButton(ButtonValue buttonValue)
    {
        buttonValue.button.onClick.AddListener(() => OnButtonClicked(buttonValue.id, buttonValue.value));
    }

    private void OnButtonClicked(int id, int value)
    {
        totalValue += value;
        purchasedItems.Add(new PurchasedItem { id = id, value = value });
        totalText.text = "Total: " + totalValue;
    }

    public void Pay()
    {
        foreach (var item in purchasedItems)
        {
            Debug.Log($"Purchased item: ID={item.id}, Value={item.value}");
        }

        purchasedItems.Clear();
        totalValue = 0;
        totalText.text = "Total: " + totalValue;
    }
}

[System.Serializable]
public class ButtonValue
{
    public Button button;
    public int id;
    public int value;
}

public class PurchasedItem
{
    public int id;
    public int value;
}
