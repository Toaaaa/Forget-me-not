using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MapData : MonoBehaviour
{
    private void Start() //���ο� ���� �ε�Ǹ� �ش� ���� �����͸� �Ŵ����� ����.
    {
        GameManager.Instance.mapData = this;
        CombatManager.Instance.mapData = this;
    }



}
