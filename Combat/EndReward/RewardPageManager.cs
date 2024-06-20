using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardPageManager : MonoBehaviour
{
    //���� â������ ���� ������Ʈ��, ������.
    public PlayableC[] charactersInParty;//������ ������ ĳ���͵� 
    public GameObject[] rewardCharacter;//������ ������ ĳ���͵��� ��ġ
    public GameObject[] spotLightSingle;//ĳ���͵鿡�� ���� ���ߴ� ����Ʈ����Ʈ
    public GameObject[] spotLightDouble;//�������� �ش� ĳ���Ϳ��� ����� �� ���� ����Ʈ ����Ʈ
    public RewardDisplay rewardDisplay;//����â�� ������� ǥ���� �� ��ũ��Ʈ (������� ����ġ,���,������)
    public GameObject[] expShow;//����ġ�� ǥ���� �� ������Ʈ

    public GameObject[] levelUpEffect;//�������� ����� ����Ʈ(�ؽ�Ʈ)
    public GameObject[] statIncreaseEffect;//������ �����Ҷ� ����� ����Ʈ(�ؽ�Ʈ)
    public GameObject[] skillUnloackEffect;//��ų�� �رݵɶ� ����� ����Ʈ(�ؽ�Ʈ)

    private void Start()
    {
        charactersInParty = new PlayableC[CombatManager.Instance.playerList.Count];
        charactersInParty = CombatManager.Instance.playerList.ToArray();
    }
}
