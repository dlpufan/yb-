using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpOldMan : MonoBehaviour
{
    public int addAttackCount = 7;
    public int addDefenseCount = 7;
    public int addLvCount = 1;

    public int[] costExp = { 100, 30, 30 };

    public GameObject cantMoney;

    public void addAttack()
    {
        if (PlayerAllData.Ins.Exp >= costExp[1])
        {
            PlayerAllData.Ins.changePlayerData(addAttackCount, "Attack");
            PlayerAllData.Ins.changePlayerData(-costExp[1], "Exp");
        }
        else
        {
            Instantiate(cantMoney);
        }
    }
    public void addDefense()
    {
        if (PlayerAllData.Ins.Exp >= costExp[2])
        {
            PlayerAllData.Ins.changePlayerData(addDefenseCount, "Defense");
            PlayerAllData.Ins.changePlayerData(-costExp[2], "Exp");
        }
        else
        {
            Instantiate(cantMoney);
        }

    }
    public void addLv()
    {
        if (PlayerAllData.Ins.Exp >= costExp[0])
        {
            PlayerAllData.Ins.changePlayerData(addLvCount, "Lv");
            PlayerAllData.Ins.changePlayerData(-costExp[0], "Exp");
        }
        else
        {
            Instantiate(cantMoney);
        }

    }
    private void Update()
    {
        
        
    }
    public void esc()
    {
        GameObject.Find("BornGameObject").GetComponent<GameManager>().phoneMove.SetActive(true);
        GameObject.Find("BornGameObject").GetComponent<GameManager>().ToSeeEnemyUi.SetActive(true);
        GameObject.Find("Player").GetComponent<PlayerMove>().enabled = true;
        gameObject.SetActive(false);

    }
}
