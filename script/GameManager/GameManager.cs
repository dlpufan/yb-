using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{



    public Text LvText;
    public Text MoneyText;
    public Text ExpText;
    public Text HpText;
    public Text AttackText;
    public Text DefenseText;
    public Text YellowKeyText;
    public Text BlueKeyText;
    public Text RedKeyText;

    public GameObject phoneMove;
    public GameObject ToSeeEnemyUi;


    public GameObject EnemyDataUi;
    public GameObject ExpMan;

    public Transform DownFloor;

    public GameObject Shop;

    public Camera mainCamera;

    private GameObject player;
    private void Awake()//在进入新关卡时刷新UI界面，定位Player，为Player赋值
    {
        phoneMove = GameObject.Find("EasyTouchControlsCanvas");
        phoneMove.SetActive(false);
        player = GameObject.Find("Player");
        if (DownFloor != null&&PlayerPrefs.GetInt("isUp") == 0)
        {
            player.transform.position = DownFloor.transform.position;
            player.GetComponent<PlayerMove>().dest = DownFloor.transform.position;
        }
        else
        {
            player.transform.position = transform.position;
            player.GetComponent<PlayerMove>().dest = transform.position;
        }


        player.GetComponent<PlayerAllData>().LvText = LvText;
        player.GetComponent<PlayerAllData>().MoneyText = MoneyText;
        player.GetComponent<PlayerAllData>().ExpText = ExpText;
        player.GetComponent<PlayerAllData>().HpText = HpText;
        player.GetComponent<PlayerAllData>().AttackText = AttackText;
        player.GetComponent<PlayerAllData>().DefenseText = DefenseText;
        player.GetComponent<PlayerAllData>().YellowKeyText = YellowKeyText;
        player.GetComponent<PlayerAllData>().BlueKeyText = BlueKeyText;
        player.GetComponent<PlayerAllData>().RedKeyText = RedKeyText;
        player.GetComponent<PlayerMove>().mainCamera = mainCamera;
        LvText.text = player.GetComponent<PlayerAllData>().Lv.ToString();
        MoneyText.text = player.GetComponent<PlayerAllData>().Money.ToString();
        ExpText.text = player.GetComponent<PlayerAllData>().Exp.ToString();
        HpText.text = player.GetComponent<PlayerAllData>().Hp.ToString();
        AttackText.text = player.GetComponent<PlayerAllData>().Attack.ToString();
        DefenseText.text = player.GetComponent<PlayerAllData>().Defense.ToString();
        YellowKeyText.text = player.GetComponent<PlayerAllData>().YellowKey.ToString();
        BlueKeyText.text = player.GetComponent<PlayerAllData>().BlueKey.ToString();
        RedKeyText.text = player.GetComponent<PlayerAllData>().RedKey.ToString();
        Invoke("wait", .01f);
    }

    
    
    void Update()
    {
        
        
    }
    public void CheckToSeeData()
    {

        GameObject EnemyUi = Instantiate(EnemyDataUi);
       
        EnemyUi.transform.SetParent(GameObject.Find("EnemyDataUi").transform);
        player.GetComponent<PlayerMove>().enabled = false;
       
    }
    void wait()
    {
        phoneMove.SetActive(true);
    }
}

