using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public string portalTo; //�̵��� ���� �̸�
    public GameObject portalUseUI; //��Ż ���� �ߴ� UI

    private void Start()
    {
        if(portalUseUI == null)
        {
            portalUseUI = GameManager.Instance.portalUI.gameObject;
        }
    }

    public void portalOn()
    {
        Player.Instance.currentMapName = SceneManager.GetActiveScene().name; //�̵��� ���̸� �޾��ֱ�
        SceneManager.LoadScene(portalTo);
    }
    private void Update()
    {
        if(portalUseUI.activeSelf)
        {
            portalUseUI.transform.position = Camera.main.WorldToScreenPoint(this.transform.position);
            GameManager.Instance.isOtherUI = true;
        }
        else
        {
            GameManager.Instance.isOtherUI = false;
        }

        if(Input.GetKeyDown(KeyCode.Space) && portalUseUI.activeSelf)
        {
            GameManager.Instance.isOtherUI = false;
            portalUseUI.SetActive(false);
            portalOn();
        }
        if(Input.GetKeyDown(KeyCode.Escape) && portalUseUI.activeSelf)
        {
            portalUseUI.GetComponent<UIPanelEffect>().OnDeactive();
        }
    }
}
