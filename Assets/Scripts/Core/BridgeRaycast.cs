using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeRaycast : MonoBehaviour
{
    private float range = 10f;

    [SerializeField]
    private LayerMask layer;
    Vector3 ShootDirection,RayPosition;
    
    void Start()
    {
        ShootDirection = transform.TransformDirection(Quaternion.Euler(-10f, 0f, 0f) * Vector3.down * range);        
    }

    void Update()
    {
        RayPosition = transform.position;
        ShootRay();
    }

    public void ShootRay()
    {
        
        Ray BrickRay = new Ray(RayPosition, ShootDirection);
        Debug.DrawRay(RayPosition,ShootDirection);
    }
}
