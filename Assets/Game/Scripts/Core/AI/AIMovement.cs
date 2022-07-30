using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : Singleton<AIMovement>
{
    public GameObject AIBody;
    public Collider AICollider;

    private float range = 10f;

    [SerializeField]
    private LayerMask AIBridgeNav, roadLayer;

    public Vector3 AIBodyPos,ForwardDirection,OnRoadRay;
    
    public int currentPoint, redMin,redMax,greenMin,greenMax,yellowMin,yellowMax;
    
    public float AISpeed, TPRadius, turnVelocity ,turnTime;

    AITargetPoint aITargetPoint;
    AIInteract aIInteract;
    
    public Animator MovementAnim;

    public bool redReachLimit, greenReachLimit, yellowReachLimit, Collided, IsOnBridge;

    private float FallTime;

    public void Start()
    {
        MovementAnim.enabled = false;
        ForwardDirection = Vector3.forward * range;
        OnRoadRay = -Vector3.up * range;
        
        aITargetPoint = AITargetPoint.Ins;
        aIInteract = AIInteract.Ins;
        
        SetTarget();
        
        TPRadius = 0.1f;
        turnTime = 0.1f;
        
        redReachLimit = false;
        greenReachLimit = false;
        yellowReachLimit = false;
        Collided = false;

        currentPoint = 0;
        redMin = 0;
        redMax = 29;
        greenMin = 0;
        greenMax = 29;
        yellowMin = 0;
        yellowMax = 29;
    }

    private void FixedUpdate()
    {       
        if (Collided && Time.time >= FallTime)
        {
            Collided = false;
        }                

        AIBodyPos = AIBody.transform.position;
        ReleaseRay();
        ShootOnRoadRay();

        if (Time.timeScale > 0)
            MovementAnim.enabled = true;

        if (LevelManager.Ins.IsState(LevelState.Win) == false && Collided == false)
            SetTarget();        
        else if(LevelManager.Ins.IsState(LevelState.Win) == true)
            gameObject.SetActive(false);
    }

    public void AIMove(List<Vector3> target,int min, int max)
    {
        if (target.Count > 1)
        {
            RunAnim();
            if (Vector3.Distance(target[currentPoint], transform.position) < TPRadius)
            {
                currentPoint = Random.Range(min, max);
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

    public void AIComeBridge(List<GameObject> BrickHolder)
    {
        if(BrickHolder.Count == 0)
        {            
            switch (gameObject.tag)
            {
                case GameConstant.RED_TAG:
                    redReachLimit = false;
                    break;

                case GameConstant.GREEN_TAG:
                    greenReachLimit = false;
                    break;

                case GameConstant.YELLOW_TAG:
                    yellowReachLimit = false;
                    break;

                default:
                    break;
            }
        }
        else
        {
            gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            gameObject.transform.Translate(Vector3.forward * AISpeed * Time.deltaTime);
        }        
    }

    public void SetTarget()
    {
        switch (gameObject.tag)
        {
            case GameConstant.RED_TAG:
                if (redReachLimit == false)
                    AIMove(aITargetPoint.RedTarget,redMin,redMax);
                else
                    AIComeBridge(aIInteract.RedBrickHolder);
                break;

            case GameConstant.GREEN_TAG:
                if (greenReachLimit == false)
                    AIMove(aITargetPoint.GreenTarget,greenMin,greenMax);
                else
                    AIComeBridge(aIInteract.GreenBrickHolder);
                break;

            case GameConstant.YELLOW_TAG:
                if (yellowReachLimit == false)
                    AIMove(aITargetPoint.YellowTarget,yellowMin,yellowMax);
                else
                    AIComeBridge(aIInteract.YellowBrickHolder);
                break;

            default:
                Debug.Log("Can't find tag");
                break;
        }
    }

    public void ReleaseRay()
    {
        switch (gameObject.tag)
        {
            case GameConstant.RED_TAG:
                if (aIInteract.AIHolderLitmit(aIInteract.RedBrickHolder) == true)
                    NavigateRay();
                break;

            case GameConstant.GREEN_TAG:
                if (aIInteract.AIHolderLitmit(aIInteract.GreenBrickHolder) == true)
                    NavigateRay();
                break;

            case GameConstant.YELLOW_TAG:
                if (aIInteract.AIHolderLitmit(aIInteract.YellowBrickHolder) == true)
                    NavigateRay();
                break;

            default:
                Debug.Log("ErrorNav");
                break;
        }
    }
    
    public void NavigateRay()
    {
        Ray redRay = new Ray(AIBodyPos, ForwardDirection);
        RaycastHit redHit;
        Debug.DrawRay(AIBodyPos, ForwardDirection);
        if (Physics.Raycast(redRay, out redHit, range, AIBridgeNav))
        {
            switch (gameObject.tag)
            {
                case GameConstant.RED_TAG:
                    redReachLimit = true;
                    break;

                case GameConstant.GREEN_TAG:
                    greenReachLimit = true;
                    break;

                case GameConstant.YELLOW_TAG:
                    yellowReachLimit = true;
                    break;

                default:
                    Debug.Log("ErrorNav");
                    break;
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        switch (gameObject.tag)
        {
            case GameConstant.RED_TAG:
                if (other.CompareTag(GameConstant.STATECHANGER_TAG) && AIManager.Ins.IsRedState(RedAIState.SecondState) == true)
                {
                    AIRedChangeStateTarget(30, 59);
                    redReachLimit = false;
                }

                if (other.CompareTag(GameConstant.STATECHANGER_TAG) && AIManager.Ins.IsRedState(RedAIState.ThirdState) == true)
                {
                    AIRedChangeStateTarget(60, 89);
                    redReachLimit = false;
                }
                break;

            case GameConstant.GREEN_TAG:
                if (other.CompareTag(GameConstant.STATECHANGER_TAG) && AIManager.Ins.IsGreenState(GreenAIState.SecondState) == true)
                {
                    AIGreenChangeStateTarget(30, 59);
                    greenReachLimit = false;
                }

                if (other.CompareTag(GameConstant.STATECHANGER_TAG) && AIManager.Ins.IsGreenState(GreenAIState.ThirdState) == true)
                {
                    AIGreenChangeStateTarget(60, 89);
                    greenReachLimit = false;
                }
                break;

            case GameConstant.YELLOW_TAG:
                if (other.CompareTag(GameConstant.STATECHANGER_TAG) && AIManager.Ins.IsYellowState(YellowAIState.SecondState) == true)
                {
                    AIYellowChangeStateTarget(30, 59);
                    yellowReachLimit = false;
                }

                if (other.CompareTag(GameConstant.STATECHANGER_TAG) && AIManager.Ins.IsYellowState(YellowAIState.ThirdState) == true)
                {
                    AIYellowChangeStateTarget(60, 89);
                    yellowReachLimit = false;
                }
                break;

            default:
                Debug.Log("ErrorTrigger");
                break;
        }        
    }

    public void AIRedChangeStateTarget(int minNum, int maxNum)
    {
        redMin = minNum;
        redMax = maxNum;
        currentPoint = Random.Range(redMin, redMax);
    }

    public void AIGreenChangeStateTarget(int minNum, int maxNum)
    {
        greenMin = minNum;
        greenMax = maxNum;
        currentPoint = Random.Range(greenMin, greenMax);
    }

    public void AIYellowChangeStateTarget(int minNum, int maxNum)
    {
        yellowMin = minNum;
        yellowMax = maxNum;
        currentPoint = Random.Range(yellowMin, yellowMax);
    }

    public void OnCollisionEnter(Collision other)
    {
        if (Collided == false)
            FallCondition(other.gameObject);
    }

    public void FallCondition(GameObject other)
    {
        switch (gameObject.tag)
        {
            case GameConstant.RED_TAG:
                if(IsOnBridge == false)
                    FallCondition2(GameConstant.RED_TAG,other, aIInteract.RedBrickHolder.Count);
                break;

            case GameConstant.GREEN_TAG:
                if (IsOnBridge == false)
                    FallCondition2(GameConstant.GREEN_TAG,other, aIInteract.GreenBrickHolder.Count);
                break;

            case GameConstant.YELLOW_TAG:
                if (IsOnBridge == false)
                    FallCondition2(GameConstant.YELLOW_TAG,other, aIInteract.YellowBrickHolder.Count);
                break;

            default:
                Debug.Log("Error Fall Object");
                break;
        }
    }

    public void FallCondition2(string tag,GameObject other, int counter)
    {
        switch (other.tag)
        {
            case GameConstant.BLUE_TAG:
                if (counter < PlayerInteract.Ins.BrickHolder.Count)
                    Fall(tag);
                break;

            case GameConstant.RED_TAG:
                if (counter < aIInteract.RedBrickHolder.Count)
                    Fall(tag);
                break;

            case GameConstant.GREEN_TAG:
                if (counter < aIInteract.GreenBrickHolder.Count)
                    Fall(tag);
                break;

            case GameConstant.YELLOW_TAG:
                if (counter < aIInteract.YellowBrickHolder.Count)
                    Fall(tag);
                break;

            default:
                break;
        }
    }

    protected void RunAnim()
    {
        MovementAnim.ResetTrigger(GameConstant.IDLE_ANIM);
        MovementAnim.SetTrigger(GameConstant.RUN_ANIM);
    }

    public void Fall(string tag)
    {
        FallTime = Time.time + 5f;
        
        MovementAnim.SetTrigger(GameConstant.FALL_ANIM);
        MovementAnim.SetTrigger(GameConstant.KIPUP_ANIM);
        
        aIInteract.OnFall(tag);
        
        Collided = true;
    }

    public void ShootOnRoadRay()
    {
        Ray ray = new Ray(AIBodyPos, OnRoadRay);
        RaycastHit hit;
        Debug.DrawRay(AIBodyPos, OnRoadRay);

        if (Physics.Raycast(ray, out hit, range, roadLayer))
        {
            AICollider.enabled = false;
            IsOnBridge = true;
        }
        else
        {
            AICollider.enabled = true;
            IsOnBridge = false;
        }
            
    }
}
