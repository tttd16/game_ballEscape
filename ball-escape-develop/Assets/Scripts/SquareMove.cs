using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareMove : MonoBehaviour
{
    public Vector3 targetPos;
    public float speed;
    public Vector3 vectorRotate;
    public float timeRotate;
    public float RotateSpeed;
    [SerializeField] Rigidbody2D rb;
   public bool finishMoved=false;
    bool moved=false;
   public bool ballout;
    Vector3 dir;
    GameObject ball;

    // Update is called once per frame
    void Update()
    {
        

        if (Vector3.Distance(transform.position,targetPos)<0.05f)
        {
           
            if (!finishMoved)
            {
                if (!ballout)
                {
                    if(ball)
                        ball.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                }
                finishMoved = true;
                rb.velocity = Vector3.zero;
               // Debug.Log("position: " + transform.position);

            }
            if (timeRotate > 0)
            {
                transform.Rotate(vectorRotate, Time.deltaTime * RotateSpeed);
                timeRotate -= Time.deltaTime;
            }
        }
    }
    public void Move()
    {
        dir = targetPos - gameObject.transform.position;
        
        rb.velocity = dir.normalized * speed;
    }
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            ballout = false;
            if (!moved)
            {
                moved = true;
                Move();
                var ballObj = collision.gameObject;
                ballObj.transform.SetParent(gameObject.transform);
                ballObj.GetComponent<PlayerController>().Move(dir, speed);
                ball = ballObj;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Ball") && !GameManager.instance.gameover)
        {
          //  Debug.Log(ballout);
            ballout = true;
            collision.gameObject.GetComponent<PlayerController>().jumForce = 300;
            collision.gameObject.transform.SetParent(GameManager.instance.NowLevel.gameObject.transform);
            //collision.gameObject.transform.SetParent(null);
            collision.transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }
}
