using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSquare : MonoBehaviour
{
    [SerializeField] GameObject square;
    [SerializeField] GameObject circle;
    public float RotateSpeed;
    bool trigger=false;
    public float timeRotate;
    Vector3 BallJumDir;
    public bool isJumpRight;
    
    // Start is called before the first frame update
    void Start()
    {
        if (isJumpRight)
        {
            BallJumDir = new Vector3(0.3f, 0.75f, 0f);
        }
        else
        {
            BallJumDir = new Vector3(-0.3f, 0.75f, 0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            if (!trigger)
            {
                trigger = true;
                collision.gameObject.GetComponent<PlayerController>().jumForce = 300;
                collision.gameObject.GetComponent<PlayerController>().jumDir = BallJumDir;
                collision.gameObject.transform.SetParent(gameObject.transform);
                collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                StartCoroutine(RotateForSeconds());
            }
        }
    }
    
    IEnumerator RotateForSeconds() 
    {
        
        while (timeRotate > 0)    
        {
           
            transform.Rotate(new Vector3(0,0,-1), Time.deltaTime * RotateSpeed);   
            timeRotate -= Time.deltaTime;   
            yield return null;    
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball")&& !GameManager.instance.gameover)
        {
            collision.gameObject.GetComponent<PlayerController>().jumForce = 300;
            collision.gameObject.GetComponent<PlayerController>().jumDir = Vector3.up;
            collision.gameObject.transform.SetParent(GameManager.instance.NowLevel.gameObject.transform);
           // collision.gameObject.transform.SetParent(null);
            collision.transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }
}
