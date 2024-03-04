using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (T)FindObjectOfType(typeof(T));

                if (instance == null)
                {
                    GameObject obj = new GameObject(typeof(T).Name, typeof(T));
                    instance = obj.GetComponent<T>();
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (transform.parent != null && transform.root != null) // 만약 해당 오브젝트가 다른 오브젝트의 자식일때의 처리
        {
            DontDestroyOnLoad(this.transform.root.gameObject);
        }
        else
        {
            {
                DontDestroyOnLoad(this.gameObject);
            }
        }
    }
}
