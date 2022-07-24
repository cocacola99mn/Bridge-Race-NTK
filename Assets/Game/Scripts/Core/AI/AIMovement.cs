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
    
    private Animator MovementAnim;

    public bool redReachLimit, greenReachLimit, yellowReachLimit;
    
    public void Start()
    {
        ForwardDirection = Vector3.forward * range;
        MovementAnim = GetComponent<Animator>();
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
        //AIOnStateChange();
        SetTarget();
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
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, 180, ref turnVelocity, turnTime);
            gameObject.transform.rotation = Quaternion.Euler(0f, angle, 0f);
            gameObject.transform.Translate(Vector3.forward * AISpeed * Time.deltaTime);
            
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
                    Debug.Log("ErrorNav");
                    break;
            }
        }
        else
        {
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, 0, ref turnVelocity, turnTime);
            gameObject.transform.rotation = Quaternion.Euler(0f, angle, 0f);
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
        MovementAnim.SetFloat(GameConstant.SPEED_PARA, 1);
    }

    protected void Idle()
    {
        MovementAnim.SetFloat(GameConstant.SPEED_PARA, 0.1f);
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
        if(other.CompareTag(GameConstant.STATECHANGER_TAG) &&  AIManager.Ins.IsRedState(RedAIState.SecondState) == true)
        {
            redMin = 30;
            redMax = 59;
            currentPoint = Random.Range(redMin,redMax);
            redReachLimit = false;
        }
        
        if (other.CompareTag(GameConstant.STATECHANGER_TAG) && AIManager.Ins.IsRedState(RedAIState.ThirdState) == true)
        {
            redMin = 60;
            redMax = 89;
            currentPoint = Random.Range(redMin, redMax);
            redReachLimit = false;
        }

        if (other.CompareTag(GameConstant.STATECHANGER_TAG) && AIManager.Ins.IsGreenState(GreenAIState.SecondState) == true)
        {
            greenMin = 30;
            greenMax = 59;
            currentPoint = Random.Range(greenMin, greenMax);
            greenReachLimit = false;
        }
        
        if (other.CompareTag(GameConstant.STATECHANGER_TAG) && AIManager.Ins.IsGreenState(GreenAIState.ThirdState) == true)
        {
            greenMin = 60;
            greenMax = 89;
            currentPoint = Random.Range(greenMin, greenMax);
            greenReachLimit = false;
        }

        if (other.CompareTag(GameConstant.STATECHANGER_TAG) && AIManager.Ins.IsYellowState(YellowAIState.SecondState) == true)
        {
            yellowMin = 30;
            yellowMax = 59;
            currentPoint = Random.Range(yellowMin, yellowMax);
            yellowReachLimit = false;
        }
        
        if (other.CompareTag(GameConstant.STATECHANGER_TAG) && AIManager.Ins.IsYellowState(YellowAIState.ThirdState) == true)
        {
            yellowMin = 60;
            yellowMax = 89;
            currentPoint = Random.Range(yellowMin, yellowMax);
            yellowReachLimit = false;
        }
    }
}
