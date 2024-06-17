#if !(UNITY_SWITCH || UNITY_TVOS || UNITY_IPHONE || UNITY_IOS || UNITY_ANDROID || UNITY_FLASH || UNITY_PS3 || UNITY_PS4 || UNITY_XBOXONE || UNITY_BLACKBERRY || UNITY_WP8 || UNITY_PSM || UNITY_WEBGL || UNITY_SWITCH)

using UnityEngine;
using UnityEngine.Video; // Adicione esta linha

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Movie)]
    [Tooltip("Sets the Game Object as the Audio Source associated with the Video Player. The Game Object must have an AudioSource Component.")]
    public class MovieTextureAudioSettings : FsmStateAction
    {
        [RequiredField]
        [CheckForComponent(typeof(VideoPlayer))]
        public FsmGameObject gameObject;

        [RequiredField]
        [CheckForComponent(typeof(AudioSource))]
        public FsmGameObject audioSourceObject;

        public override void Reset()
        {
            gameObject = null;
            audioSourceObject = null;
        }

        public override void OnEnter()
        {
            var videoPlayer = gameObject.Value.GetComponent<VideoPlayer>();

            if (videoPlayer != null && audioSourceObject.Value != null)
            {
                var audio = audioSourceObject.Value.GetComponent<AudioSource>();
                if (audio != null)
                {
                    videoPlayer.SetTargetAudioSource(0, audio);
                }
            }

            Finish();
        }
    }
}

#endif
