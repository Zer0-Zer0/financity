using UnityEngine;

namespace UISystem
{
    [CreateAssetMenu(fileName = "InspiringQuotes", menuName = "ScriptableObjects/InspiringQuotes", order = 1)]
    public class InspiringQuotesSO : ScriptableObject
    {
        [SerializeField]
        private string[] quotes;

        public string[] Quotes => quotes;

        public string GetRandomQuote()
        {
            if (quotes == null || quotes.Length == 0)
            {
                return "No quotes available.";
            }

            int randomIndex = Random.Range(0, quotes.Length);
            return quotes[randomIndex];
        }
    }
}