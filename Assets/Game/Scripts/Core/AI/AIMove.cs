using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMove : MonoBehaviour
{
    public AIAction aIAction;

    [SerializeField]
    private LayerMask AIBridgeNav, stairLayer;
    AITargetPoint aITargetPoint;

    public Animator MovementAnim;
    public Collider AICollider;
    public Rigidbody AIRigidbody;

    Vector3 RayPosition,ForwardDirection,OnRoadRay, ForwardMovement;
    
    public int currentPoint,minPoint, maxPoint;
    
    public float TPRadius, AISpeed, turnVelocity, turnTime, range, FallTime, FallRealTime;   

    public bool reachLimit,IsOnBridge,fall;

    void Start()
    {
        aITargetPoint = AITargetPoint.Ins;
        MovementAnim.enabled = false;

        AISpeed = 1.5f;
        TPRadius = 0.1f;
        turnTime = 0.2f;
        range = 10;
        FallTime = 5;

        minPoint = 0;
        maxPoint = 29;
        
        reachLimit = false;
        IsOnBridge = false;
        fall = false;

        ForwardDirection = Vector3.forward * range;
        OnRoadRay = -Vector3.up * range;
        ForwardMovement = Vector3.forward * AISpeed * Time.deltaTime;
    }

    void Update()
    {
        if (fall && Time.time >= FallRealTime)
        {
            fall = false;
            AICollider.enabled = true;
            AIRigidbody.constraints = RigidbodyConstraints.None;
        }

        if (Time.timeScale > 0)
            MovementAnim.enabled = true;

        RayPosition = transform.position;
        SetTarget();
        ReleaseRay();
        ShootOnStairRay();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GameConstant.STATECHANGER_TAG) && LevelManager.Ins.IsState(LevelState.SecondState) == true)
            AIChangeState(30, 59);

        if (other.CompareTag(GameConstant.STATECHANGER_TAG) && LevelManager.Ins.IsState(LevelState.ThirdState) == true)
            AIChangeState(60, 89);
    }

    private void OnCollisionEnter(Collision other)
    {
        if(IsOnBridge == false)
        {
            if (other.gameObject.CompareTag(GameConstant.BLUE_TAG))
            {
                if (aIAction.BrickHolder.Count < PlayerInteract.Ins.BrickHolder.Count)
                    Fall();
            }
            else
            {
                try
                {
                    int otherCounter = other.gameObject.GetComponent<AIAction>().BrickHolder.Count;
                    if (fall == false)
                        FallCondition(other.gameObject, aIAction.BrickHolder.Count, otherCounter);
                }
                catch
                {
                }
            }
        }        
    }

    public void AIMovement(List<Vector3> target, int min, int max)
    {
        if (target.Count > 1 && reachLimit == false && IsOnBridge == false && fall == false)
        {
            RunAnim();
            if (Vector3.Distance(target[currentPoint], transform.position) < TPRadius)
            {
                currentPoint = Random.Range(min, max);
            }
            AIRotation(target[currentPoint] - transform.position);
            transform.position = Vector3.MoveTowards(transform.position, target[currentPoint], AISpeed * Time.deltaTime);
        }
        else if (fall == false)
        {
            AIComeBridge();
        }
    }

    public void AIRotation(Vector3 direction)
    {
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnVelocity, turnTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }

    public void AIComeBridge()
    {
        if (aIAction.BrickHolder.Count == 0)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
            gameObject.transform.Translate(ForwardMovement);
            
            reachLimit = false;
        }            
        else
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            gameObject.transform.Translate(ForwardMovement);
        }
    }

    public void SetTarget()
    {
        switch (gameObject.tag)
        {
            case GameConstant.RED_TAG:
                AIMovement(aITargetPoint.RedTarget, minPoint, maxPoint);
                break;

            case GameConstant.GREEN_TAG:
                AIMovement(aITargetPoint.GreenTarget, minPoint, maxPoint);
                break;

            case GameConstant.YELLOW_TAG:
                AIMovement(aITargetPoint.YellowTarget, minPoint, maxPoint);
                break;

            default:
                Debug.Log("ErrorNav");
                break;
        }
    }

    public void ReleaseRay()
    {
        if (AIHolderLitmit(aIAction.BrickHolder) == true)
            NavigateRay();
    }

    public void NavigateRay()
    {
        Ray ray = new Ray(RayPosition, ForwardDirection);
        Debug.DrawRay(RayPosition, ForwardDirection);
        
        if (Physics.Raycast(ray, range, AIBridgeNav))
            reachLimit = true;
    }

    public void ShootOnStairRay()
    {
        Ray ray = new Ray(RayPosition, OnRoadRay);
        Debug.DrawRay(RayPosition, OnRoadRay);

        if (Physics.Raycast(ray, range, stairLayer))         
            IsOnBridge = true;
        else
            IsOnBridge = false;
    }

    public bool AIHolderLitmit(Stack<GameObject> BrickHolder)
    {
        bool RaycastOn = false;

        if (BrickHolder.Count >= 13)
            RaycastOn = true;

        return RaycastOn;
    }

    public void AIChangeState(int minNum, int maxNum)
    {
        minPoint = minNum;
        maxPoint = maxNum;
        currentPoint = Random.Range(minPoint, maxPoint);
        reachLimit = false;
    }

    public void FallCondition(GameObject other, int counter, int otherCounter)
    {
        switch (other.tag)
        {
            case GameConstant.BLUE_TAG:                                
                break;
            default:
                if (counter < otherCounter)
                    Fall();
                break;
        }
    }

    public void Fall()
    {
        FallRealTime = Time.time + FallTime;

        MovementAnim.SetTrigger(GameConstant.FALL_ANIM);
        MovementAnim.SetTrigger(GameConstant.KIPUP_ANIM);

        aIAction.OnFall();
        AICollider.enabled = false;
        AIRigidbody.constraints = RigidbodyConstraints.FreezeAll;

        fall = true;
    }

    protected void RunAnim()
    {
        MovementAnim.ResetTrigger(GameConstant.IDLE_ANIM);
        MovementAnim.SetTrigger(GameConstant.RUN_ANIM);
    }
}
