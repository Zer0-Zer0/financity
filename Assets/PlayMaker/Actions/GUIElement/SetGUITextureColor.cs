using UnityEngine;
using UnityEngine.UI; // Adicione este namespace
using System;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.GUIElement)]
    [Tooltip("Sets the Color of the RawImage attached to a Game Object.")]
    public class SetGUITextureColor : ComponentAction<RawImage> // Troque GUITexture por RawImage
    {
        [RequiredField]
        [CheckForComponent(typeof(RawImage))] // Troque GUITexture por RawImage
        public FsmOwnerDefault gameObject;
        
        [RequiredField]
        public FsmColor color;
        
        public bool everyFrame;
        
        public override void Reset()
        {
            gameObject = null;
            color = Color.white;
            everyFrame = false;
        }

        public override void OnEnter()
        {
            DoSetGUITextureColor();

            if (!everyFrame)
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {
            DoSetGUITextureColor();
        }

        void DoSetGUITextureColor()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (UpdateCache(go))
            {
                cachedComponent.color = color.Value; // Troque guiTexture por cachedComponent
            }
        }
    }
}
