using UnityEngine;
using System.Collections;

public class Detector : MonoBehaviour {
	public static bool coll;
    public static string color;

	// Use this for initialization
	void Start () {
		color = "none";
	}
	
	// Update is called once per frame
	void Update () {
	}

    void OnCollisionEnter (Collision col){
		coll = true;
        if (col.gameObject.renderer.material.color == Color.red)
            color = "Red";
        else if (col.gameObject.renderer.material.color.ToString().Equals("RGBA(1.000, 0.647, 0.000, 1.000)"))
            color = "Orange";
        else if (col.gameObject.renderer.material.color == Color.yellow)
            color = "Yellow";
        else if (col.gameObject.renderer.material.color == Color.green)
            color = "Green";
        else if (col.gameObject.renderer.material.color == Color.blue)
            color = "Blue";
        else if (col.gameObject.renderer.material.color.ToString().Equals("RGBA(0.545, 0.000, 0.800, 1.000)"))
            color = "Purple";
        else if (col.gameObject.renderer.material.color == Color.black)
            color = "Black";
        else if (col.gameObject.renderer.material.color == Color.white)
            color = "White";
        else if (col.gameObject.renderer.material.color == Color.cyan)
            color = "Cyan";
        else if (col.gameObject.renderer.material.color == Color.magenta)
            color = "Magenta";
        else if (col.gameObject.renderer.material.color == Color.gray)
            color = "Gray";
        else if (col.gameObject.renderer.material.color == Color.grey)
            color = "Grey";
    }
    
    void OnCollisionExit(Collision col){
        coll = false;
    }
}