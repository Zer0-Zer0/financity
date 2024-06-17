#if !(UNITY_SWITCH || UNITY_TVOS || UNITY_IPHONE || UNITY_IOS || UNITY_ANDROID || UNITY_FLASH || UNITY_PS3 || UNITY_PS4 || UNITY_XBOXONE || UNITY_BLACKBERRY || UNITY_METRO || UNITY_WP8 || UNITY_PSM || UNITY_WEBGL || UNITY_SWITCH)

using UnityEngine;
using UnityEngine.Video;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Material)]
    [Tooltip("Sets a named texture in a game object's material to a video texture.")]
    public class SetMaterialMovieTexture : ComponentAction<Renderer>
    {
        [Tooltip("The GameObject that the material is applied to.")]
        [CheckForComponent(typeof(Renderer))]
        public FsmOwnerDefault gameObject;

        [Tooltip("GameObjects can have multiple materials. Specify an index to target a specific material.")]
        public FsmInt materialIndex;

        [Tooltip("Alternatively specify a Material instead of a GameObject and Index.")]
        public FsmMaterial material;
        
        [UIHint(UIHint.NamedTexture)]
        [Tooltip("A named texture in the shader.")]
        public FsmString namedTexture;

        [RequiredField]
        [ObjectType(typeof(VideoClip))]
        public FsmObject videoClip;

        public override void Reset()
        {
            gameObject = null;
            materialIndex = 0;
            material = null;
            namedTexture = "_MainTex";
            videoClip = null;
        }

        public override void OnEnter()
        {
            DoSetMaterialTexture();
            Finish();
        }

        void DoSetMaterialTexture()
        {
            var videoClipValue = videoClip.Value as VideoClip;

            var namedTex = namedTexture.Value;
            if (string.IsNullOrEmpty(namedTex)) namedTex = "_MainTex";

            if (material.Value != null)
            {
                SetVideoTexture(material.Value, namedTex, videoClipValue);
                return;
            }

            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (!UpdateCache(go))
            {
                return;
            }

            if (renderer == null)
            {
                LogError("Missing Renderer!");
                return;
            }

            if (renderer.material == null)
            {
                LogError("Missing Material!");
                return;
            }

            if (materialIndex.Value == 0)
            {
                SetVideoTexture(renderer.material, namedTex, videoClipValue);
            }
            else if (renderer.materials.Length > materialIndex.Value)
            {
                var materials = renderer.materials;
                SetVideoTexture(materials[materialIndex.Value], namedTex, videoClipValue);
                renderer.materials = materials;
            }
        }

        void SetVideoTexture(Material material, string textureName, VideoClip videoClip)
        {
            var videoPlayerGO = new GameObject("VideoPlayer");
            var videoPlayer = videoPlayerGO.AddComponent<VideoPlayer>();
            videoPlayer.clip = videoClip;
            videoPlayer.renderMode = VideoRenderMode.MaterialOverride;
            videoPlayer.targetMaterialRenderer = renderer;
            videoPlayer.targetMaterialProperty = textureName;
            videoPlayer.Play();
        }
    }
}

#endif
