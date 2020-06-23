using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneProp : MonoBehaviour
{
    // Start is called before the first frame update

    public string GiveName;
    public int  GiveCount;

    public GameObject cantKey;

    private Scene scene;
    private string sceneName;
    private void Awake()
    {
        scene = SceneManager.GetActiveScene();
        sceneName = scene.name;
        if (PlayerPrefs.GetString(gameObject.name + sceneName) == "die")
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            giveAndDestroy();
        }
        else
        {
            Instantiate(cantKey);
        }

    }
    public bool isKeyEnough()
    {
        return PlayerAllData.Ins.canDestroyProp(GiveCount, GiveName);
    }
    public void giveAndDestroy()
    {
        PlayerAllData.Ins.changePlayerData(GiveCount, GiveName);
        PlayerPrefs.SetString(gameObject.name + sceneName, "die");
        Destroy(gameObject);
    }
}
