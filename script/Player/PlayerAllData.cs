using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAllData : MonoBehaviour
{
    public int Lv = 1;
    public int Money = 500;
    public int Exp = 0;
    public int Hp = 1000;
    public int Attack = 10;
    public int Defense = 10;

    public int YellowKey = 0;
    public int BlueKey = 0;
    public int RedKey = 0;
    public static PlayerAllData Ins;



    public Text LvText;
    public Text MoneyText;
    public Text ExpText;
    public Text HpText;
    public Text AttackText;
    public Text DefenseText;
    public Text YellowKeyText;
    public Text BlueKeyText;
    public Text RedKeyText;
    void Start()
    {
        Ins = this;
        GameObject.DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
       
        
        
        
       
        
       
    }
    public  void changePlayerData(int count,string changeDataName)
    {
        if (changeDataName == "Hp")
        {
            Hp += count;
            HpText.text = Hp.ToString();
        }
        if (changeDataName == "Attack")
        {
            Attack += count;
            AttackText.text = Attack.ToString();
        }
        if (changeDataName == "Defense")
        {
            Defense += count;
            DefenseText.text = Defense.ToString();
        }
        if (changeDataName == "Money")
        {
            if (Money + count >= 0)
            {
                Money += count;
                MoneyText.text = Money.ToString();
            }
        }
        if (changeDataName == "Exp")
        {
            Exp += count;
            ExpText.text = Exp.ToString();
        }
        if (changeDataName == "Lv")
        {
            Lv += count;
            Attack += count * 12;
            Defense += count * 10;
            Hp += count * 750;
            LvText.text = Lv.ToString();
            AttackText.text = Attack.ToString();
            HpText.text = Hp.ToString();
            DefenseText.text = Defense.ToString();
        }
        if (changeDataName == "YellowKey")
        {
            if (YellowKey + count >= 0)
            {
                YellowKey += count;
                YellowKeyText.text = YellowKey.ToString();
            }
            
        }
        if (changeDataName == "BlueKey")
        {
            if (BlueKey + count >= 0)
            {
                BlueKey += count;
                BlueKeyText.text = BlueKey.ToString();
            }
           
        }
        if (changeDataName == "RedKey")
        {
            if (RedKey + count >= 0)
            {
                RedKey += count;
                RedKeyText.text = RedKey.ToString();
            }
            
        }
    }
    public bool canDestroyProp(int count,string name)
    {
        if (((name == "RedKey") && RedKey + count >= 0)|| ((name == "BlueKey") && BlueKey + count >= 0)|| ((name == "YellowKey") && YellowKey + count >= 0))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
