using System.Collections;
using System.Collections.Generic;
using GameLogger;
using Unity.VisualScripting;
using UnityEngine;

public class SampleScript : MonoBehaviour
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
        
        GDebug.ResetLogFile();
        
        GDebug.LogToFile("Message");
        
        GDebug.Log("Message");
        GDebug.LogWarning("Warning");
        GDebug.LogError("Error"); 
        
        GDebug.Log("Message", ("GameLogger", Color.red));

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
