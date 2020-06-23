using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HedgehogTeam.EasyTouch;
/// <summary>
/// 玩家移动脚本
/// </summary>
public class PlayerMove : MonoBehaviour
{
    public bool isBallet = false;
    public float speed = .15f; //玩家移动速度
    public Vector2 dest = Vector2.zero;  //玩家移动方向


    
    public Camera mainCamera;//主摄像机
    private void Start()
    {
        dest = GameObject.Find("BornGameObject").transform.position; //初始化将方向设置为玩家自身位置
    }
    void Update()
    {
        //if (transform.position.y >= -1)//设置摄像机的y轴位置不能低于-2
        //{
        //    Vector3 CameraPos = new Vector3(0, transform.position.y, -10);//摄像机x轴位置不变，y轴跟随玩家
        //    mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, CameraPos, 10 * Time.deltaTime);//以10的速度平滑移动摄像机，从当前位置到玩家位置

        //}
       
        if((Vector2)transform.position == dest)//只有当玩家移动到dest位置才进行下一步操作
        {
            if ((Input.GetKey(KeyCode.W)) && canMove(Vector2.up))//上
            {
                dest = (Vector2)transform.position + Vector2.up;
            }
            if (Input.GetKey(KeyCode.S) && canMove(Vector2.down))//下
            {
                dest = (Vector2)transform.position + Vector2.down;
            }
            if (Input.GetKey(KeyCode.A) && canMove(Vector2.left))//左
            {
                dest = (Vector2)transform.position + Vector2.left;
            }
            if (Input.GetKey(KeyCode.D) && canMove(Vector2.right))//右
            {
                dest = (Vector2)transform.position + Vector2.right;
            }
        }
       
    }
    
    private void FixedUpdate()//物理版Update
    {
        Vector2 temp = Vector2.MoveTowards((Vector2)transform.position, dest, speed);//生成差值，以speed的速度从玩家位置到dest位置
        GetComponent<Rigidbody2D>().MovePosition(temp);//刚体移动
    }
    private bool canMove(Vector2 dir)//判断是否可以移动到dest的位置
    {
        Vector2 pos = transform.position;
        RaycastHit2D hit = Physics2D.Linecast(pos + dir, pos);//由dest位置发射一条射线，将第一个碰撞物返回
        if(hit.collider.tag == "Enemy") //如果遇到的是敌人
        {
            if (hit.collider.GetComponent<EnemyChuFa>().canBattle())//如果能打过，则可以走
            {
                isBallet  = true;
                return true;
            }
            else
            {
                hit.collider.GetComponent<EnemyChuFa>().playAni();//打不过，播放动画并返回不可以走
                return false;
            }
        }
        if ( hit.collider.tag == "SceneProp") return true;//如果是场景可交互物体，可以走
        if (hit.collider.tag == "Shop")//如果是商店，可以走
        {
            GameObject.Find("BornGameObject").GetComponent<GameManager>().phoneMove.SetActive(false);
            GameObject.Find("BornGameObject").GetComponent<GameManager>().Shop.SetActive(true);//显示商店的UI
            GameObject.Find("BornGameObject").GetComponent<GameManager>().ToSeeEnemyUi.SetActive(false);
            GameObject.Find("Player").GetComponent<PlayerMove>().enabled = false;//禁用掉移动脚本
        }
        if(hit.collider.tag == "ExpOldman")
        {
            GameObject.Find("BornGameObject").GetComponent<GameManager>().phoneMove.SetActive(false);
            GameObject.Find("BornGameObject").GetComponent<GameManager>().ToSeeEnemyUi.SetActive(false);
            GameObject.Find("BornGameObject").GetComponent<GameManager>().ExpMan.SetActive(true);
            GameObject.Find("Player").GetComponent<PlayerMove>().enabled = false;//禁用掉移动脚本
        }
        if (hit.collider.tag == "Door") {//如果是门
            if (hit.collider.GetComponent<SceneProp>().isKeyEnough())//如果钥匙数量足够，返回成功
            {

                return true;
            }
            if(!hit.collider.GetComponent<SceneProp>().isKeyEnough())//如果钥匙数量不足，返回失败
            {
                Instantiate(hit.collider.GetComponent<SceneProp>().cantKey);//生成钥匙不足动画
                return false;
            }

        }
        return (hit.collider == GetComponent<Collider2D>());//除此之外，返回判断射线的第一个物体是不是玩家自己
    }
    public void PhoneMove(Vector2 dir)
    {
        if ((Vector2)transform.position == dest&&!isBallet)
        {
            if (dir.x > -0.5 && dir.x < 0.5 && dir.y > 0.7)
            {
                if (canMove(Vector2.up))//上
                {
                    dest = (Vector2)transform.position + Vector2.up;
                }

            }
            if (dir.x > -0.5 && dir.x < 0.5 && dir.y < -0.7)
            {
                if (canMove(Vector2.down))//下
                {
                    dest = (Vector2)transform.position + Vector2.down;
                }
            }
            if (dir.y > -0.5 && dir.y < 0.5 && dir.x < -0.7)
            {
                if (canMove(Vector2.left))//左
                {
                    dest = (Vector2)transform.position + Vector2.left;
                }
            }
            if (dir.y > -0.5 && dir.y < 0.5 && dir.x > 0.7)
            {
                if (canMove(Vector2.right))//右
                {
                    dest = (Vector2)transform.position + Vector2.right;
                }
            }
        }

          

    }
}
