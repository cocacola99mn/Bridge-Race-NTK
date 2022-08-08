using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAction : MonoBehaviour
{
    [SerializeField]
    private LayerMask bridgeStairLayer;
    
    public GameObject Holder;
    
    Renderer selectChildRenderer;

    Transform select;

    [SerializeField]
    private Material BrickMaterial;

    public Stack<GameObject> BrickHolder;
    public Stack<Vector3> GridBrickPos;   

    ObjectPooling objPool;

    public float height = 0.07f, range = 10;

    public Vector3 holderPos, RayPosition, DropBrickDirection;

    void Start()
    {
        BrickHolder = new Stack<GameObject>();
        GridBrickPos = new Stack<Vector3>();

        objPool = ObjectPooling.Ins;

        holderPos = Holder.transform.localPosition;
        
        DropBrickDirection = Quaternion.Euler(-50, 0, 0) * Vector3.down * range;
    }

    private void Update()
    {
        RayPosition = transform.position;

        DropBrickRay();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(gameObject.tag))
            AddBrick(other.transform);
    }

    public void AddBrick(Transform Brick)
    {
        GridBrickPos.Push(Brick.localPosition);

        Brick.SetParent(Holder.transform);

        holderPos.y += height;
        Brick.localPosition = holderPos;
        Brick.localEulerAngles = holderPos;

        BrickHolder.Push(Brick.gameObject);
    }

    public void DropBrick(string tag)
    {
        GameObject lastElement = BrickHolder.Pop();
        Vector3 lastPosElement = GridBrickPos.Pop();

        lastElement.transform.SetParent(null);

        objPool.Despawn(tag, lastElement);
        objPool.Spawn(tag, lastPosElement, Quaternion.identity);

        holderPos.y -= height;
    }

    public void DropBrickRay()
    {
        Ray ray = new Ray(RayPosition, DropBrickDirection);
        RaycastHit hit;
        Debug.DrawRay(RayPosition, DropBrickDirection);

        if (Physics.Raycast(ray, out hit, range, bridgeStairLayer))
            BuildBridge(hit);
    }

    public void BuildBridge(RaycastHit hit)
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
                    Debug.Log("None");
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
            DropBrick(gameObject.tag);
        }
    }

    public void OnFall()
    {
        for (int i = BrickHolder.Count; i > 0; i--)
        {
            BrickHolder.Peek().transform.SetParent(null);
            objPool.Despawn(tag, BrickHolder.Pop());
            objPool.Spawn(tag, GridBrickPos.Pop(), Quaternion.identity);

            holderPos.y -= height;
        }
    }
}
