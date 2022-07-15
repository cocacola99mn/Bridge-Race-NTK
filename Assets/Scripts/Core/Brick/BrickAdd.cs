using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickAdd : MonoBehaviour
{
    [SerializeField]
    private Brick brickColor;
    
    private void OnTriggerEnter(Collider other)
    {
        if(string.Equals(brickColor.name,GameConstant.BLUE_TAG) && other.CompareTag(GameConstant.PLAYER_TAG))
        {
            PlayerInteract.Ins.AddBrick(gameObject);
        }           
    }
}
