using UnityEngine;
using System.Collections;


public class PlayerMovement : MonoBehaviour {

	float maxSpeed = 5f;
    float jumpSpeed = 300f;
    public int direction;
    public bool isGrounded;
    float attackRate = .25f;
    public bool isBlocking;
    bool canAttack;
    float reach = .5f;//distance that the player can reach
    float damage;
    Rigidbody2D rb;

	// Use this for initialization
	void Start () 
	{
        rb = GetComponent<Rigidbody2D>();
        isGrounded = true;
        canAttack = true;
        direction = 1;
        isBlocking = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
        if(Input.GetAxisRaw("Horizontal") > 0)
        {
            direction = 1;
        }
        else if(Input.GetAxisRaw("Horizontal") < 0)
        {
            direction = -1;
        }

        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * maxSpeed,rb.velocity.y);

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpSpeed);
            isGrounded = false;
        }
        if(Input.GetButtonDown("Fire1") && canAttack)
        {
            StartCoroutine(Attack());
        }
        if(Input.GetButton("Fire2"))
        {
            isBlocking = true;
            maxSpeed = 2.5f;
        }
        else
        {
            isBlocking = false;
            maxSpeed = 5f;
        }


	}

    void OnCollisionEnter2D(Collision2D col)
    {
        //Debug.Log("I am working!!!!");
        if(col.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    IEnumerator Attack()
    {
        canAttack = false;
        RaycastHit2D ray = Physics2D.Raycast(transform.position, new Vector2(direction * reach, 0));
        Debug.DrawRay(ray.point, new Vector2(direction * reach,0),Color.red);
        if (ray.collider != null)
        {
            if(direction != ray.collider.GetComponent<PlayerMovement>().Direction())
            {
                //ray.collider.gameObject.GetComponent<Health>().Damage();
            }
        }
        yield return new WaitForSeconds(attackRate);
        canAttack = true;
    }

    public int Direction()
    {
        return direction;
    }

}
