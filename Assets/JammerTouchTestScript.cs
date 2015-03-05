using UnityEngine;
using System.Collections;

public class JammerTouchTestScript : MonoBehaviour {
		
	// Update is called once per frame
	void Update () {
		if (Input.touchCount > 0){
			for(int i = 0; i < Input.touchCount; i++){
				Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);
				transform.position = new Vector3(touchPosition.x, touchPosition.y, 0);
			}
		}
	}
}
