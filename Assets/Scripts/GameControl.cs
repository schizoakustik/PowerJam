using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameControl : MonoBehaviour {

	//PlayerControl PlayerControl;
	public float[] lanes;
	public Vector3[] lanesVector;
	public GameObject Jammer;
	public GameObject[] blockers;
	public GameObject ActiveBlockers;
	public GameObject PowerUp;
	public Button RestartButton;
	//public GameObject SpecialMovePanel;

//	public Image SpecialMoveButton;
//	bool specialMoveIsActive = false;
//	float specialMoveWarmUpTime = 1.0f;
//	float specialMoveWindow;
//	float specialMoveLastTime;
//	bool specialMoveLastTimeIsSet = false;

	public bool areThereBlockers = false;
	public bool scoreUpdated = false;
	public bool endJam;
	public float distanceToBlockers;
	float timeVariable;

	//JAM TIMER
	float jamTimer;
	public Text JamTimerText;
	float minutes, seconds;

	//SPAWNING BLOCKERS
//	float blockerLastTime = 0;
//	float blockerSpawnTime = 4f;
	float distanceToNextPack = 15f;
	float jammerPos;

	float powerUpLastTime = 0;
	float powerUpSpawnTime = 5f;

	void Start(){
//		SpecialMoveAnim = SpecialMovePanel.GetComponent<Animator>();
		endJam = false;
		jamTimer = 120f;

	}

	void ActivateBlockers(){
		// Activate a random set of blockers from pool.
		int rand = Random.Range(0, 4);
	//	int rand = 0;
		ActiveBlockers = blockers[rand];
		ActiveBlockers.transform.position = new Vector3(Jammer.transform.position.x + distanceToBlockers, 0f, 0f);
		ActiveBlockers.SetActive(true);
		scoreUpdated = false;
		areThereBlockers = ActiveBlockers.activeSelf;


	}

	void ActivePowerUp(){
		int rand = Random.Range(0, 9);
		if (rand >= 5){
			int randomLane = Random.Range(1, 4);
			PowerUp.transform.position = new Vector2((Jammer.transform.position.x + Random.Range(5, 9)), lanes[randomLane]);
			PowerUp.gameObject.SetActive(true);}
		}

//	public void ActivateSpecialMove(){
//		// A button with a closing ring appears. Get the timing right and the jammer will perform a special move, otherwise it's a penalty.
//
//		SpecialMoveButton.color = Color.yellow;
//		specialMoveIsActive = true;
//		if (!specialMoveLastTimeIsSet){
//			specialMoveLastTime = timeVariable;
//			specialMoveLastTimeIsSet = true;
//		}
//
//	}
//
//	public void DeactivateSpecialMove(){
//		SpecialMoveButton.color = new Color(1, 1, 0, 0);
//	}


	void Update(){
		timeVariable += 1*Time.deltaTime;
		jammerPos = Jammer.transform.position.x;

//		if ((timeVariable - blockerLastTime) > blockerSpawnTime && !endJam){
		if (jammerPos > distanceToNextPack){
			ActivateBlockers();
			distanceToNextPack = distanceToNextPack + 15;
//			blockerLastTime = timeVariable;
		}

		if ((timeVariable - powerUpLastTime) > powerUpSpawnTime && !endJam){
			ActivePowerUp();
			powerUpLastTime = timeVariable;
		}

//		if (specialMoveIsActive){
//			specialMoveWindow = Random.Range(0, 1);
//			if((timeVariable - specialMoveWindow) > specialMoveLastTime){
//				SpecialMoveButton.color = Color.green;
//			}
//		}

		if (!endJam){
			minutes = Mathf.FloorToInt((jamTimer - timeVariable) / 60F);
			seconds = Mathf.FloorToInt((jamTimer - timeVariable) - minutes * 60);

			string formattedTimer = string.Format("{0:0}:{1:00}", minutes, seconds);

			JamTimerText.text = formattedTimer;
		}

		if ((jamTimer - timeVariable) < 0 && !endJam)
			EndJam("End of jam!");

	}

	public void EndJam(string msg){

		endJam = true;
		JamTimerText.text = msg;
		RestartButton.gameObject.SetActive(true);

	}

	public void Restart(){

		Application.LoadLevel(0);
	}

}
