using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VirtualCameraManaging : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cmvC;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelLoaded;
    }

    private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("cmvc enable");
        cmvC.enabled = false;
        cmvC.enabled = true;
    }
}
