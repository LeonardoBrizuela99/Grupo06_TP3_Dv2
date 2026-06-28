using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 5;

    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            transform.Translate(Vector2.up * (speed * Time.deltaTime));

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            transform.Translate(Vector2.down * (speed * Time.deltaTime));

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            transform.Translate(Vector2.left * (speed * Time.deltaTime));

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            transform.Translate(Vector2.right * (speed * Time.deltaTime));
    }
}
