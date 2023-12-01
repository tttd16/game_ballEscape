using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public float jumForce;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] GameObject square;
    public AudioClip jumpSound;
    public bool isGameOver;
    private Camera cam;
    private float heightCam;
    private float weightCam;
    public bool isGrounded=false;
    private bool firstTouch = true;
    public Vector3 jumDir = Vector3.up;
    [SerializeField] float maxVelocity ;

    [SerializeField] private AudioClip crashSound;
    [SerializeField] private LayerMask groundMask;

    private float coyote = 0.08f;
    private float curCoyote;
    
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        heightCam = cam.orthographicSize;
        weightCam = heightCam * ((float)Screen.width / Screen.height);
       
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxVelocity);
       
    }
    void Update()
    {
        if (!isGrounded && curCoyote > 0)
            curCoyote -= Time.deltaTime;
        
        CheckOutofCam();
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(transform.up * jumForce);
        }
        if (Input.GetMouseButtonDown(0) && !IsPointerOverUIObject() )
        {
            if (!firstTouch && (isGrounded|| curCoyote > 0))
            {
                SoundManager.instance.PlaySFX(jumpSound);
               
                Jump(jumDir);
            }
            else
            {
                square.SetActive(false);
                firstTouch = false;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            curCoyote = coyote;
            
            var hit = Physics2D.Raycast(transform.position, Vector2.down, 2f, groundMask);

            if (collision.GetComponent<SquareMove>() 
                || (hit.collider && Mathf.Sign(rb.velocity.x) == Mathf.Sign(hit.normal.x)))
            {
                
            }
            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }

        }
        if (collision.gameObject.CompareTag("Nail"))
        {
            SoundManager.instance.PlaySFX(crashSound);
            GameManager.instance.PlayParticle(gameObject.transform);
            GameManager.instance.GameOver();
            Destroy(gameObject);
        }
       
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
    private void CheckOutofCam()
    {
        if (transform.position.x > (cam.transform.position.x + weightCam) || transform.position.x < (cam.transform.position.x - weightCam) || transform.position.y > (cam.transform.position.y + heightCam) || transform.position.y < (cam.transform.position.y - heightCam))
        {
            if (!isGameOver)
            {
                SoundManager.instance.PlaySFX(crashSound);
                GameManager.instance.PlayParticle(gameObject.transform);
                gameObject.SetActive(false);
                isGameOver = true;
                GameManager.instance.GameOver();
            }
        }
    }
    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
    public void Move(Vector3 dir,float speed)
    {
        rb.velocity = dir.normalized * speed;
    }

    public void Jump(Vector3 jumDir)
    {
        rb.AddForce(jumDir * jumForce);
    }
}
