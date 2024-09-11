using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UISystem
{
    public class LoadingBarView : MonoBehaviour
    {
        [SerializeField] Text inspiringQuoteText;
        [SerializeField] GameObject Canvas;
        [SerializeField] Slider LoadingBar;
        [SerializeField] InspiringQuotesSO inspiringQuotes;

        public void StartGame()
        {
            Debug.Log($"Save da barra de carregando\n{DataManager.playerData}");
            DataManager.SavePlayerData(DataManager.playerData);
            Canvas.SetActive(true);
            inspiringQuoteText.SetText(inspiringQuotes.GetRandomQuote());
            Cursor.visible = false;
            StartCoroutine(LoadLevel());
        }

        IEnumerator LoadLevel()
        {
            yield return null;
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("SpaceSafezone");
            while (!asyncOperation.isDone)
            {
                LoadingBar.value = asyncOperation.progress;
                yield return null;
            }
        }
    }
}