using UnityEngine;

public class Ball : MonoBehaviour
{
    public Collider2D paddleLeft;
    public Collider2D paddleRight;
    public Collider2D goalLeft;
    public Collider2D goalRight;
    public float speed;
    private Rigidbody2D rb;
    public Paddle paddle;
    private int schlagAbtausch;
    public GameManager gM;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void InitialStart()
    {
        //Gamestart, Ball wird nach rechts geschossen
        Vector2 initalDir = new Vector2(1, Random.Range(-0.3f, 0.3f)).normalized;
        rb.velocity = initalDir * speed;        
    }

    public void ResetBall()
    {
        speed = 18f;
        schlagAbtausch = 0;
        rb.velocity = Vector2.zero;
        rb.transform.position = Vector2.zero;
        GameManager.gameRunning = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        //If BAll hits paddle
        if(collision.collider == paddleLeft || collision.collider == paddleRight)
        {
            schlagAbtausch++;
            //Debug Position auf der y Achse
            float y = transform.position.y - collision.transform.position.y;
            //Debug.Log(y);

            float x = 0;
            //Wenn Ball paddleLeft trifft, wird er zurück geworfen
            if (collision.collider == paddleLeft)
                x = 1;
            else
                x = -1;

            //geben den neuen Vector mit einer geschwindigkeit von 1 wieder
            Vector2 dir = new Vector2(x, y).normalized;
            rb.velocity = dir * speed;

            if(schlagAbtausch > 5 )
                IncreaseBallSpeed();

            collision.transform.GetComponent<AudioSource>().Play();
        }
     
        //Ball Collision with goals
        if(collision.collider == goalLeft || collision.collider == goalRight)
        {
            if(collision.collider == goalRight)
                FindObjectOfType<GameManager>().IncreaseScore(true);
            else
                FindObjectOfType<GameManager>().IncreaseScore(false);

            paddle.ResetPeddlePosition();
            ResetBall();            
        }

    }

    private void IncreaseBallSpeed()
    {
        speed += gM.increaseBallSpeed;
        Debug.Log(speed);
    }

}
