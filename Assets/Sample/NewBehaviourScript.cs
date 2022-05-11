using System.Collections;
using System.Collections.Generic;
using GameLogger;
using Unity.VisualScripting;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Color a = Color.black;
        a.ToHexString();
        Init("");
    }

    public void Init(string a)
    {
        GDebug.ResetLogFile();
        
        Debug.Log("Message");
        Debug.LogWarning("Warning");
        Debug.LogError("Error");
        
        GDebug.Log("Message Message Message Message Message Message Message Message Message Message Message Message Message Message Message Message Message Message Message Message Message Message Message Message Message Message Message Message Message Message Message Message Message Message Message Message Message Message Message Message Message Message Message Message Message Message Message Message Message Message Message Message Message Message Message Message Message Message Message ");
        GDebug.LogWarning("Warning", ("Logger", Color.red));
        GDebug.LogError("Error"); 
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
