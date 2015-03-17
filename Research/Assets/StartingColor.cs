using UnityEngine;
using System.Collections;

public class StartingColor : MonoBehaviour {
	public float r;
	public float g;
	public float b;

	// Use this for initialization
	void Start () {
		gameObject.renderer.material.color = new Color (r, g, b);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}