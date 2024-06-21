using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoOnReward : MonoBehaviour
{
    public GameObject Level;
    public GameObject Exp;

    public GameObject levelUpEffect;//레벨업시 사용할 이펙트(텍스트)
    public GameObject statIncreaseEffect;//스탯이 증가할때 사용할 이펙트(텍스트)
    public GameObject skillUnlockEffect;//스킬이 해금될때 사용할 이펙트(텍스트)

    // Start is called before the first frame update
    void Update()
    {
        SetInfo();
    }

    private void SetInfo()//레벨과 exp를 해당 캐릭터에 맞게 설정
    {

    }
}
