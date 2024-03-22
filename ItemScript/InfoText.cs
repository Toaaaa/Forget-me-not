using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoText : MonoBehaviour
{
    public GameObject[] itemInfos; //0:sptire, 1: name, 2: option, 3: description
    //public GameObject selectedItem;

    private static InfoText instance;
    public static InfoText Instance
    {
        get
        {
            if (instance == null)
            {

                    instance = new InfoText();
            }
            return instance;
        }
    }

    private void Update()
    {
        


    }

}
