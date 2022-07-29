using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeRaycast : MonoBehaviour
{
    public GameObject player;

    private float range = 10f;

    Renderer selectChildRenderer;
    Transform select;

    [SerializeField]
    private Material BrickMaterial;

    [SerializeField]
    private LayerMask bridgeStairLayer,stateChangerLayer,roadLayer;
    
    Vector3 MovementBackwardDirection,MovementForwardDirection,DropBrickDirection,RayPosition, OnRoadRay;

    PlayerInteract Interact;
    Player playerIns;

    void Start()
    {
        DropBrickDirection =  Quaternion.Euler(-50,0,0) * Vector3.down * range;
        MovementForwardDirection = Vector3.forward * range;
        MovementBackwardDirection = -Vector3.forward * range;
        OnRoadRay =  -Vector3.up * range;
        
        Interact = PlayerInteract.Ins;
        playerIns = Player.Ins;
    }

    void Update()
    {
        RayPosition = transform.position;
        
        DropBrickRay();
        
        MoveForwardRestrictRay();
        MoveBackwardRestrictRay();
        ShootOnRoadRay();
    }

    public void DropBrickRay()
    {        
        Ray ray = new Ray(RayPosition, DropBrickDirection);
        RaycastHit hit;
        Debug.DrawRay(RayPosition,DropBrickDirection);
        
        if(Physics.Raycast(ray,out hit, range, bridgeStairLayer))
        {
            BuildBridge(hit);
        }        
    }

    public void BuildBridge(RaycastHit hit) {
        if (Interact.BrickHolder.Count > 0)
        {
            select = hit.transform;
            if(!hit.transform.gameObject.CompareTag(GameConstant.BLUE_TAG))
            {
                hit.transform.gameObject.tag = GameConstant.BLUE_TAG;
                try
                {
                    modifyChildRenderer();
                }
                catch
                {
                    Debug.Log("Can't find component");
                }
            }
            
        }
    }

    public void modifyChildRenderer()
    {
        selectChildRenderer = select.GetComponentInChildren<Renderer>();
        if (selectChildRenderer != null)
        {
            selectChildRenderer.material = BrickMaterial;
            selectChildRenderer.enabled = true;
            Interact.DropBrick(GameConstant.BLUE_TAG);
        }
    }

    public void MoveForwardRestrictRay()
    {
        Ray ray = new Ray(RayPosition, MovementForwardDirection);
        RaycastHit hit;
        Debug.DrawRay(RayPosition, MovementForwardDirection);

        if (Physics.Raycast(ray, out hit, range, bridgeStairLayer))
        {
            if (Interact.BrickHolder.Count > 0)
            {
                playerIns.MoveForwardRestrict = false;
            }
            else if (hit.distance < 0.42)
            {
                checkStairTag(GameConstant.BLUE_TAG, hit);
            }
            else
            {
                playerIns.MoveForwardRestrict = false;
            }
        }
    }

    public void MoveBackwardRestrictRay()
    {
        Ray ray = new Ray(RayPosition, MovementBackwardDirection);
        RaycastHit hit;
        Debug.DrawRay(RayPosition, MovementBackwardDirection);

        if (Physics.Raycast(ray, out hit, range, stateChangerLayer))
        {
            if (hit.distance < 0.2)
                playerIns.MoveBackRestrict = true;
            else
                playerIns.MoveBackRestrict = false;
        }
    }

    public void checkStairTag(string tag, RaycastHit hit)
    {
        if (hit.collider.CompareTag(tag) == true)
            playerIns.MoveForwardRestrict = false;
        else
            playerIns.MoveForwardRestrict = true;
    }

    public void ShootOnRoadRay()
    {
        Ray ray = new Ray(RayPosition, OnRoadRay);
        RaycastHit hit;
        Debug.DrawRay(RayPosition, OnRoadRay);

        if (Physics.Raycast(ray, out hit, range, roadLayer))
            playerIns.IsOnBridge = true;
        else
            playerIns.IsOnBridge = false;
    }
}
