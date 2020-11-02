using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using AOT;
public class AppsKitsUtiliOS : MonoBehaviour
{
#if UNITY_IOS
    [DllImport("__Internal")]
    private static extern string _sayHiToUnity();
    [DllImport("__Internal")]
    private static extern void sendMail();
#endif
    public static string SayHiToUnity()
    {
#if UNITY_IOS
        return _sayHiToUnity();
#endif
    }

    public static void SendMail()
    {
#if UNITY_IOS
        Debug.Log("AppsKitsUtiliOS:sendMail()");
        sendMail();
#endif
    }

    private static AppsKitsUtiliOS _instance;

    public static AppsKitsUtiliOS Instance
    {
        get
        {
            if (_instance == null)
            {
                var obj = new GameObject("AppsKitsUtiliOS");
                _instance = obj.AddComponent<AppsKitsUtiliOS>();
            }

            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    

}
