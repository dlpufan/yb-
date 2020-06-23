using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelectSave : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        PlayerPrefs.DeleteAll();
        Destroy(gameObject);
    }
}
