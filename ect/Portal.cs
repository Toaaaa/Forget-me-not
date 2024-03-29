using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public string portalTo; //이동할 씬의 이름

    public void portalOn()
    {
        Player.Instance.currentMapName = SceneManager.GetActiveScene().name; //이동전 맵이름 받아주기
        SceneManager.LoadScene(portalTo);
    }
}
