using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISpriteCamera : MonoBehaviour
{
    Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }
    private void Update()
    {
        this.transform.position = mainCamera.transform.position;
    }
}
