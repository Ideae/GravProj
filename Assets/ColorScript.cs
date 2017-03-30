using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
    var platforms = FindObjectsOfType<PlatformController>();
    int counter = 0;
    foreach(var p in platforms)
    {
      var mr = p.gameObject.GetComponent<MeshRenderer>();
      float hue = (float)counter++ / platforms.Length;
      //print(hue);
      mr.material.color = Color.HSVToRGB(hue, 1f, 1f);

      var light = p.gameObject.GetComponentInChildren<Light>();
      light.color = mr.material.color;

    }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
