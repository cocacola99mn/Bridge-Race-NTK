using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : Singleton<AIMovement>
{
    public GameObject AIBody;

    private float range = 10f;

    [SerializeField]
    private LayerMask AIBridgeNav;

    public Vector3 AIBodyPos,ForwardDirection;
    
    public int currentPoint, redMin,redMax,greenMin,greenMax,yellowMin,yellowMax;
    
    public float AISpeed, TPRadius, turnVelocity ,turnTime;

    AITargetPoint aITargetPoint;
    
    public Animator MovementAnim;

    public bool redReachLimit, greenReachLimit, yellowReachLimit;

    public void Start()
    {
        MovementAnim.enabled = false;
        ForwardDirection = Vector3.forward * range;
        aITargetPoint = AITargetPoint.Ins;
        
        SetTarget();
        
        TPRadius = 0.1f;
        turnTime = 0.1f;
        
        redReachLimit = false;
        greenReachLimit = false;
        yellowReachLimit = false;

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
        AIBodyPos = AIBody.transform.position;

        ReleaseRay();

        if (Time.timeScale > 0)
            MovementAnim.enabled = true;

        if (LevelManager.Ins.IsState(LevelState.Win) == false)
        {
            SetTarget();
        }            
        else
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
                    AIComeBridge(AIInteract.Ins.RedBrickHolder);
                break;

            case GameConstant.GREEN_TAG:
                if (greenReachLimit == false)
                    AIMove(aITargetPoint.GreenTarget,greenMin,greenMax);
                else
                    AIComeBridge(AIInteract.Ins.GreenBrickHolder);
                break;

            case GameConstant.YELLOW_TAG:
                if (yellowReachLimit == false)
                    AIMove(aITargetPoint.YellowTarget,yellowMin,yellowMax);
                else
                    AIComeBridge(AIInteract.Ins.YellowBrickHolder);
                break;

            default:
                Debug.Log("Can't find tag");
                break;
        }
    }

    protected void RunAnim()
    {
        MovementAnim.ResetTrigger(GameConstant.IDLE_ANIM);
        MovementAnim.SetTrigger(GameConstant.RUN_ANIM);
    }

    public void ReleaseRay()
    {
        switch (gameObject.tag)
        {
            case GameConstant.RED_TAG:
                if (AIInteract.Ins.AIHolderLitmit(AIInteract.Ins.RedBrickHolder) == true)
                    NavigateRay();
                break;

            case GameConstant.GREEN_TAG:
                if (AIInteract.Ins.AIHolderLitmit(AIInteract.Ins.GreenBrickHolder) == true)
                    NavigateRay();
                break;

            case GameConstant.YELLOW_TAG:
                if (AIInteract.Ins.AIHolderLitmit(AIInteract.Ins.YellowBrickHolder) == true)
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
}
