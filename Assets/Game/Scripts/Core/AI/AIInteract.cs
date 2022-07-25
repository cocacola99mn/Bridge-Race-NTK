using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIInteract : Singleton<AIInteract>
{
    public List<GameObject> RedBrickHolder,YellowBrickHolder, GreenBrickHolder;
    public List<Vector3> RedGridBrickPos, YellowGridBrickPos, GreenGridBrickPos;

    public GameObject RedHolder, YellowHolder, GreenHolder;

    ObjectPooling objPool;

    public float height = 0.07f;

    public Vector3 RedholderPos, GreenholderPos, YellowholderPos;

    private void Start()
    {       
        RedBrickHolder = new List<GameObject>();
        RedGridBrickPos = new List<Vector3>();
        
        GreenBrickHolder = new List<GameObject>();
        GreenGridBrickPos = new List<Vector3>();
        
        YellowBrickHolder = new List<GameObject>();
        YellowGridBrickPos = new List<Vector3>();

        objPool = ObjectPooling.Ins;
    }

    public void AddBrick(string tag ,GameObject Brick,List<Vector3> GridBrickPos, GameObject Holder, List<GameObject> BrickHolder)
    {
        GridBrickPos.Add(Brick.transform.localPosition);
        Brick.transform.parent = Holder.transform;

        switch (tag)
        {
            case GameConstant.RED_TAG:
                RedholderPos.y += height;
                Brick.transform.localPosition = RedholderPos;
                Brick.transform.localEulerAngles = RedholderPos;
                break;

            case GameConstant.GREEN_TAG:
                GreenholderPos.y += height;
                Brick.transform.localPosition = GreenholderPos;
                Brick.transform.localEulerAngles = GreenholderPos;
                break;

            case GameConstant.YELLOW_TAG:
                YellowholderPos.y += height;
                Brick.transform.localPosition = YellowholderPos;
                Brick.transform.localEulerAngles = YellowholderPos;
                break;

            default:
                Debug.Log("Add Brick Error");
                break;
        }
        
        BrickHolder.Add(Brick);
    }

    public void DropBrick(string tag, List<Vector3> GridBrickPos, List<GameObject> BrickHolder)
    {

        GameObject lastElement = BrickHolder[BrickHolder.Count - 1];
        Vector3 lastPosElement = GridBrickPos[GridBrickPos.Count - 1];

        BrickHolder[BrickHolder.Count - 1].transform.parent = null;

        objPool.Despawn(tag, lastElement);

        RemoveLastElement(lastElement, lastPosElement,GridBrickPos, BrickHolder);

        objPool.Spawn(tag, lastPosElement, Quaternion.identity);

        switch (tag)
        {
            case GameConstant.RED_TAG:
                RedholderPos.y -= height;
                break;

            case GameConstant.GREEN_TAG:
                GreenholderPos.y -= height;
                break;

            case GameConstant.YELLOW_TAG:
                YellowholderPos.y -= height;
                break;

            default:
                Debug.Log("ErrorNav");
                break;
        }
    }

    public void RemoveLastElement(GameObject lastElement, Vector3 lastPosElement, List<Vector3> GridBrickPos, List<GameObject> BrickHolder)
    {
        BrickHolder.Remove(lastElement);
        GridBrickPos.Remove(lastPosElement);
    }

    public bool AIHolderLitmit(List<GameObject> BrickHolder)
    {
        bool RaycastOn = false;

        if(BrickHolder.Count >= 13)
            RaycastOn = true;

        return RaycastOn;
    }
}
