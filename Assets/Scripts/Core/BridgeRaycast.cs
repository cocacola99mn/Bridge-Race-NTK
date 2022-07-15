using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeRaycast : MonoBehaviour
{
    private float range = 10f;

    [SerializeField]
    private Material BlueMaterial;

    [SerializeField]
    private LayerMask bridgeStairLayer;
    
    Vector3 ShootDirection,RayPosition;

    PlayerInteract Interact;

    public Brick brickColor;

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
        RaycastHit hit;
        Debug.DrawRay(RayPosition,ShootDirection);
        
        if(Physics.Raycast(ray,out hit, range, bridgeStairLayer))
        {
            BuildBridge(hit);
        }        
    }

    public void BuildBridge(RaycastHit hit) {
        if (Interact.BrickHolder.Count > 0)
        {
            var select = hit.transform;
            if(!hit.transform.gameObject.CompareTag(GameConstant.LASTHITOBJECT_TAG))
            {
                hit.transform.gameObject.tag = GameConstant.LASTHITOBJECT_TAG;
                try
                {
                    var selectChildRenderer = select.GetComponentInChildren<Renderer>();
                    if (selectChildRenderer != null)
                    {
                        selectChildRenderer.material = BlueMaterial;
                        selectChildRenderer.enabled = true;
                        Interact.DropBrick(GameConstant.BLUE_TAG);
                    }
                }
                catch
                {
                    Debug.Log("Can't find component");
                }
            }
            
        }
    }
}
