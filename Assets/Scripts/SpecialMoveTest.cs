using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpecialMoveTest : MonoBehaviour
{
	public Animator anim;
	public GameControl gameControl;
	public Image Button;
	public Image Ring;
	public Text specialMoveText;
	public string specialMoveName;
	public Transform Jammer;
//	public Text ResultText, SuccessScore, FailScore;
//	int successScore = 0, failScore = 0;
	public bool activateSpecialMove;
	float speed;
	float size;
	bool greenState, redState;


	// Use this for initialization
	void Start ()
	{
		speed = (Random.Range (200, 350));
		print (speed);
		specialMoveText = GetComponentInChildren<Text> ();

	}

	void Update ()
	{

		if (activateSpecialMove) {
			SpecialMove ();
		}
	}

	public void SpecialMove ()
	{
		activateSpecialMove = true;
		size = Mathf.PingPong (speed * Time.time, 200);
		
		Ring.GetComponent<RectTransform> ().sizeDelta = new Vector2 (size, size);
		
		if (size > 70 && size < 130) {
			Button.color = Color.green;
			redState = false;
			greenState = true;
		} else {
			Button.color = Color.red;
			greenState = false;
			redState = true;
		}
		
		if (Input.GetButtonDown ("Jump")) {
			if (redState) {
				//					ResultText.color = new Color32(54, 0, 0, 255);
				//					ResultText.text = "FAIL";
				//					failScore++;
				//					FailScore.text = failScore.ToString();
				print ("FAIL");
				activateSpecialMove = false;
			}
			
			if (greenState) {
				print ("SUCCESS");
				Jammer.position = new Vector3 (gameControl.ActiveBlockers.transform.position.x + 1, Jammer.position.y, Jammer.position.z);
				activateSpecialMove = false;
				//					ResultText.color = new Color32(0, 54, 0, 255);
				//					ResultText.text = "SUCCESS";
				//					successScore++;
				//					SuccessScore.text = successScore.ToString();
			}
		}
	}
}
