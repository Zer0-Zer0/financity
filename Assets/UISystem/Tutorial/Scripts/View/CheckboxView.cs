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

        [HideInInspector]
        public bool completed = false;

        protected override void Setup() { }

        protected override void Configure()
        {
            ObjectiveTextbox.SetText(ObjectiveText);
            Checkbox.gameObject.SetActive(false);
        }

        public void CompleteObjective()
        {
            completed = true;
            Checkbox.gameObject.SetActive(true);
        }

    }
}