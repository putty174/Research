using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {

	public GameObject door;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter (Collider other) {
		if (string.Compare (other.name, "Sphere") == 0) {
			door.transform.position = new Vector3(door.transform.position.x + 3f, door.transform.position.y,door.transform.position.z);
			Dialogue.nextLine();
				}
	}
}
