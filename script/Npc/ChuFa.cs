using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChuFa : MonoBehaviour
{
    public GameObject talk;
    public bool isTalk = false;
    private bool NpcMove = true;

    public string NpcMoveDir;
    public string[] giveName;
    public int[] giveCount;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player"&&!isTalk)
        {
            GameObject.Find("Player").GetComponent<PlayerMove>().enabled =false;
            isTalk = true;
            talk.SetActive(true);
        }

    }
    private void OnTriggerExit(Collider other)
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if ((Input.GetKeyDown(KeyCode.E) && isTalk && NpcMove)||(Input.GetMouseButtonDown(0)&&isTalk&&NpcMove) )//结束对话
        {
            NpcMove = false;
            if (NpcMoveDir != null)
            {
                NpcMoveToPos(NpcMoveDir);
            }
           
            for(int i = 0; i < giveName.Length; i++)
            {
                NpcGivePlayer(giveName[i], giveCount[i]);
            }
            talk.SetActive(false);
            GameObject.Find("Player").GetComponent<PlayerMove>().enabled = true;
        }
    }
    void NpcGivePlayer(string giveName,int giveCount)//Npc给玩家物品
    {
        PlayerAllData.Ins.changePlayerData(giveCount, giveName);
    }
    void NpcMoveToPos(string dirName)//Npc移动
    {
        if (dirName == "Left")
        {
            transform.parent.position += new Vector3(-1, 0, 0);
        }
        if (dirName == "Right")
        {
            transform.parent.position += new Vector3(1, 0, 0);
        }
        if (dirName == "Up")
        {
            transform.parent.position += new Vector3(0, 1, 0);
        }
        if (dirName == "Down")
        {
            transform.parent.position += new Vector3(0, -1, 0);
        }
        if(dirName == "Destroy")
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}
