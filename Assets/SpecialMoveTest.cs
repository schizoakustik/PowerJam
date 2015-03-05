using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpecialMoveTest : MonoBehaviour {

	public Image button;
	float timeVariable;
	public float yellowTime, greenTime, redTime, endTime;
	bool yellowState, greenState, redState;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		timeVariable += 1*Time.deltaTime;

		if((timeVariable - yellowTime) > 0){
			TurnYellow();
			//yellowTime = timeVariable;
		}

		if((timeVariable - greenTime) > yellowTime){
			TurnGreen();
			//greenTime = timeVariable;
		}

		if ((timeVariable - redTime) > greenTime){
			TurnRed();
			//redTime = timeVariable;
		}

		if ((timeVariable - endTime) > redTime){
			EndCycle();
		}

		if (Input.GetButtonDown("Jump")){
			if (yellowState || redState){
				Debug.Log("FAIL");
			}

			if (greenState){
				Debug.Log("SUCCESS");
			}
		}

	}

	void ActivateButton(){
		// First, turn yellow to tell player to get ready.

		// Then, after a slightly random period of time turn green for a short time,
		// indicating that it's time to press the button.

		// Then, turn red to show that the time is up.

		// Pressing during yellow or red results in a failed attempt.
		// Pressing during green results in a successful attempt.
		// Not pressing at all has no result, i.e. game goes on as usual.

		// Three floats: yellowTime, greenTime, redTime?
		// At the start yellowTime is set to Time.time and is then tested against Time.time until the difference is... yellowTimeLength or w/e.
		// That makes 6 floats. Feels bulky.
	}

	void TurnYellow(){
		button.color = Color.yellow;
		yellowState = true;
	//	Debug.Log ("Yellow: " + Time.time);
	}

	void TurnGreen(){
		button.color = Color.green;
		yellowState = false;
		greenState = true;
		Debug.Log(redTime);
	}

	void TurnRed(){
		button.color = Color.red;
		greenState = false;
		redState = true;
//		Debug.Log("Red: " + Time.time);
	}

	void EndCycle(){
		button.color = new Color(0, 0, 0, 0);
		redState = false;
		timeVariable = 0;
		redTime = Random.Range(2.4f, 2.8f);
	}

}
