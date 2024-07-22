using UnityEngine;
using UnityEngine.UI; // Adicione este namespace

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.GUIElement)]
    [Tooltip("Sets the Texture used by the RawImage attached to a Game Object.")]
    public class SetGUITexture : ComponentAction<RawImage> // Troque GUITexture por RawImage
    {
        [RequiredField]
        [CheckForComponent(typeof(RawImage))] // Troque GUITexture por RawImage
        [Tooltip("The GameObject that owns the RawImage.")]
        public FsmOwnerDefault gameObject;

        [Tooltip("Texture to apply.")]
        public FsmTexture texture;

        public override void Reset()
        {
            gameObject = null;
            texture = null;
        }

        public override void OnEnter()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (UpdateCache(go))
            {
                cachedComponent.texture = texture.Value; // Troque guiTexture por cachedComponent
            }

            Finish();
        }
    }
}
