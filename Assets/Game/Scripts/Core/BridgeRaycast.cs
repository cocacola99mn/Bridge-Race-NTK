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
    private Material BlueMaterial;

    [SerializeField]
    private LayerMask bridgeStairLayer;
    
    Vector3 MovementRestrictDirection,DropBrickDirection,RayPosition;

    PlayerInteract Interact;
    Player playerIns;

    void Start()
    {
        DropBrickDirection =  Quaternion.Euler(-60,0,0) * Vector3.down * range;
        MovementRestrictDirection = Vector3.forward * range;
        
        Interact = PlayerInteract.Ins;
        playerIns = Player.Ins;
    }

    void Update()
    {
        RayPosition = transform.position;
        
        DropBrickRay();
        
        MovementRestrictRay();
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
            selectChildRenderer.material = BlueMaterial;
            selectChildRenderer.enabled = true;
            Interact.DropBrick(GameConstant.BLUE_TAG);
        }
    }

    public void MovementRestrictRay()
    {
        Ray ray = new Ray(RayPosition, MovementRestrictDirection);
        RaycastHit hit;
        Debug.DrawRay(RayPosition, MovementRestrictDirection);

        if (Physics.Raycast(ray, out hit, range, bridgeStairLayer))
        {
            if (Interact.BrickHolder.Count > 0)
            {
                playerIns.MoveForwardRestrict = false;
            }
            else if (hit.distance < 0.42)
            {
                switch (player.tag)
                {
                    case GameConstant.BLUE_TAG:
                        checkStairTag(GameConstant.BLUE_TAG, hit);
                        break;

                    case GameConstant.GREEN_TAG:
                        checkStairTag(GameConstant.GREEN_TAG, hit);
                        break;

                    case GameConstant.RED_TAG:
                        checkStairTag(GameConstant.RED_TAG, hit);
                        break;

                    case GameConstant.YELLOW_TAG:
                        checkStairTag(GameConstant.YELLOW_TAG, hit);
                        break;

                    default:
                        Debug.Log("Error Raycast");
                        break;
                }
            }
            else
            {
                playerIns.MoveForwardRestrict = false;
            }
        }
    }

    public void checkStairTag(string tag, RaycastHit hit)
    {
        if (hit.collider.CompareTag(tag))
            playerIns.MoveForwardRestrict = false;
        else
            playerIns.MoveForwardRestrict = true;
    }
}
