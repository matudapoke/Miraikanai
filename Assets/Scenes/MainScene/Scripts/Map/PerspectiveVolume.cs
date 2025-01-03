using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerspecitveVolume : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] Transform ListenerTrs;
    [SerializeField] float PlusVolume;
    public float Coeffcient;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        float dis = Vector3.Distance(transform.position, ListenerTrs.position);
        audioSource.volume = Coeffcient * dis *dis + PlusVolume;
        audioSource.panStereo = (ListenerTrs.position.x - transform.position.x) * 0.05f;
    }
}
