using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpecialMoveControl : MonoBehaviour {

	Animator anim;
	PlayerControl playerControl;
	public Image button;
	float timeVariable;
	public GameObject Jammer;

	//float yellowTime, greenTime, redTime, endTime;
	public float yellowDistance, greenDistance, redDistance;
	bool yellowState, greenState, redState;
	public bool specialMoveIsActive = false;
	int specialMoveCounter = 0;
	
	
	// Use this for initialization
	void Start () {
		anim = GameObject.Find ("SpecialMovePanel").GetComponent<Animator>();
		playerControl = Jammer.GetComponent<PlayerControl>();
	}
	
	// Update is called once per frame
	void Update () {
		timeVariable += 1*Time.deltaTime;

		if (specialMoveIsActive){
			if(playerControl.hit.distance < yellowDistance){
				TurnYellow();

			}
			
			if(playerControl.hit.distance < greenDistance){
				TurnGreen();
				//greenTime = timeVariable;
			}
			
			if (playerControl.hit.distance < redDistance){
				TurnRed();
				//redTime = timeVariable;
			}
			
//			if ((timeVariable - endTime) > redTime){
//				EndCycle();
//			}

			if (yellowState && (playerControl.currentLane == 1 || playerControl.currentLane == 4)){
				anim.SetTrigger("outsideText");
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
//		Debug.Log(redTime);
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
//		redTime = Random.Range(2.4f, 2.8f);
		specialMoveIsActive = false;
		specialMoveCounter++;
	}
	
}

