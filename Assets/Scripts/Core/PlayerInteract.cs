using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : Singleton<PlayerInteract>
{
    public List<GameObject> BrickHolder;

    public GameObject Holder;
    Vector3 holderPos;

    private void Start()
    {
        holderPos = Holder.transform.localPosition;
        holderPos = Holder.transform.localEulerAngles;
        BrickHolder = new List<GameObject>();
    }

    public void AddBrick(GameObject Brick)
    {
        Brick.transform.SetParent(Holder.transform);

        holderPos.y += 0.07f;
        Brick.transform.localPosition = holderPos;
        Brick.transform.localEulerAngles = holderPos;

        BrickHolder.Add(Brick);
    }
}
