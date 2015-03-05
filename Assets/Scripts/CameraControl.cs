using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	public GameObject Player;
	public float offset;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(Player.transform.position.x + offset, transform.position.y, -10);
	}
}
