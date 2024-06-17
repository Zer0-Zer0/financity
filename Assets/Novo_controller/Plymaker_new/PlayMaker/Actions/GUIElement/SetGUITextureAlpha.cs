using UnityEngine;
using UnityEngine.UI; // Adicione este namespace
using System;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.GUIElement)]
    [Tooltip("Sets the Alpha of the RawImage attached to a Game Object. Useful for fading GUI elements in/out.")]
    public class SetGUITextureAlpha : ComponentAction<RawImage> // Troque GUITexture por RawImage
    {
        [RequiredField]
        [CheckForComponent(typeof(RawImage))] // Troque GUITexture por RawImage
        public FsmOwnerDefault gameObject;
        
        [RequiredField]
        public FsmFloat alpha;
        
        public bool everyFrame;
        
        public override void Reset()
        {
            gameObject = null;
            alpha = 1.0f;
            everyFrame = false;
        }

        public override void OnEnter()
        {
            DoGUITextureAlpha();
            
            if(!everyFrame)
            {
                Finish();
            }
        }
        
        public override void OnUpdate()
        {
            DoGUITextureAlpha();
        }
        
        void DoGUITextureAlpha()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (UpdateCache(go))
            {
                var color = cachedComponent.color;
                cachedComponent.color = new Color(color.r, color.g, color.b, alpha.Value); // Troque guiTexture por cachedComponent
            }            
        }
    }
}
