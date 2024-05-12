using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] Transform ListenerTrs;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        float dis = Vector3.Distance(this.transform.position, ListenerTrs.position);
        audioSource.volume = 0.005f*dis*dis+0.1f;
    }
}
