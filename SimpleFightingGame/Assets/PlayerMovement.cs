using UnityEngine;
using System.Collections;


public class PlayerMovement : MonoBehaviour {

	float maxSpeed = 4.5f;
    float jumpSpeed = 350f;
    public bool isGrounded;
    Rigidbody2D rb;

	// Use this for initialization
	void Start () 
	{
        rb = GetComponent<Rigidbody2D>();
        isGrounded = true;
	}
	
	// Update is called once per frame
	void Update ()
	{

        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * maxSpeed,rb.velocity.y);

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpSpeed);
            isGrounded = false;
        }

	}

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("I am working!!!!");
        if(col.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

}
