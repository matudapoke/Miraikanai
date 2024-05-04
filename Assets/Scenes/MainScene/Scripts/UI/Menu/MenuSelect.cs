using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSelect : MonoBehaviour
{
    Image image;
    [SerializeField] Sprite SelectedSprite;
    Sprite OriginalSprite;
    bool Selected;
    [SerializeField] Vector3 SelectedObj_Position;
    [SerializeField] GameObject UpMenu;
    [SerializeField] GameObject DownMenu;
    [SerializeField] GameObject RightMenu;
    [SerializeField] GameObject LeftMenu;
    Coroutine coroutine;

    void Start()
    {
        image = GetComponent<Image>();
        OriginalSprite = image.sprite;
    }

    void Update()
    {
        if (Selected)
        {
            if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && UpMenu != null)
            {
                UpMenu.GetComponent<MenuSelect>().Select();
                DeSelection();
            }
            if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && DownMenu != null)
            {
                DownMenu.GetComponent<MenuSelect>().Select();
                DeSelection();
            }
            if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && RightMenu != null)
            {
                RightMenu.GetComponent<MenuSelect>().Select();
                DeSelection();
            }
            if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && LeftMenu != null)
            {
                LeftMenu.GetComponent<MenuSelect>().Select();
                DeSelection();
            }
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            DeSelection();
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
            }
        }
    }

    public void Select()
    {
        OriginalSprite = image.sprite;
        image.sprite = SelectedSprite;
        coroutine = StartCoroutine(SelectedSetBool(true));
        if (!Input.GetKeyDown(KeyCode.Tab))
        {
            GetComponent<AudioSource>().Play();
        }
    }
    IEnumerator SelectedSetBool(bool Value)
    {
        yield return new WaitForSeconds(0.15f);
        Selected = Value;
    } 
    public void DeSelection()
    {
        Selected = false;
        image.sprite = OriginalSprite;
    }
}
