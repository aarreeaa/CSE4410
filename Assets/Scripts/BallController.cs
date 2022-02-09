using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{

    public float speed;
    public float randomUp;

    Rigidbody2D ballRigidbody;

    GameController cont;

    // Start is called before the first frame update
    void Start()
    {
        ballRigidbody = GetComponent<Rigidbody2D>();
        cont = FindObjectOfType<GameController>();

    }

    private void OnEnable()
    {
        Invoke("PushBall",1);
    }

    private void PushBall()
    {
        int dir = Random.Range(0,2); //ramdom from 0 - 1 (2 not included), return 0 or 1
        // if dir = 0 then move ball to right, if dir =1 then move ball to left
        float x, y;
        if (dir == 0)
            x = speed;
        else
            x = -speed;

        y = Random.Range(-randomUp,randomUp);

        ballRigidbody.AddForce(new Vector2(x, y));

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 vel;
            vel.x = ballRigidbody.velocity.x;
            vel.y = (ballRigidbody.velocity.y/2) + (collision.collider.attachedRigidbody.velocity.y/2); //collison.collider is the player, half velocity of ball and half velocity of player

            ballRigidbody.velocity = vel;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Goal"))
        {
            if (ballRigidbody.velocity.x > 0)//positive vel -- leftPlayerScore++
            {
                cont.Score(true);
            }
            else if (ballRigidbody.velocity.x < 0)//negative velocity -- rightPlayerScore++
            {
                cont.Score(false);
            }
            else
            { }

            ballRigidbody.velocity = Vector2.zero;
            transform.position = Vector3.zero; //new vector3(0,0,0)
            Invoke("PushBall", 2);
        }
    }

}
