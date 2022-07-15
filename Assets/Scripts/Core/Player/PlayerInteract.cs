using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : Singleton<PlayerInteract>
{
    public List<GameObject> BrickHolder;
    public List<Vector3> GridBrickPos;

    public GameObject Holder;

    ObjectPooling objPool;

    Vector3 holderPos;
    
    private void Start()
    {
        holderPos = Holder.transform.localPosition;
        holderPos = Holder.transform.localEulerAngles;

        BrickHolder = new List<GameObject>();
        GridBrickPos = new List<Vector3>();

        objPool = ObjectPooling.Ins;
    }

    public void AddBrick(GameObject Brick)
    {
        Debug.Log(Brick.transform.localPosition);
        GridBrickPos.Add(Brick.transform.localPosition);

        Brick.transform.parent = Holder.transform;

        holderPos.y += 0.07f;
        Brick.transform.localPosition = holderPos;
        Brick.transform.localEulerAngles = holderPos;

        BrickHolder.Add(Brick);
    }

    public void DropBrick(string tag)
    {
        
        GameObject lastElement = BrickHolder[BrickHolder.Count -1];
        Vector3 lastPosElement = GridBrickPos[GridBrickPos.Count - 1];

        BrickHolder[BrickHolder.Count - 1].transform.parent = null;

        objPool.Despawn(tag, lastElement);

        RemoveLastElement(lastElement,lastPosElement);

        objPool.Spawn(tag, lastPosElement, Quaternion.identity);

        holderPos.y -= 0.07f;
    }

    public void RemoveLastElement(GameObject lastElement, Vector3 lastPosElement)
    {
        BrickHolder.Remove(lastElement);
        GridBrickPos.Remove(lastPosElement);
    }
}
