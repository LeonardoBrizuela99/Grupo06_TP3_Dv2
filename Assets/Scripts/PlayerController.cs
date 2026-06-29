using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 2;
    public float jumpForce = 4;
    private Rigidbody2D rb2d;
    private float move;
    private bool isGrounded;
    public Transform groundCheck;
    public float groundRadius = 0.1f;
    public LayerMask groundLayer;

    private void Start()
    {
        rb2d=GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        move = Input.GetAxisRaw("Horizontal");
        rb2d.linearVelocity = new Vector2(move * speed, rb2d.linearVelocity.y);

        if (move != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(move), 1, 1);
        }
        if (Input.GetButtonDown("Jump") && isGrounded)
        {

            rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, jumpForce);

        }
    }
    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);
        if (isGrounded)
        {

        }
    }
}
