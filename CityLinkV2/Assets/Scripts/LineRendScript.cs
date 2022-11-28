using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendScript : MonoBehaviour
{

    [SerializeField] GameObject mouse3Dref;


    LineRenderer lr;
    Transform posRef;
    Mouse3DPosition mouse3Dscript;

    private GameObject ville1Ref;
    private GameObject ville2Ref;


    public Vector3 hitTransform;

    private Vector3 startPos;
    private Vector3 endPos;

    private float distance;

    private bool isCreated = false;



    private void Start()
    {
        mouse3Dref = GameObject.FindGameObjectWithTag("Mouse");
        mouse3Dscript = mouse3Dref.GetComponent<Mouse3DPosition>();
        lr = GetComponent<LineRenderer>();

        posRef = mouse3Dref.transform;

        hitTransform = mouse3Dscript.hitObject.transform.position;

        ville1Ref = mouse3Dscript.hitObject;

        lr.SetPosition(0, new Vector3(hitTransform.x, 0, hitTransform.z));
    }
    private void Update()
    {
        Debug.Log("position souris :" + posRef);

        if (Input.GetMouseButtonDown(0) && isCreated == false)
        {
            if (mouse3Dref.GetComponent<Mouse3DPosition>().hitObject.layer == 6)
            {
                hitTransform = mouse3Dscript.hitPoint;
                
                lr.SetPosition(0, new Vector3(hitTransform.x, 1, hitTransform.z));
            }

        }

        if (isCreated == false)
        {
            lr.SetPosition(1, new Vector3(posRef.position.x, 1, posRef.position.z));
        }

        if(isCreated == true)
        {
            if (ville1Ref == null || ville2Ref == null)
            {
                Destroy(gameObject);
            }
        }
        

    }

    public void setEndPos()
    {
        lr.SetPosition(1, new Vector3(mouse3Dscript.hitObject.transform.position.x, 1, mouse3Dscript.hitObject.transform.position.z));
        distance = (mouse3Dscript.hitObject.transform.position - hitTransform).magnitude;

        Debug.Log("distance =" + distance);
        isCreated = true;
        ville2Ref = mouse3Dscript.hitObject;
        ville1Ref.GetComponent<Town>().setMoneyRate(10 / distance);
        ville2Ref.GetComponent<Town>().setMoneyRate(10 / distance);

    }

}
