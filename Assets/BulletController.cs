using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {
	public GameObject Player1;
	Vector3 velocity;
	// Use this for initialization
	void Start () {
		float speed = 0.2f;
		Player1 = GameObject.Find ("Player");
		transform.position = Player1.transform.position;
		/*
		float mag = Mathf.Sqrt(Mathf.Pow(Input.mousePosition.x - Player1.transform.position.x,2)+Mathf.Pow (Input.mousePosition.y - Player1.transform.position.y,2));
		Vector3 unit = new Vector3((Input.mousePosition.x - Player1.transform.position.x)/mag,(Input.mousePosition.y - Player1.transform.position.y)/mag,0);
		unit *= speed;
		velocity = unit;
		*/
		Physics.IgnoreCollision(Player1.collider,collider);
		Vector3 screenCoords = Camera.main.WorldToScreenPoint(transform.position);
		float angle = Mathf.Atan2 (Input.mousePosition.y - screenCoords.y,Input.mousePosition.x - screenCoords.x);
		//Debug.Log (angle);
		//Debug.Log (Input.mousePosition.x + " " + Input.mousePosition.y + " : " + transform.position.x + " " + transform.position.y);
		Vector3 vect = new Vector3(Mathf.Cos (angle),Mathf.Sin(angle),0);
		velocity = vect * speed;
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 temp = transform.position;
		temp += velocity;
		transform.position = temp;
		
		//Physics.Ign
	}
}
