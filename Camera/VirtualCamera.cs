using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VirtualCamera :MonoBehaviour
{
    private static VirtualCamera instance;

    public static VirtualCamera Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (VirtualCamera)FindObjectOfType(typeof(VirtualCamera));

                if (instance == null)
                {
                    GameObject obj = new GameObject(typeof(VirtualCamera).Name, typeof(VirtualCamera));
                    instance = obj.GetComponent<VirtualCamera>();
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
            instance = this as VirtualCamera;
        }
        else
        {
            Destroy(gameObject);
        }

        CinemachineVirtualCamera cinevircamera = GetComponent<CinemachineVirtualCamera>();
        CinemachineConfiner2D cineconf = GetComponent<CinemachineConfiner2D>();
        cinevircamera.Follow = GameManager.Instance.Player.transform;
        cinevircamera.LookAt = GameManager.Instance.Player.transform;
        if(cineconf.m_BoundingShape2D == null)
        {
            cineconf.m_BoundingShape2D = FindObjectOfType<PolygonCollider2D>();
        }
    }
}
