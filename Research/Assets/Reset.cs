using UnityEngine;
using System.Collections;

public class Reset : MonoBehaviour {

	public Vector3 spawn;
	public string nextLvl;
	Generate g;

	// Use this for initialization
	void Start () {
		if (g != null) {
						g = gameObject.GetComponent<Generate> ();
						spawn = g.spawn;
				}
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.transform.position.y < -50.0f) {
			Application.LoadLevel(nextLvl);
			Dialogue.nextLevel();
			Generate.numSpawn = 0;
				}
	}
}
