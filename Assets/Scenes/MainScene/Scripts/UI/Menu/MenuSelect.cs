using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSelect : MonoBehaviour
{
    RectTransform SelectedObj_RectTransform;
    [SerializeField] Vector3 SelectedObj_Position;
    [SerializeField] GameObject UpMenu;
    [SerializeField] GameObject DownMenu;
    [SerializeField] GameObject RightMenu;
    [SerializeField] GameObject LeftMenu;

    void Start()
    {
        SelectedObj_RectTransform = GameObject.Find("Selected").GetComponent<RectTransform>();
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && UpMenu != null)
        {
            UpMenu.GetComponent<MenuSelect>().Select();
        }
        if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) && DownMenu != null)
        {
            DownMenu.GetComponent<MenuSelect>().Select();
        }
        if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && RightMenu != null)
        {
            RightMenu.GetComponent<MenuSelect>().Select();
        }
        if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && LeftMenu != null)
        {
            LeftMenu.GetComponent<MenuSelect>().Select();
        }
    }

    public void Select()
    {
        SelectedObj_RectTransform.position = SelectedObj_Position;
    }
}
