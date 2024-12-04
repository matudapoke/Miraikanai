using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RandomSound : MonoBehaviour
{
    public bool CanPlay;
    AudioSource audioSource;
    [SerializeField] List<AudioClip> Sounds = new List<AudioClip>();
    [SerializeField] float SoundIntervalMin;
    [SerializeField] float SoundIntervalMax;
    [SerializeField] float SoundVolumeMin;
    [SerializeField] float SoundVolumeMax;
    [SerializeField] GameObject SpawnPrefab;
    [SerializeField] float SpawnSoundVolumeMin;
    
    void Start()
    {
        audioSource = GetComponents<AudioSource>()[1];
        StartCoroutine(RandomInterval());
    }

    IEnumerator RandomInterval()
    {
        yield return new WaitForSeconds(Random.Range(SoundIntervalMin, SoundIntervalMax));
        if (CanPlay)
        {
            float SoundVolume = Random.Range(SoundVolumeMin, SoundVolumeMax);
            audioSource.PlayOneShot(Sounds[Random.Range(0, Sounds.Count - 1)], SoundVolume);
            if (SpawnPrefab != null)
            {
                if (SoundVolume >= SpawnSoundVolumeMin)
                {
                    Instantiate(SpawnPrefab);
                }
            }
        }
        StartCoroutine(RandomInterval());
    }

}
