using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    [HideInInspector] public Image LevelUpMoneyMeter_Image;
    [SerializeField] Text LevelUpMoney_Text;
    public int money;
    Text text;
    [HideInInspector] public int textNum;
    int pulsmoney;
    void Start()
    {
        text = GetComponent<Text>();
    }
    void Update()
    {
        text.text = textNum.ToString("N0");
        LevelUpMoneyMeter_Image.fillAmount =  textNum / float.Parse(LevelUpMoney_Text.text);
        if (money > textNum)
        {
            textNum += pulsmoney;
        }
        else
        {
            textNum = money;
            text.text = money.ToString("N0");
        }
    }
    public void AddMoney(int MoneyInt)
    {
        money += MoneyInt;
        pulsmoney = MoneyInt/60+1;
    }
}
