using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectZSorting : MonoBehaviour
{

    void Start()
    {
        SpriteZValueSorting();
    }
    void SpriteZValueSorting()
    {
        var position = this.gameObject.transform.position;
        position.z = 3 + position.y * 0.01f; //(ÃÖ¼Ò°ª 3)
        this.gameObject.transform.position = position;
    }
}
