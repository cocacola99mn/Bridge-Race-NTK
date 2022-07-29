using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    AIInteract aIInteract;
    PlayerInteract interact;

    [SerializeField] 
    protected CharacterController controller;

    public float PlayerSpeed;

    public float turnTime = 0.1f;

    float turnVelocity, horizontal, vertical;

    protected Vector3 direction;
    private float FallTime;

    public Animator MovementAnim;
    public Animation DancingAnim;

    public bool MoveForwardRestrict,MoveBackRestrict,OnFinish, Collided;

    private void Start()
    {
        MoveForwardRestrict = MoveBackRestrict = false;

        OnFinish = false;
        Collided = false;

        interact = PlayerInteract.Ins;
        aIInteract = AIInteract.Ins;

    }

    private void FixedUpdate()
    {
        if (Collided && Time.time >= FallTime)
        {
            controller.enabled = true;
            Collided = false;
        }
              
            

        if (OnFinish == false && Collided == false)
            PlayerMovement();

        if (OnFinish == true)
            Dancing();
    }
    
    public void PlayerMovement()
    {
        KeyboardInput();

        JoyStickInput();

        if ((direction - Vector3.zero).sqrMagnitude < 0.001f)
            Idle();
        else 
            Run();
               
        if (direction.magnitude >= 0.1f)
        {
            PlayerRotation(direction);
            controller.Move(direction * PlayerSpeed * Time.deltaTime);
        }
    }

    public void JoyStickInput()
    {
        horizontal = JoystickInput.Ins.inputHorizontal();
        vertical = JoystickInput.Ins.inputVertical();
        
        direction = new Vector3(horizontal, 0, vertical).normalized;
        
        if(vertical > 0 && MoveForwardRestrict == true)
            direction = Vector3.zero;

        if (vertical < 0 && MoveBackRestrict == true)
            direction = Vector3.zero;

    }

    public void KeyboardInput()
    {
        horizontal = Input.GetAxis(GameConstant.HORIZONTAL_AXIS);
        vertical = Input.GetAxis(GameConstant.VERTICAL_AXIS);

        direction = new Vector3(horizontal, 0, vertical).normalized;

        if (vertical > 0 && MoveForwardRestrict == true)
            direction = Vector3.zero;

        if (vertical < 0 && MoveBackRestrict == true)
            direction = Vector3.zero;
    }

    public void PlayerRotation(Vector3 direction)
    {
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnVelocity, turnTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }

    private void OnCollisionEnter(Collision other)
    {
        if(Collided == false)
            FallCondition(other.gameObject);
    }

    public void FallCondition(GameObject other)
    {
        switch (other.tag)
        {
            case GameConstant.RED_TAG:
                if (interact.BrickHolder.Count < aIInteract.RedBrickHolder.Count)
                    Fall();
                break;
            case GameConstant.GREEN_TAG:
                if (interact.BrickHolder.Count < aIInteract.GreenBrickHolder.Count)
                    Fall();
                break;
            case GameConstant.YELLOW_TAG:
                if (interact.BrickHolder.Count < aIInteract.YellowBrickHolder.Count)
                    Fall();
                break;
            default:
                Debug.Log("Error Fall Condition");
                break;
        }
    }

    public void Run()
    {
        MovementAnim.ResetTrigger(GameConstant.IDLE_ANIM);
        MovementAnim.SetTrigger(GameConstant.RUN_ANIM);
    }

    public void Idle()
    {
        MovementAnim.ResetTrigger(GameConstant.RUN_ANIM);
        MovementAnim.SetTrigger(GameConstant.IDLE_ANIM);
    }

    public void Dancing()
    {
        DancingAnim.Play(GameConstant.DANCE_ANIM);
    }

    public void Fall()
    {
        FallTime = Time.time + 5f;
        interact.OnFall(GameConstant.BLUE_TAG);
        MovementAnim.SetTrigger(GameConstant.FALL_ANIM);
        MovementAnim.SetTrigger(GameConstant.KIPUP_ANIM);
        controller.enabled = false;
        Collided = true;        
    }
}
