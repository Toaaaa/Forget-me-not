using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    public GameObject textObject;

    public void Textscript(string text)
    {
        textObject.GetComponent<TextMeshProUGUI>().text = text;
        textObject.SetActive(true);
        StartCoroutine(Turnof3Sec());
    }

    IEnumerator Turnof3Sec()
    {
        yield return new WaitForSeconds(3f);
        textObject.SetActive(false);
    }
}
