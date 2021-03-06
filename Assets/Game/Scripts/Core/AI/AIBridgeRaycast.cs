using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBridgeRaycast : MonoBehaviour
{
    private float range = 10f;

    public GameObject RedAI, GreenAI, YellowAI;
    public Collider AICollider;
    public Rigidbody AIRigidbody;
    
    Renderer selectChildRenderer;
    Transform select;

    [SerializeField]
    private Material BrickMaterial;

    [SerializeField]
    private LayerMask bridgeStairLayer;

    Vector3 ForwardDirection, DropBrickDirection, RayPosition;

    AIInteract aIInteract;

    void Start()
    {
        DropBrickDirection = Quaternion.Euler(-50, 0, 0) * Vector3.down * range;
        ForwardDirection = Vector3.forward * range;

        aIInteract = AIInteract.Ins;
    }

    void Update()
    {
        RayPosition = transform.position;
            
        DropBrickRay();
    }

    public void DropBrickRay()
    {
        Ray ray = new Ray(RayPosition, DropBrickDirection);
        RaycastHit hit;
        Debug.DrawRay(RayPosition, DropBrickDirection);

        if (Physics.Raycast(ray, out hit, range, bridgeStairLayer))
        {
            switch (gameObject.tag)
            {
                case GameConstant.RED_TAG:
                    BuildBridge(hit, aIInteract.RedBrickHolder);
                    break;
                case GameConstant.GREEN_TAG:
                    BuildBridge(hit, aIInteract.GreenBrickHolder);
                    break;
                case GameConstant.YELLOW_TAG:
                    BuildBridge(hit, aIInteract.YellowBrickHolder);
                    break;
                default:
                    Debug.Log("Error DropBrickRay");
                    break;
            }            
        }
    }

    public void BuildBridge(RaycastHit hit, List<GameObject> BrickHolder)
    {
        if (BrickHolder.Count > 0)
        {
            select = hit.transform;
            if (!hit.transform.gameObject.CompareTag(gameObject.tag))
            {
                hit.transform.gameObject.tag = gameObject.tag;
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
        
            switch (gameObject.tag)
            {
                case GameConstant.RED_TAG:
                    aIInteract.DropBrick(gameObject.tag, aIInteract.RedGridBrickPos,aIInteract.RedBrickHolder);
                    break;
                case GameConstant.GREEN_TAG:
                    aIInteract.DropBrick(gameObject.tag, aIInteract.GreenGridBrickPos, aIInteract.GreenBrickHolder);
                    break;
                case GameConstant.YELLOW_TAG:
                    aIInteract.DropBrick(gameObject.tag, aIInteract.YellowGridBrickPos, aIInteract.YellowBrickHolder);
                    break;
                default:
                    Debug.Log("Error DropBrickRay 2");
                    break;
            }            
        }
    }
}
