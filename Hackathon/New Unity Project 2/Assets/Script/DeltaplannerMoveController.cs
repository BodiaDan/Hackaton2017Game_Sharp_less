using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeltaplannerMoveController : MonoBehaviour {
	public float deltaPlanSpeed = 20f;
	public float upForce = 200f;
	public float rotateNumber = 0.3f;


	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Input.GetKey (KeyCode.A)) {
			if (transform.localEulerAngles.y > -40f)
				transform.localEulerAngles += new Vector3 (0, -0.9f, 0f);
			if (transform.localEulerAngles.z < 30f)
				transform.localEulerAngles += new Vector3 (0, 0, 0.9f);
		}

		if (Input.GetKey (KeyCode.D)) {
			if (transform.localEulerAngles.y < 40f)
				transform.localEulerAngles += new Vector3 (0, 0.9f, 0);

			if (transform.localEulerAngles.z > -30f)
				transform.localEulerAngles += new Vector3 (0, 0, -0.9f);
		}
		if (transform.localEulerAngles.x < 70f) {
			transform.localEulerAngles += new Vector3 (0.2f, 0, 0);
		}
		gameObject.transform.Translate (Vector3.forward * deltaPlanSpeed * Time.deltaTime);
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.transform.tag == "Ground")
		{
			Destroy (gameObject);
		}
	}

	void OnTriggerStay(Collider other) {
		if (transform.localEulerAngles.x > -50) {
			transform.localEulerAngles += new Vector3 (-1.2f, 0	, 0);
		}
	}
}
