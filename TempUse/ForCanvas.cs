using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForCanvas : MonoBehaviour
{
    private static ForCanvas instance;

    public static ForCanvas Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (ForCanvas)FindObjectOfType(typeof(ForCanvas));

                if (instance == null)
                {
                    GameObject obj = new GameObject(typeof(ForCanvas).Name, typeof(ForCanvas));
                    instance = obj.GetComponent<ForCanvas>();
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (instance == null)
        {
            instance = this as ForCanvas;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
