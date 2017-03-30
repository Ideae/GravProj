using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PlatformController : MonoBehaviour {
	public Vector3 velo;
	// Use this for initialization
	public GameObject[] platforms;
	//public List<GameObject> plats;
	public Vector3 destination;
	int counter;
	
	void Start () {
		velo = new Vector3(0,0,0);
		//platforms = GameObject.FindGameObjectsWithTag("Platform");
		destination = transform.position;
		//int[] ar1 = new int[7];
		counter = 0;
		//ar1 = generateRandMapping(ar1);
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 temp = transform.position;
		temp += velo;
		//transform.position = temp;
		
		approachPosition(this.gameObject,destination,0.05f);
	}

  
	
	void approachPosition(GameObject obj, Vector3 pos, float speed)
	{
		Vector3 temp = obj.transform.position;
		if (temp.x == pos.x && temp.y == pos.y)
		{
			//findNextDestination();
			return;
		}
		if (Mathf.Abs(temp.x-pos.x) < speed)
			temp.x = pos.x;
		if (Mathf.Abs(temp.y-pos.y) < speed)
			temp.y = pos.y;
		
		if (temp.x < pos.x) temp.x += speed;
		else if (temp.x > pos.x) temp.x -= speed;
		
		if (temp.y < pos.y) temp.y += speed;
		else if (temp.y > pos.y) temp.y -= speed;
		
		obj.transform.position = temp;
		
	}
	
	void findNextDestination()
	{
		platforms = GameObject.FindGameObjectsWithTag("Platform");
		int index = 0;
		for (int i = 0; i < platforms.Length; i++)
		{
			if (platforms[i] == this.gameObject)
			{
				index = i;
				break;
			}
		}
		index = (index+1) % (platforms.Length);
		destination = platforms[index].transform.position;
	}
	
	
}
