using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    public GameObject AIBody;
    
    public int currentPoint = 0;
    public float AISpeed, TPRadius, turnVelocity ,turnTime;

    AITargetPoint aITargetPoint;
    
    private Animator MovementAnim;

    public void Start()
    {
        MovementAnim = GetComponent<Animator>();
        aITargetPoint = AITargetPoint.Ins;
        
        SetTarget();
        
        TPRadius = 0.1f;
        turnTime = 0.1f;
    }

    private void Update()
    {
        SetTarget();
    }

    public void AIMove(List<Vector3> target)
    {
        RunAnim();

        if (target.Count > 1)
        {
            if (Vector3.Distance(target[currentPoint], transform.position) < TPRadius)
            {
                currentPoint = Random.Range(0, 29);
            }
            AIRotation(target[currentPoint] - transform.position);
            transform.position = Vector3.MoveTowards(transform.position, target[currentPoint], AISpeed * Time.deltaTime);
        }
    }

    public void AIRotation(Vector3 direction)
    {
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnVelocity, turnTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }

    public void SetTarget()
    {
        switch (gameObject.tag)
        {
            case GameConstant.RED_TAG:
                AIMove(aITargetPoint.RedTarget);
                break;

            case GameConstant.GREEN_TAG:
                AIMove(aITargetPoint.GreenTarget);
                break;

            case GameConstant.YELLOW_TAG:
                AIMove(aITargetPoint.YellowTarget);
                break;

            default:
                Debug.Log("Can't find tag");
                break;
        }
    }

    protected void RunAnim()
    {
        MovementAnim.SetFloat(GameConstant.SPEED_PARA, 1);
    }

    protected void Idle()
    {
        MovementAnim.SetFloat(GameConstant.SPEED_PARA, 0.1f);
    }
}
