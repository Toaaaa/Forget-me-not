using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotPlacement : MonoBehaviour
{
    private CombatManager combatManager;
    public GameObject[] P_slotPlace;//�÷��̾ ��ġ�� ��ġ
    public GameObject[] M_slotPlace;//���Ͱ� ��ġ�� ��ġ

    public Camera mainCamera;
    public Canvas canvas;
    public RectTransform[] UiInCanvas;//ĵ���� �� �ִ� �÷��̾� slot ��ġ
    public RectTransform[] UiMobInCanvas;//ĵ���� �� �ִ� ���� slot ��ġ

    public Transform[] worldObject;
    public Transform[] worldMobObject;

    // Start is called before the first frame update
    void Start()
    {
        combatManager = CombatManager.Instance;
        combatManager.slotPlacement = this;
        worldObject = new Transform[P_slotPlace.Length];
        worldMobObject = new Transform[M_slotPlace.Length];
        UiInCanvas = new RectTransform[P_slotPlace.Length];
        UiMobInCanvas = new RectTransform[combatManager.monsterList.Count];
        for (int i = 0; i < P_slotPlace.Length; i++)
        {
            worldObject[i] = P_slotPlace[i].GetComponent<Transform>();
        }
        for (int i = 0; i < M_slotPlace.Length; i++)
        {
            worldMobObject[i] = M_slotPlace[i].GetComponent<Transform>();
        }
        for (int i = 0; i< combatManager.playerList.Count; i++)
        {
            UiInCanvas[i] = combatManager.combatDisplay.slotList[i].GetComponent<RectTransform>();
        }
        for (int i = 0; i < combatManager.monsterList.Count; i++)
        {
            UiMobInCanvas[i] = combatManager.combatDisplay.mobSlotList[i].GetComponent<RectTransform>();
        }
        mainCamera = Camera.main;
        canvas = combatManager.combatDisplay.canvas;
        PlayerSlotPlace();
    }

    // Update is called once per frame
    void Update()
    {
        if(canvas != null)
        {
            for (int i = 0; i < worldObject.Length; i++)
            {
                Vector3 worldPosition = worldObject[i].position;
                Vector3 screenPosition = mainCamera.WorldToScreenPoint(worldPosition);

                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    canvas.transform as RectTransform,
                    screenPosition,
                    canvas.worldCamera,
                    out Vector2 localPoint
                );

                UiInCanvas[i].localPosition =new Vector2(localPoint.x*1.4085f,localPoint.y*1.4085f);
            }

            for (int i = 0; i < combatManager.monsterList.Count; i++)
            {
                Vector3 worldPosition = worldMobObject[i].position;
                Vector3 screenPosition = mainCamera.WorldToScreenPoint(worldPosition);

                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.transform as RectTransform,
                screenPosition,
                canvas.worldCamera,
                out Vector2 localPoint
                );

                UiMobInCanvas[i].localPosition = new Vector2(localPoint.x * 1.4085f, localPoint.y * 1.4085f);
            }
        }
    }

    public void PlayerSlotPlace() //������ ��� combatManager ���� SlotPlacement�� ���� ��ġ��Ŵ.
    {
        for (int i = 0; i < combatManager.playerList.Count; i++)
        {
            if (combatManager.playerList[i] != null)
            {
                var obj = Instantiate(combatManager.playerList[i].Char_Prefab, P_slotPlace[i].transform.position, Quaternion.identity);
                combatManager.combatDisplay.slotList[i].playerPrefab = obj; //p0 slot�� playerPrefab�� �ش�Ǵ� �÷��̾��� ������Ʈ�� ��Ͻ�����.
                obj.GetComponent<CharacterPrefab>().thisSlot = combatManager.combatDisplay.slotList[i];
            }
        }
    }
}
