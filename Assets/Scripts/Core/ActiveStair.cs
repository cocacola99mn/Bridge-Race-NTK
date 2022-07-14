using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveStair : MonoBehaviour
{
    public bool Active = false;
    private void Update()
    {
        Active = PlayerInteract.Ins.Walkable;
        Debug.Log(PlayerInteract.Ins.Walkable);
        if(Active == true)
        {
            gameObject.GetComponentInChildren<MeshRenderer>().enabled = true;
        }
    }
}
