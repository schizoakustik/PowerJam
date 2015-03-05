using UnityEngine;
using System.Collections;

public class PowerUpControl : MonoBehaviour {

	void OnBecameInvisible(){
		gameObject.SetActive(false);

	}
}
