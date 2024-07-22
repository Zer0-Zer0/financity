// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;
using UnityEngine.UI; // Adicione este namespace

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.GUIElement)]
    [Tooltip("Sets the Text used by the Text Component attached to a Game Object.")]
    public class SetGUIText : ComponentAction<Text> // Troque GUIText por Text
    {
        [RequiredField]
        [CheckForComponent(typeof(Text))] // Troque GUIText por Text
        public FsmOwnerDefault gameObject;

        [UIHint(UIHint.TextArea)]
        public FsmString text;

        public bool everyFrame;

        public override void Reset()
        {
            gameObject = null;
            text = "";
        }

        public override void OnEnter()
        {
            DoSetGUIText();

            if (!everyFrame)
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {
            DoSetGUIText();
        }

        void DoSetGUIText()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (UpdateCache(go))
            {
                cachedComponent.text = text.Value; // Troque guiText por cachedComponent
            }
        }
    }
}
