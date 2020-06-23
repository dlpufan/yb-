using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetEnemyData : MonoBehaviour
{
    public List<Transform> enemyChild;

    public GameObject enemyUi;

    void Awake()
    {


        GameObject Enemy = GameObject.Find("Enemy");

        foreach (Transform obj in Enemy.transform)
        {
            if (isstringEqual(obj))
            {
                enemyChild.Add(obj);
            }
        }
        GameObject linshi;
        for (int i = 0; i < enemyChild.ToArray().Length; i++)
        {
            for (int j = i; j < enemyChild.ToArray().Length; j++)
            {
                if (enemyChild[i].transform.GetComponent<EnemyChuFa>().exp > enemyChild[j].transform.GetComponent<EnemyChuFa>().exp)
                {
                    linshi = enemyChild[i].gameObject;
                    enemyChild[i] = enemyChild[j];
                    enemyChild[j] = linshi.transform;
                }
            }
        }
        for (int i = 0; i < enemyChild.ToArray().Length; i++)
        {
            GameObject enemyDataUi = Instantiate(enemyUi);
            enemyDataUi.transform.SetParent(gameObject.transform);
            if (i % 2 == 0)
            {
                enemyDataUi.transform.position = new Vector3(0, i * -1f, 0);
            }
            else
            {
                enemyDataUi.transform.position = new Vector3(13f, (i-1) * -1f, 0);
            }
           
            enemyDataUi.transform.Find("EnemyImg").GetComponentInChildren<Image>().sprite = enemyChild[i].transform.GetComponent<SpriteRenderer>().sprite;
            enemyDataUi.transform.Find("EnemyName").GetComponentInChildren<Text>().text = enemyChild[i].transform.GetComponent<EnemyChuFa>().Name;
            enemyDataUi.transform.Find("EnemyHp").GetComponentInChildren<Text>().text = "生命" + enemyChild[i].transform.GetComponent<EnemyChuFa>().hp.ToString();
            enemyDataUi.transform.Find("EnemyAttack").GetComponentInChildren<Text>().text = "攻击" + enemyChild[i].transform.GetComponent<EnemyChuFa>().attack.ToString();
            enemyDataUi.transform.Find("EnemyDefense").GetComponentInChildren<Text>().text = "防御" + enemyChild[i].transform.GetComponent<EnemyChuFa>().defense.ToString();
            enemyDataUi.transform.Find("EnemyExp").GetComponentInChildren<Text>().text = "经验" + enemyChild[i].transform.GetComponent<EnemyChuFa>().exp.ToString();
            enemyDataUi.transform.Find("EnemyMoney").GetComponentInChildren<Text>().text = "金钱" + enemyChild[i].transform.GetComponent<EnemyChuFa>().money.ToString();
            enemyDataUi.transform.Find("EnemyHurt").GetComponentInChildren<Text>().text = "对你的伤害" + enemyChild[i].transform.GetComponent<EnemyChuFa>().hurtCount();
        }



    }
    bool isstringEqual(Transform obj)
    {
        for (int i = 0; i < enemyChild.ToArray().Length; i++)
        {
            for (int j = i; j < enemyChild.ToArray().Length - i; j++)
            {
                if (obj.GetComponent<EnemyChuFa>().Name == enemyChild[j].GetComponent<EnemyChuFa>().Name)
                {
                    return false;
                }
            }
        }
        return true;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject.Find("Player").GetComponent<PlayerMove>().enabled = true;
            Destroy(gameObject);
        }
    }
    
}
