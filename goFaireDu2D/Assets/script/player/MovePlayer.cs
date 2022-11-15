using UnityEngine;

public class MovePlayer : MonoBehaviour
{

    public float moveSpeed;
    public Rigidbody2D rb;
    private Vector3 velocity = Vector3.zero;
    public bool isJumping = false;
    public float jumpForce;
    public bool isGrounded;
    public Transform groundCheckLeft;
    public Transform groundCheckRight;

    public Animator animator;
    public SpriteRenderer SpriteRenderer;

    public float timeRemaining = 3;


    public bool dash = false;


    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapArea(groundCheckLeft.position, groundCheckRight.position);

        float horizontalMovenment;

        if (Input.GetKey("left shift"))
            horizontalMovenment = Input.GetAxis("Horizontal") * (moveSpeed + 100) * Time.deltaTime;
        else
            horizontalMovenment = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;

        if ((Input.GetButtonDown("Jump") || (Input.GetKey("up")) || (Input.GetKey("space"))) && isGrounded)
        {
            isJumping = true;
        }

        Movement(horizontalMovenment);
        flip(rb.velocity.x);


        float characterVelocity = Mathf.Abs(rb.velocity.x);
        animator.SetFloat("Speed", characterVelocity);
    }

    void Movement(float _horizontalMovenment)
    {
        if (!dash && Input.GetKey("left alt") && _horizontalMovenment != 0)
        {
            Vector3 targetVelocity = new Vector2(_horizontalMovenment - 200, rb.velocity.y);
            if (_horizontalMovenment > 0) {
                targetVelocity = new Vector2(_horizontalMovenment + 200, rb.velocity.y);
            }
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);
            dash = true;
            timeRemaining = 3;
        }
        else
        {
            Vector3 targetVelocity = new Vector2(_horizontalMovenment, rb.velocity.y);
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            if (timeRemaining <= 0)
                dash = false;
        }

        if (isJumping)
        {
            if (Input.GetKey("left shift"))
            {
                rb.AddForce(new Vector2(0f, jumpForce + 100));
            }
            else
            {
                rb.AddForce(new Vector2(0f, jumpForce));
            }
            isJumping = false;
        }
        if (!isGrounded && Input.GetKey("down"))
        {
            rb.AddForce(new Vector2(0f, -50));
        }
    }

    void flip(float _speed)
    {
        if (_speed > 0.1f)
        {
            SpriteRenderer.flipX = false;
        } else if  (_speed < -0.1f)
        {
            SpriteRenderer.flipX = true;
        }
            
    }
}
