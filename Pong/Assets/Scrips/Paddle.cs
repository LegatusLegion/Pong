using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float speed = 10;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

    }


    private void FixedUpdate()
    {
        float vertical = Input.GetAxisRaw("Vertical") * speed;

        rb.velocity = new Vector2(0,vertical);
    }

    public void ResetPeddlePosition()
    {
        rb.velocity = Vector2.zero;
        rb.transform.position = new Vector2(-8, 0);
    }

}
