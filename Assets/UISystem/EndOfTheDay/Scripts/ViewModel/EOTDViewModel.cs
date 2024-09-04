using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UISystem;

public class EOTDViewModel : MonoBehaviour
{
    [SerializeField] EOTDView GetEOTDView;

    public void OnEndOfTheDayEvent(Component sender, object data)
    {
        GetEOTDView.FirstPanel.SetActive(true);
        GetEOTDView.ClearText();
        GetEOTDView.SetData(DataManager.playerData.GetCurrentWallet());
    }
}