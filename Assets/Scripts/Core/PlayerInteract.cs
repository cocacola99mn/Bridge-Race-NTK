using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : Singleton<PlayerInteract>
{
    public List<GameObject> BrickHolder;

    public GameObject Holder;

    ObjectPooling objPool;

    Vector3 holderPos;

    private void Start()
    {
        holderPos = Holder.transform.localPosition;
        holderPos = Holder.transform.localEulerAngles;

        BrickHolder = new List<GameObject>();

        objPool = ObjectPooling.Ins;
    }

    public void AddBrick(GameObject Brick)
    {
        Brick.transform.parent = Holder.transform;
        
        holderPos.y += 0.07f;
        Brick.transform.localPosition = holderPos;
        Brick.transform.localEulerAngles = holderPos;

        BrickHolder.Add(Brick);
    }

    public void DropBrick(string tag)
    {
        GameObject lastElement = BrickHolder[BrickHolder.Count -1];

        BrickHolder[BrickHolder.Count - 1].transform.parent = null;

        objPool.Despawn(tag, lastElement);

        BrickHolder.Remove(lastElement);

        holderPos.y -= 0.07f;
    }
}
