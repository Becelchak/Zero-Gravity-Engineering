using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandUse : MonoBehaviour
{
    public GameObject hand;
    public Rigidbody2D wrist;
    private HingeJoint2D thisBody;

    private Moving playerMoving;
    private GameObject nowUsedObject;
    private HingeJoint2D objectBody;

    private GameObject nowHandleObject;
    void Start()
    {
        playerMoving = GetComponent<Moving>();
        thisBody = GetComponent<HingeJoint2D>();
    }

    void Update()
    {
        if (hand.transform.localPosition.x != 0.03f || hand.transform.localPosition.y != 0.44f)
            hand.transform.localPosition = new Vector3(0.03f, 0.44f, 0);


        if(playerMoving.GetFreezeStatus()) return;

        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z += Camera.main.nearClipPlane;
        if (Input.GetMouseButton(1))
        {

            float AngleRad = Mathf.Atan2(
                mousePosition.y - transform.position.y,
                mousePosition.x - transform.position.x
            );
            // Get Angle in Degrees
            float AngleDeg = (180 / Mathf.PI) * AngleRad;
            // Rotate Object
            hand.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);

            if(playerMoving.facingRight)
                hand.transform.Rotate(Vector3.right * 180);


            if (nowUsedObject != null)
            {
                if(nowUsedObject.GetComponent<HingeJoint2D>() == null)
                {
                    nowUsedObject.AddComponent<HingeJoint2D>();
                    objectBody = nowUsedObject.GetComponent<HingeJoint2D>();
                    objectBody.connectedBody = hand.GetComponent<Rigidbody2D>();
                }

                //nowUsedObject.transform.position = wrist.gameObject.transform.position;
            }

            if (nowHandleObject != null)
            {
                thisBody.enabled = true;
                if (thisBody.connectedBody == null)
                {
                    thisBody.connectedBody = nowHandleObject.GetComponent<Rigidbody2D>();
                }

                Debug.Log($"Now handle {nowHandleObject}");
            }
        }

        if (Input.GetMouseButtonUp(1))
        {
            if(objectBody != null)
                objectBody.connectedBody = null;
            if (thisBody.connectedBody != null)
            {
                thisBody.connectedBody = null;
                thisBody.enabled = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //if (other.tag == "Transferable")
        //{
        //    nowUsedObject = other.gameObject;
        //}
        if(other.tag is "Object" or "ObjectTileMap")
        {
            nowHandleObject = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Destroy(other.GetComponent<HingeJoint2D>());
        nowUsedObject = null;
        nowHandleObject = null;
    }
}

