using UnityEngine;
using System.Collections;


public class PlayerMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 pos = transform.position;
		pos.x += Input.GetAxis ("Horizontal");
		transform.position = pos;

		Vector2 pos2 = transform.position;
		pos.y += Input.GetAxis ("Vertical");
		transform.position = pos2;
		}
}
