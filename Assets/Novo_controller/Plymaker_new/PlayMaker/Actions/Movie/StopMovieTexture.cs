#if !(UNITY_SWITCH || UNITY_TVOS || UNITY_IPHONE || UNITY_IOS || UNITY_ANDROID || UNITY_FLASH || UNITY_PS3 || UNITY_PS4 || UNITY_XBOXONE || UNITY_BLACKBERRY || UNITY_METRO || UNITY_WP8 || UNITY_PSM || UNITY_WEBGL || UNITY_SWITCH)

using UnityEngine;
using UnityEngine.Video; // Adicione esta linha

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Movie)]
    [Tooltip("Stops playing the Video Player, and rewinds it to the beginning.")]
    public class StopMovieTexture : FsmStateAction
    {
        [RequiredField]
        [ObjectType(typeof(VideoPlayer))]
        public FsmObject videoPlayer;

        public override void Reset()
        {
            videoPlayer = null;
        }

        public override void OnEnter()
        {
            var player = videoPlayer.Value as VideoPlayer;

            if (player != null)
            {
                player.Stop();
                player.time = 0f; // Rewind to the beginning
            }

            Finish();
        }
    }
}

#endif
