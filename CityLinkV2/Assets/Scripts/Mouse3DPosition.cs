using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse3DPosition : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private LayerMask _lmVilles;


    public Vector3 hitPoint;

    public GameObject hitObject;

    private float mouseState = 0f;
    private void Awake()
    {
        hitObject = null;
    }
    private void Update()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, _lmVilles))
        {
            transform.position = raycastHit.point;
            hitPoint = raycastHit.point;
            hitObject = raycastHit.collider.gameObject;

        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            changeState(1);
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            changeState(0);
        }

    }



    private float changeState(float value)
    {
        return mouseState = value;
    }

    public float getState()
    {
        return mouseState;
    }



}
