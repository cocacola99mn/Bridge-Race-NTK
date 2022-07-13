using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickAdd : MonoBehaviour
{
    public static string BLUE_TAG = "Blue";
    public static string PLAYER_TAG = "Player";

    public GameObject Holder;

    [SerializeField]
    private Brick brickColor;
    
    private void OnTriggerEnter(Collider other)
    {
        if(string.Equals(brickColor.name,BLUE_TAG) && other.CompareTag(PLAYER_TAG))
        {
            PlayerInteract.Ins.AddBrick(gameObject);
        }           
    }


}
