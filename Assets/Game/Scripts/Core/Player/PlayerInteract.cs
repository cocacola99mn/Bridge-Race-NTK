using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : Singleton<PlayerInteract>
{
    public List<GameObject> BrickHolder;
    public List<Vector3> GridBrickPos;

    public GameObject Holder;
    public float height = 0.07f;

    ObjectPooling objPool;

    Vector3 holderPos;
    
    private void Start()
    {
        holderPos = Holder.transform.localPosition;

        BrickHolder = new List<GameObject>();
        GridBrickPos = new List<Vector3>();

        objPool = ObjectPooling.Ins;
    }

    public void AddBrick(GameObject Brick)
    {
        GridBrickPos.Add(Brick.transform.localPosition);

        Brick.transform.SetParent(Holder.transform);

        holderPos.y += height;
        Brick.transform.localPosition = holderPos;
        Brick.transform.localEulerAngles = holderPos;

        BrickHolder.Add(Brick);
    }

    public void DropBrick(string tag)
    {
        
        GameObject lastElement = BrickHolder[BrickHolder.Count -1];
        Vector3 lastPosElement = GridBrickPos[GridBrickPos.Count - 1];

        BrickHolder[BrickHolder.Count - 1].transform.SetParent(null);

        objPool.Despawn(tag, lastElement);
        objPool.Spawn(tag, lastPosElement, Quaternion.identity);

        RemoveLastElement(lastElement,lastPosElement);

        holderPos.y -= height;
    }

    public void RemoveLastElement(GameObject lastElement, Vector3 lastPosElement)
    {
        BrickHolder.Remove(lastElement);
        GridBrickPos.Remove(lastPosElement);
    }

    public void RemoveAllElement()
    {
        Holder.SetActive(false);
    }

    public void OnFall(string tag)
    {
        for(int i = BrickHolder.Count - 1; i >= 0; i--)
        {
            BrickHolder[i].transform.SetParent(null);
            objPool.Despawn(tag, BrickHolder[i]);
            objPool.Spawn(tag, GridBrickPos[i], Quaternion.identity);
            
            holderPos.y -= height;
        }

        GridBrickPos.Clear();
        BrickHolder.Clear();
    }
}
