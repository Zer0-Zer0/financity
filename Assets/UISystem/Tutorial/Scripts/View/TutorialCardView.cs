using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace UISystem
{
    public class TutorialCardView : CustomUIComponent
    {
        public CheckboxView[] Checkboxes;
        public float timeoutTime = 5f;
        public UnityEvent OnCardDone = new UnityEvent();
        bool IsComplete = false;

        protected override void Setup() { }

        protected override void Configure() { }

        private bool VerifyTutorialCompletion()
        {
            bool IsCompleted = true;
            foreach (var Checkbox in Checkboxes)
                if (Checkbox.completed == false)
                    IsCompleted = false;
            return IsCompleted;
        }

        IEnumerator DisableAfterTimeout()
        {
            IsComplete = true;
            yield return new WaitForSecondsRealtime(timeoutTime);
            OnCardDone?.Invoke();
            gameObject.SetActive(false);
        }

        public void CheckCompletion()
        {
            if (IsComplete)
                return;

            IsComplete = VerifyTutorialCompletion();

            if (IsComplete)
                StartCoroutine(DisableAfterTimeout());
        }

        void Update()
        {
            CheckCompletion();
        }
    }
}