using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 2;
    private Rigidbody2D rb2d;
    private float move;

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
    }
}
