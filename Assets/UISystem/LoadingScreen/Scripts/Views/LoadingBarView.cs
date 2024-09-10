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
            DataManager.ClearPlayerData();
            StartCoroutine(LoadLevel());
            Canvas.SetActive(true);
            inspiringQuoteText.SetText(inspiringQuotes.GetRandomQuote());
            Cursor.visible = false;
        }

        IEnumerator LoadLevel()
        {
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("SpaceSafezone");
            while (!asyncOperation.isDone)
            {
                LoadingBar.value = asyncOperation.progress;
                yield return null;
            }
        }
    }
}