using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{  
    [Header("Movement Settings")]
    [SerializeField] private float speed = 2f;
    [SerializeField] private float jumpForce = 4f;

    [Header("Ground Detection")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundRadius = 0.1f;
    [SerializeField] private LayerMask groundLayer;

    [Header("UI References")]
    [SerializeField] private TMP_Text textCoins;

    [Header("Audio Settings")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip coinClip;
    [SerializeField] private AudioClip barrelClip;
    [SerializeField] private AudioClip spikeClip;
    [SerializeField] private AudioClip jumpClip;
   
    private Rigidbody2D rb2d;
    private Animator animator;
    private float moveInput;
    private bool isGrounded;
    private int coinsCount;
    private const int MAX_COINS_VICTORY = 30;
     
    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        HandleMovement();
        HandleJump();
        UpdateAnimator();
    }
    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        HandleTriggers(collision);
    } 
    private void HandleMovement()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        rb2d.linearVelocity = new Vector2(moveInput * speed, rb2d.linearVelocity.y);
        if (moveInput != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(moveInput), 1, 1);
        }
    }
    private void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            if (audioSource != null && jumpClip != null)
            {
                audioSource.PlayOneShot(jumpClip);
            }
            rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, jumpForce);
        }
    }
    private void UpdateAnimator()
    {
        animator.SetFloat("Speed", Mathf.Abs(moveInput));
        animator.SetFloat("VerticalVelocity", rb2d.linearVelocity.y);
        animator.SetBool("isGrounded", isGrounded); 
    }  
    private void HandleTriggers(Collider2D collision)
    {
        if (collision.transform.CompareTag("Coin"))
        {
            PlayAudio(coinClip);
            Destroy(collision.gameObject);
            coinsCount++;
            if (textCoins != null)
            {
                textCoins.text = coinsCount.ToString();
            }
            CheckVictoryCondition();
        }
        if (collision.transform.CompareTag("Spikes"))
        {
            PlayAudio(spikeClip);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (collision.transform.CompareTag("Barrel"))
        {
            PlayAudio(barrelClip);
            ApplyBarrelKnockback(collision);
            TriggerBarrelDestruction(collision.gameObject);
        }
    }
    private void CheckVictoryCondition()
    {
        if (coinsCount >= MAX_COINS_VICTORY)
        {
            SceneManager.LoadScene("Victory");
        }
    }
    private void ApplyBarrelKnockback(Collider2D collision)
    {
        Vector2 knockbackDir = (rb2d.position - (Vector2)collision.transform.position).normalized;
        rb2d.linearVelocity = Vector2.zero;
        rb2d.AddForce(knockbackDir * 3f, ForceMode2D.Impulse);
    }
    private void TriggerBarrelDestruction(GameObject barrel)
    {
        BoxCollider2D[] colliders = barrel.GetComponents<BoxCollider2D>();
        foreach (BoxCollider2D col in colliders)
        {
            col.enabled = false;
        }
        Animator barrelAnim = barrel.GetComponent<Animator>();
        if (barrelAnim != null)
        {
            barrelAnim.enabled = true;
        }
        Destroy(barrel, 0.5f);
    }
    private void PlayAudio(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}