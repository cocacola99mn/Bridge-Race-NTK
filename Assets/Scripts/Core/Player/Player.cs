using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    [SerializeField] 
    CharacterController controller;

    public float PlayerSpeed;

    public float turnTime = 0.1f;
    
    float turnVelocity, horizontal, vertical;

    Vector3 direction;

    private Animator MovementAnim;

    public bool MoveForwardRestrict;

    private void Start()
    {
        MovementAnim = GetComponent<Animator>();

        MoveForwardRestrict = false;
    }

    void Update()
    {
            PlayerMovement();
    }
    
    public void PlayerMovement()
    {
        PlayerInput();
        
        if ((direction - Vector3.zero).sqrMagnitude < 0.001f)
        {
            Idle();
        }
        else {
            RunAnim();
        }
               
        if (direction.magnitude >= 0.1f)
        {
            PlayerRotation(direction);
            controller.Move(direction * PlayerSpeed * Time.deltaTime);
        }
    }

    public void PlayerInput()
    {
        direction = Vector3.zero;
        
        if (Input.GetKey(KeyCode.W) && MoveForwardRestrict == false)
        {
            direction = new Vector3(0, 0, 1).normalized;
        }

        if (Input.GetKey(KeyCode.S))
        {
            direction = new Vector3(0, 0, -1).normalized;
        }

        if (Input.GetKey(KeyCode.A))
        {
            direction = new Vector3(-1, 0, 0).normalized;
        }

        if (Input.GetKey(KeyCode.D))
        {
            direction = new Vector3(1, 0, 0).normalized;
        }
    }

    public void PlayerRotation(Vector3 direction)
    {
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnVelocity, turnTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }

    private void RunAnim()
    {
        MovementAnim.SetFloat(GameConstant.SPEED_PARA, 1);
    }

    private void Idle()
    {
        MovementAnim.SetFloat(GameConstant.SPEED_PARA, 0.1f);
    }
}
