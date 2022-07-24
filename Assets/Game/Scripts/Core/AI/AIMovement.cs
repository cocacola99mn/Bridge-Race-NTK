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
    
    public int currentPoint = 0;
    
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
    }

    private void Update()
    {
        AIBodyPos = AIBody.transform.position;

        ReleaseRay();
        SetTarget();
    }

    public void AIMove(List<Vector3> target)
    {
        if (target.Count > 1)
        {
            RunAnim();
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

    public void AIComeBridge(List<GameObject> BrickHolder)
    {
        if(BrickHolder.Count == 0)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
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
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            gameObject.transform.Translate(Vector3.forward * AISpeed * Time.deltaTime);
        }        
    }

    public void SetTarget()
    {
        switch (gameObject.tag)
        {
            case GameConstant.RED_TAG:
                if (redReachLimit == false)
                    AIMove(aITargetPoint.RedTarget);
                else
                    AIComeBridge(AIInteract.Ins.RedBrickHolder);
                break;

            case GameConstant.GREEN_TAG:
                if (greenReachLimit == false)
                    AIMove(aITargetPoint.GreenTarget);
                else
                    AIComeBridge(AIInteract.Ins.GreenBrickHolder);
                break;

            case GameConstant.YELLOW_TAG:
                if (yellowReachLimit == false)
                    AIMove(aITargetPoint.YellowTarget);
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
}
