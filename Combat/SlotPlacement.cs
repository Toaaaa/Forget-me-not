using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotPlacement : MonoBehaviour
{
    private CombatManager combatManager;
    public GameObject[] P_slotPlace;//플레이어가 배치될 위치
    public GameObject[] M_slotPlace;//몬스터가 배치될 위치

    public Camera mainCamera;
    public Canvas canvas;
    public RectTransform[] UiInCanvas;//캔버스 에 있는 플레이어 slot 위치
    public RectTransform[] UiMobInCanvas;//캔버스 에 있는 몬스터 slot 위치

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

    public void PlayerSlotPlace() //몬스터의 경우 combatManager 에서 SlotPlacement를 통해 배치시킴.
    {
        for (int i = 0; i < combatManager.playerList.Count; i++)
        {
            if (combatManager.playerList[i] != null)
            {
                var obj = Instantiate(combatManager.playerList[i].Char_Prefab, P_slotPlace[i].transform.position, Quaternion.identity);
                combatManager.combatDisplay.slotList[i].playerPrefab = obj; //p0 slot의 playerPrefab에 해당되는 플레이어의 오브젝트를 등록시켜줌.
                obj.GetComponent<CharacterPrefab>().thisSlot = combatManager.combatDisplay.slotList[i];
            }
        }
    }
}
