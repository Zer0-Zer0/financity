using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UISystem
{
    public class QuestSystemView : CustomUIComponent
    {
        [SerializeField] Text _otherObjectives;

        private List<string> _objectives;

        protected override void Setup() { }

        protected override void Configure()
        {
            ClearObjectives();
        }

        public void SetObjective(string objective)
        {
            string formattedObjective = $"-{objective}.\n";
            _objectives.Add(formattedObjective);
            UpdateGraphics();
        }

        void UpdateGraphics()
        {
            string _textToSet = "";
            foreach (var objective in _objectives)
                _textToSet += objective;
            _otherObjectives.SetText(_textToSet);
        }

        public void ClearObjectives()
        {
            _objectives.Clear();
            _otherObjectives.SetText(String.Empty);
        }
    }
}