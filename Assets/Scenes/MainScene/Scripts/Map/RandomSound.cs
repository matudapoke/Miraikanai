using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RandomSound : MonoBehaviour
{
    AudioSource audioSource;
    AudioSource audioSource1;
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
        audioSource1 = GetComponents<AudioSource>()[1];
        StartCoroutine(RandomInterval());
    }

    IEnumerator RandomInterval()
    {
        yield return new WaitForSeconds(Random.Range(SoundIntervalMin, SoundIntervalMax));
        float SoundVolume = Random.Range(SoundVolumeMin, SoundIntervalMax);
        audioSource1.PlayOneShot(Sounds[Random.Range(0, Sounds.Count - 1)], SoundVolume);
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
