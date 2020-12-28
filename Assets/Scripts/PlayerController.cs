using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigidBody;
    Animator animator;
    Vector3 startPosition;

    const string STATE_ALIVE = "isAlive";
    const string STATE_ON_THE_GROUND = "isOnTheGround";

    [SerializeField]
    float jumpForce = 7f;

    [SerializeField]
    float runningSpeed = 4f;

    [SerializeField]
    LayerMask groundMask;

    [SerializeField]
    float groundDistanceToJump = 1.5f;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        startPosition = transform.position;
    }

    void Start()
    {
        rigidBody.gravityScale = 0;
    }

    public void StartGame()
    {
        
        animator.SetBool(STATE_ALIVE, true);
        animator.SetBool(STATE_ON_THE_GROUND, false);
        StartCoroutine(RestartPosition());
    }
    IEnumerator RestartPosition()
    {
        yield return new WaitForSeconds(0.3f);
        transform.position = startPosition;
        rigidBody.velocity = Vector2.zero;
        rigidBody.gravityScale = 1;
        GameObject mainCamera = GameObject.Find("Main Camera");
        mainCamera.GetComponent<CameraFollow>().ResetCameraPosition();
    }

    void Update()
    {
        animator.SetBool(STATE_ON_THE_GROUND, IsTouchingTheGround());

        if (Input.GetKeyDown(KeyCode.Space) && GameManager.sharedInstance.currentGameState == GameState.InGame)
        {
            if (IsTouchingTheGround())
            {
                Jump();
            }
        }

        //Debug.DrawRay(this.transform.position, Vector2.down * groundDistanceToJump, Color.red);
    }

    void FixedUpdate()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.InGame)
        {
            Move();
        }
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

    public void Die()
    {
        animator.SetBool(STATE_ALIVE, false);
        GameManager.sharedInstance.GameOver();
    }
}
