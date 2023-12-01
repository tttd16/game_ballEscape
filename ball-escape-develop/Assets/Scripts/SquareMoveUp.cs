using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareMoveUp : MonoBehaviour
{
    public Vector3 targetPos;
    public float speed;
    [SerializeField] Rigidbody2D rb;
    bool isMove;
    public bool moveUp;
    Vector3 JumpDir;
    public bool BallJumpRight;
    GameObject ball;
    bool ballout;
    Vector3 dir;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(transform.position);
        if (BallJumpRight)
        {
            JumpDir = new Vector3(0.5f, 0.75f, 0f);
        }
        else
        {
            JumpDir = new Vector3(-0.5f, 0.75f, 0f);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Vector3.Distance(targetPos, transform.position) < 0.05f)
        {
            if (isMove)
            {
                isMove = false;
                rb.velocity = Vector3.zero;
                if (moveUp && !ballout )
                {
                    ball.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                }
               // 
            }
        }
        
    }
    public void Move()
    {
        dir = targetPos - transform.position;
        rb.velocity = dir.normalized * speed;
        isMove = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball")){
            Move();
            ballout = false;
            collision.gameObject.GetComponent<PlayerController>().jumDir = JumpDir;
           // collision.gameObject.transform.SetParent(gameObject.transform);
            collision.gameObject.GetComponent<PlayerController>().Move(dir, speed);
            ball = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
           // Debug.Log("ball out");
            ballout = true;
            collision.gameObject.GetComponent<PlayerController>().jumDir = Vector3.up;
           
        }
    }
}
