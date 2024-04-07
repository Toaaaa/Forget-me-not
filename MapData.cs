using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MapData : MonoBehaviour
{
    private void Start() //새로운 씬이 로드되면 해당 맵의 데이터를 매니저에 전달.
    {
        GameManager.Instance.mapData = this;
        CombatManager.Instance.mapData = this;
    }



}
