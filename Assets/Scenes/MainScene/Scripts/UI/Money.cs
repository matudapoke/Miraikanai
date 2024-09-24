using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    int money;
    Text text;
    int textNum;
    void Start()
    {
        text = GetComponent<Text>();
    }
    void Update()
    {
        if (money > textNum)
        {
            textNum += 1;
            text.text = (textNum).ToString();
        }
        else
        {
            textNum = money;
            text.text = money.ToString();
        }
    }
    public void AddMoney(int MoneyInt)
    {
        money += MoneyInt;
    }
}
