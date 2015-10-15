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
    public float knockback;
    bool isStunned;
    public float stun;
    
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
        Physics2D.IgnoreLayerCollision(8,8);
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
        block = Input.GetButton(isPlayer.ToString() + "Block");

        if(movement > 0)
        {
            direction = 1;
        }
        else if(movement < 0)
        {
            direction = -1;
        }

        if(!isStunned)
            rb.velocity = new Vector2(movement * maxSpeed,rb.velocity.y);

        if(jump && isGrounded && !isStunned)
        {
            rb.AddForce(Vector2.up * jumpSpeed);
            isGrounded = false;
        }
        if(attack && canAttack && !isStunned)
        {
            StartCoroutine(Attack());
        }
        if(block && !isStunned)
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
        RaycastHit2D ray = Physics2D.Raycast(transform.position, new Vector2(direction, 0),reach);
        Debug.DrawRay(transform.position, new Vector2(direction * reach, 0), Color.red);
        if (ray.collider != null)
        {
            Collider2D target = ray.collider;
            if(target.tag == "Player")
            {
                if (target.GetComponent<PlayerMovement>().isBlocking)
                {
                    if(target.GetComponent<PlayerMovement>().Direction() == this.direction)
                    {
                        target.GetComponent<HealthBar>().Damage(damage);
                        StartCoroutine(target.GetComponent<PlayerMovement>().Stun(stun));
                        target.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                        target.GetComponent<Rigidbody2D>().AddForce(new Vector2(direction,1) * knockback);
                    }
                }
                else
                {
                    target.GetComponent<HealthBar>().Damage(damage);
                    StartCoroutine(target.GetComponent<PlayerMovement>().Stun(stun));
                    target.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    target.GetComponent<Rigidbody2D>().AddForce(new Vector2(direction, 1) * knockback);
                }



            }
        }
        yield return new WaitForSeconds(attackRate);
        canAttack = true;
    }

    public int Direction()
    {
        return direction;
    }

    public IEnumerator Stun(float stunTime)
    {
        isStunned = true;
        yield return new WaitForSeconds(stunTime);
        isStunned = false;
    }
}
