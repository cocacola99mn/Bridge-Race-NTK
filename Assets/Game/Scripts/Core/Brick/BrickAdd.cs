using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickAdd : AIAction
{
    [SerializeField]
    private AllColor brickColor;

    private void OnTriggerEnter(Collider other)
    {
        if (string.Equals(brickColor.color, GameConstant.BLUE_TAG) && other.CompareTag(GameConstant.BLUE_TAG))
        {
            PlayerInteract.Ins.AddBrick(gameObject);
        }
    }
}
