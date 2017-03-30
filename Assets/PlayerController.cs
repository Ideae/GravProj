using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	public string orient;
	public string lastOrient;
	public float gravforce;
	public GameObject player1;
	public Camera camera1;
	public float currentCamRotation;
	public GameObject activePlatform;
	public Transform Bullet;
	// Use this for initialization
	void Start () {
		orient = "up";
		lastOrient = "up";
		gravforce = 5.0f;
		currentCamRotation = 0;
		activePlatform = GameObject.Find("Platform");
	}
	
	// Update is called once per frame
	void Update () {
		findNearestPlatform();
		applyGravity();
		KeyboardInput();
		MouseInput();
		//playerMovement();
	}
	
	
	void findNearestPlatform()
	{
		GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platform");
		//Debug.Log (platforms.Length);
		GameObject nearest = platforms[0];
		float nearestDist = 1000000.0f;
		foreach (GameObject platform in platforms)
		{
			float dist = (transform.position - platform.transform.position).sqrMagnitude;
			if (dist < nearestDist)
			{
				nearestDist = dist;
				nearest = platform;
				//nearest = platform;
			}
			
		}
		activePlatform = nearest;

	}
	
	void applyGravity()
	{
		Vector3 playerpos = transform.position;
		//var box = GameObject.Find ("Platform");
		
		//box.collider.bounds.max.x;
		
		
		Vector2 posvect = new Vector2(playerpos.x - activePlatform.transform.position.x,playerpos.y - activePlatform.transform.position.y);
		float distance = Mathf.Sqrt(Mathf.Pow (posvect.x,2)+Mathf.Pow(posvect.y,2));
		//Debug.Log (distance);
		float relGravForce = (4*gravforce) / (3*distance);
		//Debug.Log (relGravForce);
		float angle = Mathf.Atan2(posvect.y,posvect.x);
		//Debug.Log (currentCamRotation);
		Vector3 temp = camera1.transform.position;
		temp.z = (-distance)-10;
		camera1.transform.position = temp;
		camera1.orthographicSize = distance+8;
		
		approachPosition(camera1,activePlatform.transform.position,0.1f);
		
		float speed = 2.0f;
		float relPlatRotZ = activePlatform.transform.rotation.z;
		if (angle >= (-Mathf.PI/4) && angle <= (Mathf.PI/4))
		{
			orient = "right";
			GetComponent<Rigidbody>().AddForce(new Vector3(-relGravForce,0,0));
			
			//camera.transform.rotation = Quaternion.Euler(0,0,270);
			
			currentCamRotation = approachAngle(currentCamRotation,relPlatRotZ + 270,speed);
			camera1.transform.rotation = Quaternion.Euler(0,0,currentCamRotation);
			//camera.transform.rotation = Quaternion.Euler(0,0,activePlatform.transform.rotation.z+270);
			
			
		}
		else if (angle >= (Mathf.PI/4) && angle <= (3*Mathf.PI/4))
		{
			orient = "up";
			GetComponent<Rigidbody>().AddForce(new Vector3(0,-relGravForce,0));
			
			currentCamRotation = approachAngle(currentCamRotation, relPlatRotZ,speed);
			camera1.transform.rotation = Quaternion.Euler(0,0,currentCamRotation);
			//camera.transform.rotation = Quaternion.Euler(0,0,0);
			//camera.transform.rotation = Quaternion.Euler(0,0,activePlatform.transform.rotation.z);
		}
		else if (angle >= (3*Mathf.PI/4) || angle <= (-3*Mathf.PI/4))
		{
			orient = "left";
			GetComponent<Rigidbody>().AddForce(new Vector3(relGravForce,0,0));
			//camera.transform.rotation = Quaternion.Euler(0,0,90);
			currentCamRotation = approachAngle(currentCamRotation, relPlatRotZ + 90,speed);
			camera1.transform.rotation = Quaternion.Euler(0,0,currentCamRotation);
			//camera.transform.rotation = Quaternion.Euler(0,0,activePlatform.transform.rotation.z+90);
		}
		//else if (angle >= (3*Mathf.PI/4) || angle <= (-3*Mathf.PI/4))
		else
		{
			orient = "down";
			GetComponent<Rigidbody>().AddForce(new Vector3(0,relGravForce,0));
			//camera.transform.rotation = Quaternion.Euler(0,0,180);
			currentCamRotation = approachAngle(currentCamRotation, relPlatRotZ + 180,speed);
			camera1.transform.rotation = Quaternion.Euler(0,0,currentCamRotation);
			//camera.transform.rotation = Quaternion.Euler(0,0,activePlatform.transform.rotation.z+180);
		}
		
		lastOrient = orient;
        //Debug.Log(orient);
	}
	
	void KeyboardInput()
	{
		//Debug.Log (Input.GetAxis("Horizontal"));
		float jumpForce = 200.0f;
		Vector3 relativeVect = activePlatform.transform.right;
		if (orient.Equals("up"))
		{
			relativeVect = activePlatform.transform.right;
			if (Input.GetKeyDown("space"))
				GetComponent<Rigidbody>().AddForce(new Vector3(0,jumpForce,0));
		}
		else if (orient.Equals("down"))
		{
			relativeVect = activePlatform.transform.right * -1;
			if (Input.GetKeyDown("space"))
				GetComponent<Rigidbody>().AddForce(new Vector3(0,-jumpForce,0));
		}
		else if (orient.Equals("right"))
		{
			relativeVect = activePlatform.transform.up * -1;
			if (Input.GetKeyDown("space"))
				GetComponent<Rigidbody>().AddForce(new Vector3(jumpForce,0,0));
		}
		else if (orient.Equals("left"))
		{
			relativeVect = activePlatform.transform.up;
			if (Input.GetKeyDown("space"))
				GetComponent<Rigidbody>().AddForce(new Vector3(-jumpForce,0,0));
		}
		transform.position = transform.position + (relativeVect*Input.GetAxis("Horizontal")*0.1f);
		
		
	}
	
	void MouseInput()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Instantiate(Bullet);
		}
	}
	
	
	void approachPosition(Camera cam, Vector3 pos, float speed)
	{
		Vector3 temp = cam.transform.position;
		if (temp.x == pos.x && temp.y == pos.y)
			return;
		if (Mathf.Abs(temp.x-pos.x) < speed)
			temp.x = pos.x;
		if (Mathf.Abs(temp.y-pos.y) < speed)
			temp.y = pos.y;
		
		if (temp.x < pos.x) temp.x += speed;
		else if (temp.x > pos.x) temp.x -= speed;
		
		if (temp.y < pos.y) temp.y += speed;
		else if (temp.y > pos.y) temp.y -= speed;
		
		cam.transform.position = temp;
		
	} 
	
	float approachAngle (float angle, float destination, float speed)
	{
		if (angle == destination)
			return angle;
		
		if (angle < 0)
			angle = 360+angle;
		else if (angle > 360)
			angle = angle - 360;
		
		if (destination > 180)
		{
			if (angle > destination-180 && angle < destination)
				angle += speed;
			else
				angle -= speed;
		}
		else if (destination <= 180)
		{
			if (angle < destination + 180 && angle > destination)
				angle -= speed;
			else
				angle += speed;
		}
		return angle;
	}
}


//float leftRight = Input.GetAxis("Horizontal");
		//float forwardBack = Input.GetAxis("Vertical");
		//CharacterController cc = (CharacterController)GetComponent("player1");
		//CharacterController cc = GetComponent<CharacterController>();
		//cc.Move(transform.forward * forwardBack * speed * Time.deltaTime);
		//cc.Move(transform.up * forwardBack * Time.deltaTime);
		//cc.Move (transform.right * leftRight * Time.deltaTime);