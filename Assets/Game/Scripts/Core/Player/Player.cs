using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    [SerializeField] 
    protected CharacterController controller;

    public float PlayerSpeed;

    public float turnTime = 0.1f;

    float turnVelocity, horizontal, vertical;

    protected Vector3 direction;

    public Animator MovementAnim;
    public Animation DancingAnim;

    public bool MoveForwardRestrict,MoveBackRestrict,OnFinish;

    private void Start()
    {
        MoveForwardRestrict = MoveBackRestrict = false;

        OnFinish = false;
    }

    private void Update()
    {
        if (OnFinish == false)
            PlayerMovement();
        else
            Dancing();
    }
    
    public void PlayerMovement()
    {
        KeyboardInput();

        JoyStickInput();

        if ((direction - Vector3.zero).sqrMagnitude < 0.001f)
            Idle();
        else 
            RunAnim();
               
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

    public void RunAnim()
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
