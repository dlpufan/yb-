using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public int addAttackCount = 5;
    public int addDefenseCount = 5;
    public int addHpCount = 300;

    public int costMoney = 25;

    public GameObject cantMoney;

    public void addAttack()
    {
        if (PlayerAllData.Ins.Money >= costMoney)
        {
            PlayerAllData.Ins.changePlayerData(addAttackCount, "Attack");
            PlayerAllData.Ins.changePlayerData(-costMoney, "Money");
        }
        else
        {
            Instantiate(cantMoney);
        }
    }
    public void addDefense()
    {
        if (PlayerAllData.Ins.Money >= costMoney)
        {
            PlayerAllData.Ins.changePlayerData(addDefenseCount, "Defense");
            PlayerAllData.Ins.changePlayerData(-costMoney, "Money");
        }
        else
        {
            Instantiate(cantMoney);
        }

    }
    public void addHp()
    {
        if (PlayerAllData.Ins.Money >= costMoney)
        {
            PlayerAllData.Ins.changePlayerData(addHpCount, "Hp");
            PlayerAllData.Ins.changePlayerData(-costMoney, "Money");
        }
        else
        {
            Instantiate(cantMoney);
        }

    }
    private void Update()
    {
        
        
    }
    public void falseGameobject()
    {
        GameObject.Find("BornGameObject").GetComponent<GameManager>().phoneMove.SetActive(true);
        GameObject.Find("BornGameObject").GetComponent<GameManager>().ToSeeEnemyUi.SetActive(true);
        GameObject.Find("Player").GetComponent<PlayerMove>().enabled = true;
        gameObject.SetActive(false);
    }
}
