/*using System;
using UnityEngine;

public class CharacterDialogue : MonoBehaviour
{
    public string characterName;
    public AudioClip[] audioClips;
    public AudioSource secondAudioSource; 

    public void Speak(int id)
    {
        if (id >= 0 && id < audioClips.Length)
        {
            AudioClip audioClip = audioClips[id];
            secondAudioSource.clip = audioClip;
            secondAudioSource.Play();
            Debug.Log(characterName + " está falando: " + audioClip.name);
        }
        else
        {
            Debug.LogWarning("ID de áudio inválido para " + characterName);
        }
    }

    internal float GetClipLength(int v)
    {
        throw new NotImplementedException();
    }
}*/
