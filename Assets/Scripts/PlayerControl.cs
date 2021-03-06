﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerControl : MonoBehaviour
{


	Animator anim;
	public GameControl gameControl;
//	public SpecialMoveControl specialMoveControl;
	public SpecialMoveTest specialMoveTest;
	public GameObject Blockers;
	public Text ScoreText;
	public Text HiScoreText;
	public LayerMask currentLayer;
	public Transform BlockerCheck;
	public RaycastHit2D hit;
	public Vector2 backBlockForce;
	
	public int score = 0;
	int hiscore = 0;

	public float moveForce = 10f;
	// float moveSpeed = 10f;
	public int currentLane;

	public float laneChangeCost = 7.5f;
	public float stamina = 100f;
	public Slider StaminaSlider;
	public Color StaminaColor;
	float hInput = 0;


	// Use this for initialization
	void Start ()
	{

		if (PlayerPrefs.HasKey ("hiscore")) {
			hiscore = PlayerPrefs.GetInt ("hiscore");
			HiScoreText.text = hiscore.ToString ();
		}

		currentLane = Random.Range (1, 4);
		transform.position = new Vector3 (0, gameControl.lanesVector [currentLane].y, gameControl.lanesVector [currentLane].z);
		gameObject.layer = currentLane + 7;
		currentLayer = 1 << gameObject.layer;

		anim = GetComponent<Animator> ();

	}


	// Update is called once per frame
	void Update ()
	{

//		Debug.Log(LayerMask.LayerToName(gameObject.layer));

		#if UNITY_EDITOR

		hInput = Input.GetAxisRaw ("Horizontal");

		if (hInput > 0)
			Movement (hInput);

		if (Input.GetButtonDown ("LaneUp")) {
			ChangeLane ("up");
		}
		
		if (Input.GetButtonDown ("LaneDown")) {
			ChangeLane ("down");
		}

		#endif

		if (GetComponent<Rigidbody2D> ().velocity.x > 5f)
			GetComponent<Rigidbody2D> ().velocity = new Vector3 (5f, 0, 0);
		if (GetComponent<Rigidbody2D> ().velocity.x < -5f)
			GetComponent<Rigidbody2D> ().velocity = new Vector3 (-5f, 0, 0);

		#if UNITY_ANDROID
//		Movement (hInput);
		#endif

		UpdateStamina ();

		if (gameControl.areThereBlockers) {
			Blockers = gameControl.ActiveBlockers;
			if (transform.position.x > Blockers.transform.position.x && !gameControl.scoreUpdated)
				UpdateScore ();
		}
	
		if (stamina < 0)
			gameControl.EndJam ("No more stamina");

		if (transform.position.y > 1)
			gameControl.EndJam ("Cutting!");

		CheckForBlockers ();

		if (Input.GetKeyUp ("right")) {
			anim.SetBool ("skate", false);
		}

//		print (currentLane);

	}

	void Movement (float hInput)
	{
		if (stamina > 0 && !gameControl.endJam) {
			GetComponent<Rigidbody2D> ().AddForce (new Vector2 ((this.hInput * moveForce), 0));
			anim.SetBool ("skate", true);
		}
		//Debug.Log("Movement is running. hInput is " + hInput);
		//rigidbody.velocity = new Vector3((move * moveSpeed), 0, 0);
	}

	public void Move (float horizontalInput)
	{
		hInput = horizontalInput;
		Debug.Log ("Pressing button. horizontalInput is " + horizontalInput + ", hInput is " + hInput);
	}

	public void ChangeLane (string dir)
	{
		if (!gameControl.endJam) {
			stamina = stamina - laneChangeCost;
			if (dir == "up" && currentLane > 0) {
				currentLane--;
				transform.position = new Vector3 (transform.position.x, gameControl.lanesVector [currentLane].y, gameControl.lanesVector [currentLane].z);
				gameObject.layer = currentLane + 7;
			}

			if (dir == "up" && currentLane < 1 && !gameControl.endJam) {
				//	transform.position = new Vector2(transform.position.x, 1.1f);
				gameControl.EndJam ("Cutting!");
				gameObject.layer = 12;
			}

			if (dir == "down" && currentLane < 5) {
				currentLane++;
				transform.position = new Vector3 (transform.position.x, gameControl.lanesVector [currentLane].y, gameControl.lanesVector [currentLane].z);
				gameObject.layer = currentLane + 7;
			}

			if (dir == "down" && currentLane > 4 && !gameControl.endJam) {
				gameControl.EndJam ("Cutting!");
				gameObject.layer = 11;
			}
		}

		currentLayer = 1 << gameObject.layer;
//		print (LayerMask.LayerToName (gameObject.layer));
	}

	void UpdateStamina ()
	{
		if (!gameControl.endJam) {
			if (hInput > 0)
				stamina = stamina - (hInput / 50);
			else if (stamina < 100)
				stamina = stamina + 0.1f;
		}

		StaminaSlider.value = stamina;
		StaminaColor = new Color (((100 - stamina) / 100), (stamina / 100), 0);
		StaminaSlider.fillRect.GetComponentInChildren<Image> ().color = StaminaColor;
	}

	void UpdateScore ()
	{
		if (!gameControl.endJam) {
			score = score + 4;
			ScoreText.text = score.ToString ();
			gameControl.scoreUpdated = true;
			if (score > hiscore) {
				PlayerPrefs.SetInt ("hiscore", score);
				HiScoreText.text = score.ToString ();
			}
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.tag == "powerUp" && !gameControl.endJam) {
			stamina = stamina + 25;
			other.gameObject.SetActive (false);
		}
	}

	void CheckForBlockers ()
	{

		hit = Physics2D.Raycast (BlockerCheck.transform.position, Vector2.right, 9f);

		if (hit) {

			if (hit.collider.tag == "blocker" && gameControl.ActiveBlockers.name == "Blockers06") {
				if ((currentLane == 1) || (currentLane == 4)) {
//				specialMoveControl.specialMoveIsActive = true;
//				specialMoveTest.activateSpecialMove = true;
					specialMoveTest.specialMoveText.text = "OUTSIDE!";
					specialMoveTest.anim.SetTrigger ("textTrigger");
					specialMoveTest.SpecialMove ();
				} else {
					specialMoveTest.specialMoveText.text = "SQUEEZE!";
					specialMoveTest.anim.SetTrigger ("textTrigger");
					specialMoveTest.SpecialMove ();
				}
			
			}

		} else if (!hit) {

		}

//		Debug.Log(hit);
		Debug.DrawRay (BlockerCheck.transform.position, Vector2.right * 5f);
				
	}

	void OnCollisionEnter2D (Collision2D other)
	{
		if (!gameControl.endJam && other.gameObject.tag == "blocker" && GetComponent<Rigidbody2D> ().velocity.x > 0) {
			other.gameObject.GetComponent<Rigidbody2D> ().AddForce (backBlockForce);
			other.gameObject.GetComponent<Animator> ().SetTrigger ("fall");
			gameControl.EndJam ("Back block!");
		}
	}

}
