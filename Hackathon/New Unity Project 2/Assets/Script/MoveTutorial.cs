using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MoveTutorial : MonoBehaviour {

	public GameObject body;
	public float speedPlayer = 20f;
	public float upForce = 200f;
	public bool isInJump = false;

	public bool isHide = false;
	public Vector3 previousPosition;
	public bool rightDirection = true;

	public float stateParameter = 0f;
	public int state = 0; // 0- stay 1 -lying 2- jump
	public bool blockChangeAnim = false;
	public bool isThwowing = false;
	public GameObject objForThrow;
	public Transform posForThrow;
	public Animator animator;

	public int currentIndex = 0;
	public Text tasks;
	public UIElements elements;

	public GameObject fish;
	public Transform home;

	void Start () {
		animator = GetComponentInChildren<Animator> ();
	}

	// Update is called once per frame

	void Update () {
		if (!blockChangeAnim) {
			switch (state) {
			case 0:
				stateParameter = 0f;
				break;
			case 1: 
				stateParameter = 0.375f;
				break;
			case 2:
				stateParameter = 0.75f;
				break;
			}
		}

		if (Input.GetKey (KeyCode.D)) {
			transform.position += Vector3.right * speedPlayer * Time.deltaTime;
			switch (state) {
			case 0:
				stateParameter = 0.125f;
				break;
			case 1: 
				StopCoroutine ("GoLying");
				stateParameter = 0.5f;
				animator.SetFloat ("stateParameter", stateParameter);
				print(animator.GetFloat ("stateParameter"));
				break;
			}
			if (!rightDirection) {
				body.transform.Rotate (new Vector3 (0, 180, 0));
				rightDirection = true;
			}
		}

		if (Input.GetKey (KeyCode.A)) {
			transform.position += Vector3.left * speedPlayer * Time.deltaTime;
			switch (state) {
			case 0:
				stateParameter = 0.125f;
				break;
			case 1: 
				StopCoroutine ("GoLying");
				stateParameter = 0.5f;
				animator.SetFloat ("stateParameter", stateParameter);
				print(animator.GetFloat ("stateParameter"));
				break;
			}
			if (rightDirection) {
				body.transform.Rotate (new Vector3 (0, 180, 0));
				rightDirection = false;
			}
		}
		if (Input.GetKey (KeyCode.W) && !isInJump && state != 1 && currentIndex >= 1 ) {
			isInJump = true;
			state = 2;
			StartCoroutine ("GoJump");
			GetComponent<Rigidbody>().AddForce(new Vector3( 0, upForce, 0));
		}
		if (Input.GetKeyDown (KeyCode.S) && !isInJump  && currentIndex >= 2 ) {
			state = 1;
			StartCoroutine ("GoLying");
		}

		if (Input.GetKeyUp (KeyCode.D) || Input.GetKeyUp (KeyCode.A) && currentIndex >= 2 ) {
			if (!blockChangeAnim) {
				if (state == 0) {
					stateParameter = 0f;
				}
			} else if (state == 1) {
				stateParameter = 0.375f;
				animator.SetFloat ("stateParameter", stateParameter);
			}
		}

		if (Input.GetKeyUp (KeyCode.S) && state == 1) {
			state = 0;
			StopCoroutine ("GoLying");
			blockChangeAnim = false;
			stateParameter = 0;

		}
		if (state == 0 && Input.GetKeyDown(KeyCode.Space) && !isThwowing && currentIndex >= 4) {
			isThwowing = true;
			StartCoroutine ("ThrowGem");
		}
		if (!blockChangeAnim) {
			animator.SetFloat ("stateParameter", stateParameter);
		}
	}

	IEnumerator ThrowGem() {
		print ("here");
		blockChangeAnim = true;
		stateParameter = 1f;
		animator.SetFloat ("stateParameter", stateParameter);
		yield return new WaitForSeconds (0.53f);
		print ("here2");
		stateParameter = 0f;
		state = 0;
		animator.SetFloat ("stateParameter", stateParameter);
		blockChangeAnim = false;
		isThwowing = false;
		GameObject obj =  Instantiate (objForThrow, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), transform.rotation) as GameObject;
		if (rightDirection) {
			obj.GetComponent<Rigidbody> ().AddForce (1000f, 300f, 0);
		} else {
			obj.GetComponent<Rigidbody> ().AddForce (-1000f, 300f, 0);
		}
		StopCoroutine ("ThrowGem");
	}

	IEnumerator GoJump() {
		blockChangeAnim = true;
		stateParameter = 0.625f;
		animator.SetFloat ("stateParameter", stateParameter);
		yield return new WaitForSeconds (0.2f);
		stateParameter = 0.75f;
		animator.SetFloat ("stateParameter", stateParameter);
	}

	IEnumerator GoLying() {
		blockChangeAnim = true;
		stateParameter = 0.25f;
		animator.SetFloat ("stateParameter", stateParameter);
		yield return new WaitForSeconds (0.2f);
		stateParameter = 0.375f;
		animator.SetFloat ("stateParameter", stateParameter);
	}

	void OnCollisionEnter(Collision collision) {
		Debug.Log (collision.transform.tag);
		if (collision.transform.root.tag == "Ground") {
			isInJump = false;
			StopCoroutine ("GoJump");
			stateParameter = 0;
			state = 0;
			blockChangeAnim = false;
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.transform.tag == "DeathZone") {
			Destroy(obj: gameObject);
		}
		if (other.transform.tag == "HideZone") {
			GameObject.Find ("UIController").GetComponent<UIElements> ().buttonE.SetActive (true);	
		}
		if (other.name == "bW" && currentIndex == 0) {
			currentIndex++;
			NextButton ();
		}
		if (other.name == "bS" && currentIndex == 1) {
			currentIndex++;
			NextButton ();
		}
		if (other.name == "goToHome" && currentIndex == 2) {
			transform.position =  new Vector3 (260f, transform.position.y, transform.position.z);
			currentIndex++;
			NextButton ();
		}

		if (other.name == "goBack" && currentIndex == 3) {
			currentIndex++;
			NextButton ();
			transform.position =  new Vector3 (100f, 4f, transform.position.z);

		}
	}

	void OnTriggerStay(Collider other) {
		if (Input.GetKeyDown (KeyCode.E) && other.tag == "HideZone") {
			if (!isHide) {
				isHide = true;
				HidePlayer (other.transform.position);
			} else {
				isHide = false;
				ShowPlayer ();
			}
		}
		if (other.name == "fish" && currentIndex == 3 && Input.GetKey(KeyCode.E)) {
			tasks.text = "Верніться назад";
			elements.buttonE.SetActive (false);
			fish.SetActive (false);
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.transform.tag == "HideZone" && currentIndex >= 3) {
			GameObject.Find ("UIController").GetComponent<UIElements> ().buttonE.SetActive (false);
		}
	}

	void HidePlayer(Vector3 newPos) {
		previousPosition = body.transform.position;
		body.transform.position = new Vector3 (transform.position.x, transform.position.y, newPos.z + 2f );
	}

	void ShowPlayer() {
		body.transform.position = previousPosition;
	}

	void NextButton() {
		switch (currentIndex) {
		case 1:
			elements.buttonA.SetActive (false);
			elements.buttonD.SetActive (false);
			elements.buttonW.SetActive (true);
			tasks.text = "Натисніть 'W' для стрибка ";
			break;
		case 2:
			elements.buttonW.SetActive (false);
			elements.buttonS.SetActive (true);
			tasks.text = "Натисніть 'S' для того, щоб присісти";
			break;
		case 3:
			elements.buttonS.SetActive (false);
			elements.buttonE.SetActive (true);
			tasks.text = "Натисніть 'E' для того, щоб взяти рибу";
			break;
		case 4:
			elements.buttonE.SetActive (false);
			elements.buttonSpace.SetActive (true);
			tasks.text = "Натисніть 'Space' для кидка. Попадіть в сніговика";
			break;
		case 5:
			elements.buttonSpace.SetActive (false);
			tasks.text = "Молодець";
			// загрузити рівень
			break;
		}

	}
}
