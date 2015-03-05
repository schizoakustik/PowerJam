using UnityEngine;
using System.Collections;

public class PackControl : MonoBehaviour {

	public GameControl gameControl;
	public GameObject Jammer;
	public float distance;
	
	// Update is called once per frame
	void Update () {
		if (Jammer.transform.position.x - gameObject.transform.position.x > distance){
			gameObject.SetActive(false);
		}
	}
}
