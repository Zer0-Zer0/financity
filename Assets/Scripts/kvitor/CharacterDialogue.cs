using System;
using UnityEngine;
using TMPro;

public class CharacterDialogue : MonoBehaviour
{
    public string characterName;
    public AudioClip[] audioClips;
    public AudioSource secondAudioSource; 
    public TextMeshProUGUI subtitleText; // Adiciona uma referência ao TextMeshPro para legendas
    public SubtitleData[] subtitleData; // Array de dados de legendas

    [Serializable]
    public struct SubtitleData
    {
        public int clipId;
        public SubtitleEntry[] subtitles;
    }

    [Serializable]
    public struct SubtitleEntry
    {
        public string text;
        public float startTime;
        public float duration;
    }

    public void Speak(int id)
    {
        if (id >= 0 && id < audioClips.Length)
        {
            AudioClip audioClip = audioClips[id];
            secondAudioSource.clip = audioClip;
            secondAudioSource.Play();
            Debug.Log(characterName + " está falando: " + audioClip.name);
            StartCoroutine(ShowSubtitles(id));
        }
        else
        {
            Debug.LogWarning("ID de áudio inválido para " + characterName);
        }
    }

    private System.Collections.IEnumerator ShowSubtitles(int clipId)
    {
        SubtitleData subtitle = Array.Find(subtitleData, s => s.clipId == clipId);
        if (subtitle.subtitles == null) yield break;

        foreach (var entry in subtitle.subtitles)
        {
            subtitleText.text = entry.text;
            yield return new WaitForSeconds(entry.startTime);
            subtitleText.text = "";
            yield return new WaitForSeconds(entry.duration - entry.startTime);
        }
    }
}
