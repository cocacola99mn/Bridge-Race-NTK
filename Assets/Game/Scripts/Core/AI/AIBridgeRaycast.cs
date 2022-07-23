using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBridgeRaycast : MonoBehaviour
{
    private float range = 10f;

    public GameObject RedAI, GreenAI, YellowAI;
    
    Renderer selectChildRenderer;
    Transform select;

    [SerializeField]
    private Material BrickMaterial;

    [SerializeField]
    private LayerMask bridgeStairLayer,AIBridgeNav;

    bool RayCreated = false;
    Vector3 MovementRestrictDirection, DropBrickDirection, RayPosition,RedAIPos,GreenAIPos,YellowAIPos;

    AIInteract aIInteract;

    void Start()
    {
        DropBrickDirection = Quaternion.Euler(-50, 0, 0) * Vector3.down * range;
        MovementRestrictDirection = Vector3.forward * range;

        aIInteract = AIInteract.Ins;
    }

    void Update()
    {
        RayPosition = transform.position;
        RedAIPos = RedAI.transform.position;
        GreenAIPos = GreenAI.transform.position;
        YellowAIPos = YellowAI.transform.position;
            
        DropBrickRay();

        if (aIInteract.AIRandomLitmit(aIInteract.RedBrickHolder) == true)
            NavigateBridgeRay(RedAIPos);
        if (aIInteract.AIRandomLitmit(aIInteract.GreenBrickHolder) == true)
            NavigateBridgeRay(GreenAIPos);
        if (aIInteract.AIRandomLitmit(aIInteract.YellowBrickHolder) == true)
            NavigateBridgeRay(YellowAIPos);
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
                    aIInteract.DropBrick(gameObject.tag, aIInteract.RedGridBrickPos,aIInteract.RedBrickHolder,aIInteract.RedholderPos);
                    break;
                case GameConstant.GREEN_TAG:
                    aIInteract.DropBrick(gameObject.tag, aIInteract.GreenGridBrickPos, aIInteract.GreenBrickHolder, aIInteract.GreenholderPos);
                    break;
                case GameConstant.YELLOW_TAG:
                    aIInteract.DropBrick(gameObject.tag, aIInteract.YellowGridBrickPos, aIInteract.YellowBrickHolder, aIInteract.YellowholderPos);
                    break;
                default:
                    Debug.Log("Error DropBrickRay 2");
                    break;
            }            
        }
    }

    public void NavigateBridgeRay(Vector3 RayPos)
    {
        Ray ray = new Ray(RayPos, MovementRestrictDirection);
        RaycastHit hit;
        Debug.DrawRay(RayPos, MovementRestrictDirection);

        switch (gameObject.tag)
        {
            case GameConstant.RED_TAG:
                if (Physics.Raycast(ray, out hit, range, AIBridgeNav))
                {
                    Debug.Log("Red");
                }
                break;
            case GameConstant.GREEN_TAG:
                if (Physics.Raycast(ray, out hit, range, AIBridgeNav))
                {
                    Debug.Log("Green");
                }
                break;
            case GameConstant.YELLOW_TAG:
                if (Physics.Raycast(ray, out hit, range, AIBridgeNav))
                {
                    Debug.Log("Yellow");
                }
                break;
            default:
                Debug.Log("NavBridgeError");
                break;
        }      
    }
}
