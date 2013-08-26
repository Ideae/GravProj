using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	
	public GameObject[] platforms;
	int timer;
	int counter;
	
	void Start () {
		timer = 0;
		counter = 0;
		platforms = GameObject.FindGameObjectsWithTag("Platform");
		string result = "";
		for (int i = 0; i < platforms.Length-1;i++)
		{
			for(int j = i + 1; j < platforms.Length;j++)
			{
				Physics.IgnoreCollision(platforms[i].collider,platforms[j].collider);
				
			}
		}
		
		//platforms[0].GetComponent<PlatformController>().velo = new Vector3 (-0.05f,0,0);
	}

	
	void Update () {
		
		if (timer < 600)
			timer++;
		else
		{
			timer = 0;
			assignNewDestinations();
		}
		Debug.Log(timer);
		
	}
	void assignNewDestinations()
	{
		GameObject[] plats = GameObject.FindGameObjectsWithTag("Platform");
		Vector3[] positions = new Vector3[plats.Length];
		int[] numbers = new int[plats.Length];
		numbers = generateRandMapping(numbers);
		
		for (int i = 0 ; i < positions.Length; i++)
		{
			positions[i] = plats[numbers[i]].transform.position;
		}
		for (int i = 0 ; i < positions.Length; i++)
		{
			plats[i].GetComponent<PlatformController>().destination = positions[i];
		}
		
	}
	
	int[] generateRandMapping(int[] array)
	{
		//Random.Range (0,array.Length);
		bool foundconflict = false;
		string result = "";
		counter = 0;
		for (int i = 0; i < array.Length;i++)
		{
			array[i] = array.Length + 10;
			while (true)
			{
				counter++;
				foundconflict = false;
				int nextRand = Random.Range (0,array.Length);
				//result += nextRand + " " ;
				if (nextRand == i)
					continue;
				else 
				{
					for (int j = 0; j < i; j++)
					{
						if (nextRand == array[j])
						{
							
							foundconflict = true;
						}
					}
					if (foundconflict)
					{
						if (counter>100) break;
						continue;
					}
				}
				
				array[i] = nextRand;
				
				break;
			}
		}
		
		for (int i = 0; i < array.Length;i++)
		{
			result += i + ":" + array[i] + " , ";
		}
		
		Debug.Log (result);
		return array;
	}
	
}
