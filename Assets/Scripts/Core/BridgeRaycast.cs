using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeRaycast : MonoBehaviour
{
    private float range = 10f;
    
    [SerializeField]
    private LayerMask bridgeStairLayer;
    
    Vector3 ShootDirection,RayPosition;

    PlayerInteract Interact;
    void Start()
    {
        //ShootDirection = transform.TransformDirection(Quaternion.Euler(-10f, 0f, 0f) * Vector3.down * range);
        ShootDirection = transform.TransformDirection(Vector3.down * range);
        Interact = PlayerInteract.Ins;
    }

    void Update()
    {
        RayPosition = transform.position;
        ShootRay();
    }

    public void ShootRay()
    {        
        Ray ray = new Ray(RayPosition, ShootDirection);
        Debug.DrawRay(RayPosition,ShootDirection);
        
        if(Physics.Raycast(ray,out RaycastHit hit, range, bridgeStairLayer))
        {
            if (Interact.BrickHolder.Count > 0)
            {
                Interact.DropBrick();
            }
        }
        
    }
}
