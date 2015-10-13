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
    public float damage;
    Rigidbody2D rb;
    [SerializeField]
    Player isPlayer;
    Input playerInput;
    Input playerJump;
    
    enum Player
    {
        Player1,
        Player2
    };

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
        float movement;
        bool jump;
        bool attack;
        bool block;

        movement = Input.GetAxisRaw(isPlayer.ToString() + "Movement");
        jump = Input.GetButtonDown(isPlayer.ToString() + "Jump");
        attack = Input.GetButtonDown(isPlayer.ToString() + "Attack");
        block = Input.GetButtonDown(isPlayer.ToString() + "Block");

        if(movement > 0)
        {
            direction = 1;
        }
        else if(movement < 0)
        {
            direction = -1;
        }

        rb.velocity = new Vector2(direction * maxSpeed,rb.velocity.y);

        if(jump && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpSpeed);
            isGrounded = false;
        }
        if(attack && canAttack)
        {
            StartCoroutine(Attack());
        }
        if(block)
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
                ray.collider.gameObject.GetComponent<HealthBar>().Damage(2);
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
