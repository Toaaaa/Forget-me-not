using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    public GameObject target;
    public float moveSpeed;
    [SerializeField]
    private Vector3 targetPosition;

    void Start()
    {
        if(target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }
    }

    void FixedUpdate()
    {
       if(target.gameObject != null)
        {
            targetPosition.Set(target.transform.position.x, target.transform.position.y, this.transform.position.z);
            this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
       //�ش� ������Ʈ x,yȸ���� 0���� ����
       this.transform.rotation = Quaternion.Euler(0, 0, 0);

    }

    

}
