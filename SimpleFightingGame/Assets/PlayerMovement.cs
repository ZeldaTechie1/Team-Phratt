using UnityEngine;
using System.Collections;


public class PlayerMovement : MonoBehaviour {
	float maxSpeed = 4.5f;
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		Vector2 pos = transform.position;
		//Input.GetAxis ("Horizontal");
		pos.x +=Input.GetAxis ("Horizontal") * (maxSpeed * Time.deltaTime);

		transform.position = pos;
	}


}
