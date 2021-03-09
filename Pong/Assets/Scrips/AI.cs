using UnityEngine;

public class AI : MonoBehaviour
{

    public Ball ball;
    public float difficulty = 10;

    private Rigidbody2D rb;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector2 pos = Vector2.Lerp(transform.position, new Vector2(transform.position.x, ball.transform.position.y), difficulty * Time.deltaTime);
        rb.MovePosition(pos);
    }
}
