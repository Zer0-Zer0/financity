using UnityEngine;
using System.Collections;
using System.Linq;

public class RandomAudioPlayer : MonoBehaviour
{
    public AudioSource audioSource;      
    public AudioClipVolume[] audioClips;       
    public int numberOfAudiosToPlay = 3;   

    public Transform objetoMovimento;     

    public float minPauseDuration = 4f;   
    public float maxPauseDuration = 15f;   

    private Vector3 lastPosition;          
    private bool isPlaying = false;        

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        if (audioSource != null)
        {
            audioSource.spatialBlend = 1f;  
        }

        if (objetoMovimento != null)
        {
            lastPosition = objetoMovimento.position;
        }
    }

    void Update()
    {
        if (IsMoving())
        {
            if (isPlaying)
            {
                StopAllCoroutines();
                audioSource.Stop();
                isPlaying = false;
            }
        }
        else
        {
            if (!isPlaying)
            {
                StartCoroutine(PlayRandomAudios());
                isPlaying = true;
            }
        }
    }

    IEnumerator PlayRandomAudios()
    {
        var shuffledClips = audioClips.OrderBy(x => Random.value).Take(numberOfAudiosToPlay).ToArray();

        foreach (var clipVolume in shuffledClips)
        {
            if (clipVolume.clip != null)
            {
                audioSource.volume = clipVolume.volume;  
                audioSource.PlayOneShot(clipVolume.clip);
            }

            float pauseDuration = Random.Range(minPauseDuration, maxPauseDuration);
            yield return new WaitForSeconds(pauseDuration);
        }

        isPlaying = false;
    }

    bool IsMoving()
    {
        if (objetoMovimento != null && (objetoMovimento.position != lastPosition))
        {
            lastPosition = objetoMovimento.position;
            return true;
        }

        return false;
    }
}

[System.Serializable]
public struct AudioClipVolume
{
    public AudioClip clip;  
    public float volume;    
}
