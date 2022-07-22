using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickAdd : MonoBehaviour
{
    [SerializeField]
    private AllColor brickColor;
    private AIInteract aIInteract;

    private void Start()
    {
        aIInteract = AIInteract.Ins;     
    }

    private void OnTriggerEnter(Collider other)
    {
        if (string.Equals(brickColor.color, GameConstant.BLUE_TAG) && other.CompareTag(GameConstant.BLUE_TAG))
        {
            PlayerInteract.Ins.AddBrick(gameObject);
        }

        if (string.Equals(brickColor.color, GameConstant.RED_TAG) && other.CompareTag(GameConstant.RED_TAG))
        {
            AIInteract.Ins.AddBrick(gameObject,aIInteract.RedGridBrickPos,aIInteract.RedHolder,aIInteract.RedholderPos,aIInteract.RedBrickHolder);
        }
    }
}
