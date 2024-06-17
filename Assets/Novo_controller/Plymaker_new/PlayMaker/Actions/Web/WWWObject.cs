#if !(UNITY_SWITCH || UNITY_TVOS || UNITY_IPHONE || UNITY_IOS || UNITY_ANDROID || UNITY_FLASH || UNITY_PS3 || UNITY_PS4 || UNITY_XBOXONE || UNITY_BLACKBERRY || UNITY_WP8 || UNITY_PSM || UNITY_WEBGL || UNITY_SWITCH)

using UnityEngine;
using UnityEngine.Networking;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("WWW")]
    [Tooltip("Gets data from a URL and stores it in variables.")]
    public class WWWObject : FsmStateAction
    {
        [RequiredField]
        [Tooltip("URL to download data from.")]
        public FsmString url;

        [ActionSection("Results")]
        [UIHint(UIHint.Variable)]
        [Tooltip("Gets text from the URL.")]
        public FsmString storeText;

        [UIHint(UIHint.Variable)]
        [Tooltip("Gets a Texture from the URL.")]
        public FsmTexture storeTexture;

        [UIHint(UIHint.Variable)]
        [Tooltip("Error message if there was an error during the download.")]
        public FsmString errorString;

        [UIHint(UIHint.Variable)]
        [Tooltip("How far the download progressed (0-1).")]
        public FsmFloat progress;

        [ActionSection("Events")]
        [Tooltip("Event to send when the data has finished loading (progress = 1).")]
        public FsmEvent isDone;

        [Tooltip("Event to send if there was an error.")]
        public FsmEvent isError;

        private UnityWebRequest webRequest;

        public override void Reset()
        {
            url = null;
            storeText = null;
            storeTexture = null;
            errorString = null;
            progress = null;
            isDone = null;
            isError = null;
        }

        public override void OnEnter()
        {
            if (string.IsNullOrEmpty(url.Value))
            {
                Finish();
                return;
            }

            webRequest = UnityWebRequest.Get(url.Value);
            webRequest.SendWebRequest();
        }

        public override void OnUpdate()
        {
            if (webRequest == null)
            {
                errorString.Value = "UnityWebRequest is Null!";
                Finish();
                return;
            }

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                errorString.Value = webRequest.error;
                Finish();
                Fsm.Event(isError);
                return;
            }

            progress.Value = webRequest.downloadProgress;

            if (webRequest.downloadProgress >= 1f && webRequest.result != UnityWebRequest.Result.ConnectionError && webRequest.result != UnityWebRequest.Result.ProtocolError)
            {
                storeText.Value = webRequest.downloadHandler.text;
                storeTexture.Value = DownloadHandlerTexture.GetContent(webRequest);

                errorString.Value = webRequest.error;

                Fsm.Event(string.IsNullOrEmpty(errorString.Value) ? isDone : isError);

                Finish();
            }
        }

        public override void OnExit()
        {
            if (webRequest != null)
            {
                webRequest.Dispose();
            }
        }
    }
}

#endif
