using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAnime : MonoBehaviour
{
    [SerializeField] float intervalMax;
    [SerializeField] float intervalMin;
    [SerializeField] List<string> AnimeBoolTextList = new List<string>();
    void Start()
    {
        StartCoroutine(IntervalAnime());
    }
    IEnumerator IntervalAnime()
    {
        yield return new WaitForSeconds(Random.Range(intervalMax, intervalMin));
        string AnimeBoolText = AnimeBoolTextList[Random.Range(0, AnimeBoolTextList.Count-1)];
        Debug.Log(AnimeBoolText);
        GetComponent<Animator>().SetBool(AnimeBoolText, true);
        StartCoroutine(IntervalAnimeEnd(AnimeBoolText));
    }

    IEnumerator IntervalAnimeEnd(string AnimeBoolText)
    {
        yield return new WaitForSeconds(0.1f);
        GetComponent<Animator>().SetBool(AnimeBoolText, false);
    }
}
