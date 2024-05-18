using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RandomSound : MonoBehaviour
{
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
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(RandomInterval());
    }

    IEnumerator RandomInterval()
    {
        yield return new WaitForSeconds(Random.Range(SoundIntervalMin, SoundIntervalMax));
        float SoundVolume = Random.Range(SoundVolumeMin, SoundIntervalMax);
        audioSource.PlayOneShot(Sounds[Random.Range(0, Sounds.Count - 1)], SoundVolume);
        StartCoroutine(RandomInterval());
        if (SpawnPrefab != null)
        {
            if (SoundVolume >= SpawnSoundVolumeMin)
            {
                Instantiate(SpawnPrefab);
            }
        }
    }

}
