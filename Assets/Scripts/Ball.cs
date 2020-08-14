using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    public int speed = 100;
    public int Score;
    private bool isPressed;
    private Rigidbody2D rb;
    private SpringJoint2D sj;
    private float releaseDelay;

    public static explicit operator Ball(GameObject v)
    {
        throw new NotImplementedException();
    }

    private void Awake(){
        rb = GetComponent<Rigidbody2D>();
        sj = GetComponent<SpringJoint2D>();
        
        releaseDelay = 1/(sj.frequency * 4);
    }
    
    // Start is called before the first frame update
    void Start()
    {

        GetComponent<Rigidbody2D>().velocity = Vector2.up * speed;
    }

    void OnCollisionEnter2D(Collision2D col) {

        if (col.gameObject.name == "racket") {
            // Calculate hit Factor
            float x=hitFactor(transform.position,
                              col.transform.position,
                              col.collider.bounds.size.x);

            // Calculate direction, set length to 1
            Vector2 dir = new Vector2(x, 1).normalized;

            // Set Velocity with dir * speed
            GetComponent<Rigidbody2D>().velocity = dir * speed;
        }
        if (col.gameObject.tag == "Block")
        {
            AddScore();
            speed++;
        }
        
    }

    float hitFactor(Vector2 ballPos, Vector2 racketPos,
                float racketWidth) {

        return (ballPos.x - racketPos.x) / racketWidth;
    }

    void AddScore()
    {
        Score++;
        if(FindObjectOfType<Text>() != null)
        {
            FindObjectOfType<Text>().text = "Score: " + Score * 100;
        }
            
    }

    void Update()
    {
        if(GetComponent<Rigidbody2D>().position.y < -200)
        {
            FindObjectOfType<GameManager>().GameOver();
        }
        
        if(isPressed){
            DragBall();
        }
    }

    private void OnMouseDown(){
        isPressed = true;
        rb.isKinematic = true;
        StartCoroutine(Release());
    }
    
    private void OnMouseUp(){
        isPressed = false;
        rb.isKinematic = false;
    }
    
    private void DragBall(){
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        rb.position = mousePosition;
    }
    
    private IEnumerator Release(){
        yield return new WaitForSeconds(releaseDelay);
        sj.enabled = false;
    }


}
