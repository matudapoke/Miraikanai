using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GomiCounter : MonoBehaviour
{
    Text Text;
    AudioSource AudioSource;
    void Start()
    {
        Text = GetComponent<Text>();
        AudioSource = GetComponent<AudioSource>();
    }
    public void AddGomi()
    {
        Text.text = (int.Parse(Text.text)+1).ToString();
        AudioSource.Play();
    }
}
