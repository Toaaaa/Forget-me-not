using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TypeEffect : MonoBehaviour
{
    public int CharPerSeconds;
    public GameObject EndCursor;//추후 추가 할 수도 있는 end시 표시할 커서.
    public string targetMsg;
    public TextMeshProUGUI msgText;
    public AudioSource audioSource;
    int index;
    public bool isAnim;//타이핑 도중인지.

    public void SetMsg(string msg)
    {
        if(isAnim)
        {
            Debug.Log("isAnim");
            msgText.text = targetMsg;
            CancelInvoke();
            EffectEnd();
        }
        else
        {
            Debug.Log("isNotAnim");
            targetMsg = msg;
            EffectStart();
        }
    }
    public void SetExtraMsg(string msg,Item item)
    {
        if (isAnim)
        {
            Debug.Log("isAnim");
            msgText.text = targetMsg;
            CancelInvoke();
            EffectEnd();
        }
        else
        {
            Debug.Log("isNotAnim");
            targetMsg =item.name + " " +msg;
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
        if (targetMsg[index]!=' '&& targetMsg[index] != ',')
            audioSource.Play();
        index++;
        Invoke("Effecting", 1f / CharPerSeconds);
    }

    void EffectEnd()
    {
        if(Player.Instance.storyTalking)//알람글 일때만.
            GameManager.Instance.Player.alarmOn = true;//알람글 출력이 끝나면 tilemanager의 update문에서 활용할수 있게 다시 true로 세팅.
        isAnim = false;
        if (EndCursor != null)
            EndCursor.SetActive(true);
    }
}
