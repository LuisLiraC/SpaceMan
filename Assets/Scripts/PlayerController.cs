using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigidBody;
    Animator animator;
    const string STATE_ALIVE = "isAlive";
    const string STATE_ON_THE_GROUND = "isOnTheGround";

    [SerializeField]
    float jumpForce = 6f;

    [SerializeField]
    float runningSpeed = 2f;

    [SerializeField]
    LayerMask groundMask;

    [SerializeField]
    float groundDistanceToJump = 1.5f;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        animator.SetBool(STATE_ALIVE, true);
        animator.SetBool(STATE_ON_THE_GROUND, true);
    }

    void Update()
    {
        animator.SetBool(STATE_ON_THE_GROUND, IsTouchingTheGround());

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (IsTouchingTheGround())
            {
                Jump();
            }
        }

        Debug.DrawRay(this.transform.position, Vector2.down * groundDistanceToJump, Color.red);
    }

    void FixedUpdate()
    {
        Move();
    }

    void Jump()
    {
        rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
    

    bool IsTouchingTheGround()
    {
        if (Physics2D.Raycast(this.transform.position, Vector2.down, groundDistanceToJump, groundMask))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void Move()
    {
        rigidBody.velocity = new Vector2(Input.GetAxis("Horizontal") * runningSpeed, rigidBody.velocity.y);
        if (Input.GetAxis("Horizontal") < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }
}
