using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem
{
    public class CheckboxView : CustomUIComponent
    {
        [Header("Values")]
        [SerializeField] string ObjectiveText;

        [Header("Components")]
        [SerializeField] Text ObjectiveTextbox;
        [SerializeField] Image Checkbox;

        protected override void Setup() { }

        protected override void Configure()
        {
            ObjectiveTextbox.SetText(ObjectiveText);
            Checkbox.gameObject.SetActive(false);
        }

        public void CompleteObjective()
        {
            Checkbox.gameObject.SetActive(true);
        }

    }
}