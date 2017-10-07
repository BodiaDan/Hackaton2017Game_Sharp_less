using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour {

	public float speedPlayer = 20f;
	public float upForce = 200f;
	public bool isInJump = false;

	public bool isHide = false;
	public Vector3 previousPosition;

	public float stateParameter = 0f;
	public int state = 0; // 0- stay 1 -lying 2- jump
	public bool blockChangeAnim = false;
	public Animator animator;
	void Start () {
		animator = GetComponent<Animator> ();
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

		}
		if (Input.GetKey (KeyCode.A)) {
			transform.position += Vector3.left * speedPlayer * Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.W) && !isInJump && state != 1) {
			isInJump = true;
			state = 2;
			StartCoroutine ("GoJump");
			GetComponent<Rigidbody>().AddForce(new Vector3( 0, upForce, 0));
		}
		if (Input.GetKeyDown (KeyCode.S) && !isInJump) {
		}

		if (!blockChangeAnim) {
			animator.SetFloat ("stateParameter", stateParameter);
		}
	}

	IEnumerator GoJump() {
		stateParameter = 0.625f;
		animator.SetFloat ("stateParameter", stateParameter);
		yield return new WaitForSeconds (0.417f);
		stateParameter = 0.75f;
		animator.SetFloat ("stateParameter", stateParameter);
		blockChangeAnim = false;
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
    }

	void OnTriggerStay(Collider other) {
		if (Input.GetKeyDown (KeyCode.E)) {
			if (!isHide) {
				isHide = true;
				HidePlayer (other.transform.position);
			} else {
				isHide = false;
				ShowPlayer ();
			}
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.transform.tag == "HideZone") {
			GameObject.Find ("UIController").GetComponent<UIElements> ().buttonE.SetActive (false);
		}
	}

	void HidePlayer(Vector3 newPos) {
		previousPosition = transform.position;
		transform.position = new Vector3 (transform.position.x, transform.position.y, newPos.z + 2f );
	}

	void ShowPlayer() {
		transform.position = previousPosition;
	}

}
