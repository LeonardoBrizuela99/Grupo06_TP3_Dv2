using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject Hud;
    [SerializeField] GameObject GameOverScreen;

    public float speed = 2;
    public float jumpForce = 4;
    private float move;
    public float groundRadius = 0.1f;
    
    private int coins;
    
    private bool isGrounded;
    
    private Rigidbody2D rb2d;
    
    public Transform groundCheck;
    private Animator animator;
    
    public LayerMask groundLayer;

    public TMP_Text textCoins;

    public AudioSource audioSource;
    public AudioClip coinClip;
    public AudioClip barrelClip;
    public AudioClip spikeClip;
    public AudioClip jumpClip;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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
            audioSource.PlayOneShot(jumpClip);

            rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, jumpForce);
        }

        animator.SetFloat("Speed", Mathf.Abs(move));
        animator.SetFloat("VerticalVelocity", rb2d.linearVelocity.y);
        animator.SetBool("isGrounded", isGrounded);
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Coin"))
        {
            audioSource.PlayOneShot(coinClip);
            
            Destroy(collision.gameObject);
            
            coins++;
            
            textCoins.text = coins.ToString();

        }

        if (collision.transform.CompareTag("Spikes"))
        {
            audioSource.PlayOneShot(spikeClip);

            Hud.SetActive(false);
            GameOverScreen.SetActive(true);

            Time.timeScale = 0;
        }

        if (collision.transform.CompareTag("Barrel"))
        {
            audioSource.PlayOneShot(barrelClip);
            
            Vector2 knockbackDir = (rb2d.position - (Vector2)collision.transform.position).normalized;

            rb2d.linearVelocity = Vector2.zero;
            rb2d.AddForce(knockbackDir * 3, ForceMode2D.Impulse);
           
            BoxCollider2D[] colliders=collision.gameObject.GetComponents<BoxCollider2D>();
            
            foreach (BoxCollider2D col in colliders)
            {
                col.enabled = false;
            }
            
            collision.GetComponent<Animator>().enabled = true;
            
            Destroy(collision.gameObject, 0.5f);
        }
    }
}
