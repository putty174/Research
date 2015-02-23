using UnityEngine;
using System.Collections;

public class Detector : MonoBehaviour {
	public bool coll;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision col){
		coll = true;
	}

	void OnCollisionExit(Collision col){
		coll = false;
	}
}