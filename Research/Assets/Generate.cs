using UnityEngine;
using System.Collections;

public class Generate : MonoBehaviour {
	GameObject player;
	public Vector3 spawn;
	public static int numSpawn = 0;
	public Texture2D tex;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if(Vector3.Distance(player.transform.position,gameObject.transform.position) < 3.0f && Input.GetKeyDown(KeyCode.E) && numSpawn == 0){
			GameObject newSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			newSphere.transform.position = spawn;
			newSphere.transform.rotation = Quaternion.Euler(0,0,0);
			newSphere.AddComponent<Rigidbody>();
			newSphere.AddComponent<Programmable>();
			newSphere.GetComponent<Programmable>().spawn = spawn;
			numSpawn ++;
			newSphere.renderer.material.mainTexture = tex;
			if(Application.loadedLevelName == "Scene2")
				Dialogue.nextLine();
		}
	}
}