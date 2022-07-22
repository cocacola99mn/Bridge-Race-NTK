using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    public GameObject AIBody;
    
    public int currentPoint = 0;
    public Vector3 targetPoint;
    public float AISpeed,TPRadius;

    AITargetPoint aITargetPoint;

    public void Start()
    {
        aITargetPoint = AITargetPoint.Ins;
        SetTarget();
        TPRadius = 0.1f;
        
    }

    private void Update()
    {
        if(aITargetPoint.RedTarget.Count > 1)
        {
            if (Vector3.Distance(aITargetPoint.RedTarget[currentPoint], transform.position) < TPRadius)
            {
                currentPoint = Random.Range(0, aITargetPoint.RedTarget.Count);
            }
            transform.position = Vector3.MoveTowards(transform.position, aITargetPoint.RedTarget[currentPoint], AISpeed * Time.deltaTime);
        }       
    }

    public void AIMove()
    {
        
    }
    
    public void SetTarget()
    {
        switch (gameObject.tag)
        {
            case GameConstant.RED_TAG:
                break;

            case GameConstant.GREEN_TAG:

                break;

            case GameConstant.YELLOW_TAG:

                break;

            default:
                Debug.Log("Can't find tag");
                break;
        }
    }
}
