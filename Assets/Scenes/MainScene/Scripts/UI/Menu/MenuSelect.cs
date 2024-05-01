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

    void Start()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        if (Selected)
        {
            if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && UpMenu != null)
            {
                UpMenu.GetComponent<MenuSelect>().Select();
                DeSelection();
            }
            if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) && DownMenu != null)
            {
                DownMenu.GetComponent<MenuSelect>().Select();
                DeSelection();
                Debug.Log(gameObject.name);
            }
            if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && RightMenu != null)
            {
                RightMenu.GetComponent<MenuSelect>().Select();
                DeSelection();
            }
            if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && LeftMenu != null)
            {
                LeftMenu.GetComponent<MenuSelect>().Select();
                DeSelection();
            }
        }
    }

    public void Select()
    {
        Selected = true;
        OriginalSprite = image.sprite;
        image.sprite = SelectedSprite;
    }
    public void DeSelection()
    {
        Selected = false;
        image.sprite = OriginalSprite;
    }
}
