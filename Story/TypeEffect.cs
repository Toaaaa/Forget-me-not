using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TypeEffect : MonoBehaviour
{
    public int CharPerSeconds;
    public GameObject EndCursor;//���� �߰� �� ���� �ִ� end�� ǥ���� Ŀ��.
    public string targetMsg;
    public TextMeshProUGUI msgText;
    public AudioSource audioSource;
    int index;
    public bool isAnim;//Ÿ���� ��������.

    public void SetMsg(string msg)
    {
        if(isAnim)
        {
            msgText.text = targetMsg;
            CancelInvoke();
            EffectEnd();
        }
        else
        {
            targetMsg = msg;
            EffectStart();
        }
    }
    void EffectStart()
    {
        msgText.text = "";
        index = 0;
        if(EndCursor !=null)
            EndCursor.SetActive(false);
        isAnim = true;
        Invoke("Effecting", 1f / CharPerSeconds);
    }

    void Effecting()
    {
        if (msgText.text == targetMsg)
        {
            EffectEnd();
            return;
        }
        msgText.text += targetMsg[index];
        //Sound
        if (targetMsg[index]!=' '&& targetMsg[index] != '.'&& targetMsg[index] != ',')
            audioSource.Play();
        index++;
        Invoke("Effecting", 1f / CharPerSeconds);
    }

    void EffectEnd()
    {
        isAnim = false;
        if (EndCursor != null)
            EndCursor.SetActive(true);
    }
}
