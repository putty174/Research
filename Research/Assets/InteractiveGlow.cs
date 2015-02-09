using UnityEngine;
using System.Collections;

public class InteractiveGlow : MonoBehaviour {
	GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance(player.transform.position,gameObject.transform.position) < 3.0f && Generate.numSpawn == 0) {
			gameObject.GetComponent<ParticleSystem>().enableEmission = true;
		} else {
			gameObject.GetComponent<ParticleSystem>().enableEmission = false;
		}
	}
}
