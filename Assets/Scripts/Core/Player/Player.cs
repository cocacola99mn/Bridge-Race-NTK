using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] CharacterController controller;

    public float PlayerSpeed;

    public float turnTime = 0.1f;
    float turnVelocity;

    private Animator MovementAnim;

    private void Start()
    {
        MovementAnim = GetComponent<Animator>();
    }

    void Update()
    {
        PlayerMovement();
    }
    
    public void PlayerMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;
        
        if (direction.Equals(Vector3.zero))
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
