using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stairs : MonoBehaviour
{
    public string sceneName;
    public bool isUp = true;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (isUp)
            {
                PlayerPrefs.SetInt("isUp", 1);
            }
            else
            {
                PlayerPrefs.SetInt("isUp", 0);
            }
            SceneManager.LoadScene(sceneName);
            
        }
    }
}
