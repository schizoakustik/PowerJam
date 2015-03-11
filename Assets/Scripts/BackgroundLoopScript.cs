using UnityEngine;
using System.Collections;

public class BackgroundLoopScript : MonoBehaviour {

	public Transform Jammer;
	float distance;
	private Vector3 backPos;
	public float width = 11f;
	public float height = 0f;
	private float X;
	private float Y;

	void Update(){
		distance = transform.position.x - Jammer.position.x;
//		print (distance);
	}

	void OnBecameInvisible(){
		backPos = gameObject.transform.position;
		X = backPos.x + width*2;
		Y = backPos.y + height*2;
		if (gameObject.name == "LowerTape")
			gameObject.transform.position = new Vector3(X, Y, -0.35f);
		else if (gameObject.name == "UpperTape")
			gameObject.transform.position = new Vector3(X, Y, 0.35f);
		else
			gameObject.transform.position = new Vector2(X, Y);
	}
}
