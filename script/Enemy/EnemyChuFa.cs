using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using  UnityEngine.SceneManagement;

public class EnemyChuFa : MonoBehaviour
{
    
    public int hp;  //怪物血量  
    public int attack;//怪物攻击
    public int defense;//怪物防御
    public int money;//怪物掉落金钱
    public int exp;   //怪物掉落经验
    public string Name;//怪物名字，用于宝物查看怪物信息

    private  int AllHp;//怪物总血量，固定不变
    private float timer = 0;//计时器

    public GameObject musicAttack;//攻击音效
    public GameObject musicDefense;//防御音效

    public GameObject cantBattleAni;//打不过动画

    public Slider HpSlider;//血条UI
    public Image fill;//怪物头像UI
    private Scene scene;//当前场景
    private string sceneName;//当前场景名字
    public bool isBattle = false;//正在战斗吗？
    public bool isPlay = false;//播放音效吗？

    private GameObject player;
    private void Awake()
    {
       
        scene = SceneManager.GetActiveScene();//获取当前场景
        sceneName = scene.name;//获取当前场景名
        if( PlayerPrefs.GetString(gameObject.name+sceneName) == "die")//如果gameObject.name+sceneName这个键值对存的信息为die，销毁游戏物体
        {
            Destroy(gameObject);
        }
        player = GameObject.Find("Player");
    }
    void Start()
    {
        AllHp = hp;//初始化总血量
        fill.color = Color.green;//初始化血条颜色
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")//如果与玩家相遇
        {
            isBattle = true;//开始战斗
            isPlay = true;//播放音效
            GameObject.Find("BornGameObject").GetComponent<GameManager>().ToSeeEnemyUi.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
        if((float)hp/AllHp <= .5f)//血量低于50%，血条变黄
        {
            
            if ((float)hp / AllHp <= .2f)//血条低于20%，血条变红
            {
                fill.color = Color.red;
            }
            fill.color = Color.yellow;
        }
        if (isPlay)//如果播放音乐
        {
            if (canBattle())//如果能打过
            {
                //禁用玩家移动脚本
                GameObject music1 = Instantiate(musicAttack);//生成音乐物体
                music1.transform.parent = gameObject.transform;//生成点位怪物坐标
                Invoke("waitToPlay", .4f);//0.4秒后播放防御音效
                isPlay = false;//停止播放
            }
            else
            {
                playAni();//播放打不过动画
                isPlay = false;
                return;
            }
            
        }
        if (Input.GetKey(KeyCode.Space))//如果长按空格，加速战斗过程
        {
            timer +=.075f;
        }
        else
        {
            timer += Time.deltaTime;//不操作，正常播放战斗流程
        }
        
        if (isBattle&&hp>=0&&timer >=.15f)//如果处于战斗状态，且怪物存活，且计时器加载到0.15秒
        {
            player.GetComponent<PlayerMove>().enabled = false;
            hp -= (PlayerAllData.Ins.Attack - defense);//减少怪物Hp
            if (hp <= 0)//如果怪物死亡
            {
                GameObject.Find("BornGameObject").GetComponent<GameManager>().ToSeeEnemyUi.SetActive(true);
                player.GetComponent<PlayerMove>().enabled = true;//启用玩家移动脚本
                isBattle = false;//结束战斗
                player.GetComponent<PlayerMove>().isBallet = false;
                PlayerAllData.Ins.changePlayerData(exp, "Exp");//为玩家增加exp
                PlayerAllData.Ins.changePlayerData(money, "Money");//为玩家增加金钱
                PlayerPrefs.SetString(gameObject.name + sceneName, "die");//将怪物信息储存为die
                Destroy(gameObject);//销毁游戏物体
                return;//结束
            }
            HpSlider.value = (float)hp / AllHp;//控制血条变化
            if(attack- PlayerAllData.Ins.Defense > 0)//怪物攻击玩家
            {
                PlayerAllData.Ins.changePlayerData(-(attack - PlayerAllData.Ins.Defense), "Hp");//玩家掉血
            }
            
            timer = 0f;//重置计时器
        }
        
    }
    void waitToPlay()//来播放防御音效
    {
        GameObject music2 = Instantiate(musicDefense);//实例化
        music2.transform.parent = gameObject.transform;//设置父物体为该游戏物体
    }
    public void playAni()//播放打不过的动画
    {
        Instantiate(cantBattleAni);
        cantBattleAni.transform.position = transform.position;
        isBattle = false;
        isPlay = false;
        
    }
    public string hurtCount()//计算怪物对人的伤害
    {
        int counterAttack = PlayerAllData.Ins.Attack;//获取玩家攻击
        int counterDefense = PlayerAllData.Ins.Defense;//获取玩家防御
        if (counterAttack>defense)
        {
            if (counterDefense >= attack)
            {
                return "0";//如果玩家防御大于怪物攻击
            }
            if ((((float)AllHp / (counterAttack - defense))).ToString().Contains("."))//如果计算结果带小数点，向下取整
            {
                return ((attack - counterDefense) * Math.Floor(((float)AllHp / (counterAttack - defense)))).ToString();
            }
            else//否则就是玩家最后一下正好打死怪物，返回的值减去一次怪物打人掉的血
            {
                return ((attack - counterDefense) * Math.Floor(((float)AllHp / (counterAttack - defense)) - 1)).ToString();
            }
           
        }
       return "???";
        
       
    }
    public bool canBattle()//返回能打过吗
    {
        int counterHp = PlayerAllData.Ins.Hp;
        int counterAttack = PlayerAllData.Ins.Attack;
        int counterDefense = PlayerAllData.Ins.Defense;
        if(attack - counterDefense < 0&&counterAttack>defense)
        {
            return true;
        }
        if(counterAttack == defense)
        {
            return false;
        }
        if(attack == counterDefense)
        {
            return true;
        }
        if (counterAttack < defense|| ((counterHp / (attack - counterDefense)) < (hp / (counterAttack - defense))))
        {
            
            return false;
        }
        else return true;
    }
}
