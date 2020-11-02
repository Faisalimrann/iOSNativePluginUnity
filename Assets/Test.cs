using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public Text text;
    void Start()
    {
        text.text = AppsKitsUtiliOS.SayHiToUnity();
    }

    public void sendMail(){
        Debug.Log("Test:sendMail()");
        AppsKitsUtiliOS.SendMail();
    }
}
