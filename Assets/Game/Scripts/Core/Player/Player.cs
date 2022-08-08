using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    PlayerInteract interact;

    [SerializeField] 
    protected CharacterController controller;

    public Joystick joystick;
    public float PlayerSpeed;

    public float turnTime = 0.2f;

    float turnVelocity, horizontal, vertical;

    protected Vector3 direction;
    private float FallTime;

    public Animator MovementAnim;
    public Animation DancingAnim;

    public bool MoveForwardRestrict,MoveBackRestrict,OnFinish, fall, IsOnBridge;

    private void Start()
    {
        MoveForwardRestrict = MoveBackRestrict = false;

        OnFinish = false;
        fall = false;

        interact = PlayerInteract.Ins;
    }

    private void FixedUpdate()
    {
        FallAnimTime();
        
        if (OnFinish == false && fall == false)
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
        horizontal = joystick.Horizontal;
        vertical = joystick.Vertical;
        
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
        try
        {
            int otherCounter = other.gameObject.GetComponent<AIAction>().BrickHolder.Count;
            if (fall == false)
                FallCondition(otherCounter);
        }
        catch
        {
            Debug.Log("Can't find");
        }
    }

    public void FallCondition(int otherCounter)
    {
        if (interact.BrickHolder.Count < otherCounter)
            Fall();
    }

    public void Fall()
    {
        FallTime = Time.time + 5f;
        interact.OnFall(GameConstant.BLUE_TAG);
        MovementAnim.SetTrigger(GameConstant.FALL_ANIM);
        MovementAnim.SetTrigger(GameConstant.KIPUP_ANIM);
        controller.enabled = false;
        fall = true;        
    }

    public void FallAnimTime()
    {
        if (fall && Time.time >= FallTime)
        {
            controller.enabled = true;
            fall = false;
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
}
