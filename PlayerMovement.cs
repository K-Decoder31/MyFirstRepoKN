using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public float moveInputX;
    public float moveInputY;
    public float playerSpeed;
    public float climbSpeed;
    public bool canClimb;
    public Rigidbody2D rb;
    float SavedGravity;
    public SpriteRenderer sprite;
    public bool isFacingRight;
    public bool isGrounded;
    public float jumpForce;

    void Start()
    {
        // rb = GetComponent<Rigidbody2D>();   // Voi asettaa myös itse
        // playerSpeed = 1.001f;
        SavedGravity = rb.gravityScale;
        canClimb = false;   

    }

    float ControlY()
    {
        if (canClimb)
        {
            return moveInputY * climbSpeed;
        }
        else
        {
            return rb.linearVelocity.y;
        }
    }
    void PlayerMove()
    {
        //playerSpeed = playerSpeed + 1;
        // lyhyempi tapa:
        //playerSpeed += 0.001f;

        // Skannataan jatkuvasti inputteja: vas.oik.ylös.alas 🔼🔽
        moveInputX = Input.GetAxis("Horizontal");
        moveInputY = Input.GetAxis("Vertical");
        // Miten liikutetaan Game Objectia, eri tapoja:

        //transform.position += Vector3.right * playerSpeed;
        // transform.Translate(Vector3.right * playerSpeed);
        //transform.position = new(transform.position.x + moveInput
        //    * playerSpeed * Time.deltaTime, 2);

        // Liikutetaan RigidBody2D:tä:
        rb.linearVelocity = new Vector2(moveInputX * playerSpeed,
            ControlY());

        //rb.linearVelocity = Vector2.zero; // sama kuin new Vector2(0,0);
    }
    void FlipSprite()
    {
        if (moveInputX < 0 && isFacingRight)
        {
            //transform.localScale = new Vector3(-transform.localScale.x,
            //    transform.localScale.y, transform.localScale.z);
            isFacingRight = false;
            sprite.flipX = false;
        }
        // Tänne vastaehto:
        if (moveInputX > 0 && !isFacingRight)
        {
            isFacingRight = true;
            sprite.flipX = true;
        }
    }

    void JumpControl()
    {
        if (isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                Debug.Log("Jumping");
                rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            }

        }
    }
    // Enable jumping
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = false;

        }
    }
    // Enable climbing
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("climbable"))
        {
            climbSpeed = 6f;
            rb.gravityScale = 0;
            canClimb = true;    
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("climbable"))
        {
            climbSpeed = 0;
            rb.gravityScale = SavedGravity;
            canClimb = false;
        }
    }

    void Update()
    {
        PlayerMove();
        JumpControl();
        FlipSprite();

    }
}
