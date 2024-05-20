using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VirtualCamera :MonoBehaviour
{
    private static VirtualCamera instance;
    CinemachineVirtualCamera cinevircamera;
    CinemachineConfiner2D cineconf;

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

        cinevircamera = GetComponent<CinemachineVirtualCamera>();
        cineconf = GetComponent<CinemachineConfiner2D>();
        if (GameManager.Instance.Player != null)
        {
            cinevircamera.Follow = GameManager.Instance.Player.transform;
            cinevircamera.LookAt = GameManager.Instance.Player.transform;
        }
    }

    private void Update()
    {
        if (cineconf.m_BoundingShape2D == null)
        {
            cineconf.m_BoundingShape2D = FindObjectOfType<PolygonCollider2D>();
        }
        if (Player.Instance.virtualCamera == null)
        {
            Player.Instance.virtualCamera = this;
        }
    }
    private void OnEnable()
    {
       this.transform.position = new Vector3(Player.Instance.transform.position.x, Player.Instance.transform.position.y, this.transform.position.z);

    }
}
