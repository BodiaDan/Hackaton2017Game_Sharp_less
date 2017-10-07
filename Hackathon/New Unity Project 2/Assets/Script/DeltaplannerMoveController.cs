using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeltaplannerMoveController : MonoBehaviour {
	public float deltaPlanSpeed = 20f;
	public float upForce = 200f;

	public float rotateNumber = 0.3f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.position += Vector3.forward * deltaPlanSpeed * Time.deltaTime;

		if (Input.GetKey(KeyCode.D)) {
			float tempRatateX = transform.rotation.x + rotateNumber * 2.5f;
			if (tempRatateX > 30f) {
				tempRatateX = 30f;
			} else if (tempRatateX < -30f) {
				tempRatateX = -30f;
			}
			float tempRatateY = transform.rotation.x - rotateNumber * 2.0f;
			if (tempRatateY > 40f) {
				tempRatateY = 40f;
			} else if (tempRatateX < -40f) {
				tempRatateY = -40f;
			}
			gameObject.transform.Rotate ( new Vector3 (0, tempRatateX, tempRatateY ));
		}

		if (Input.GetKey(KeyCode.A)) {
			float tempRatateX = transform.rotation.x - rotateNumber * 2.5f;
			if (tempRatateX > 30f) {
				tempRatateX = 30f;
			} else if (tempRatateX < -30f) {
				tempRatateX = -30f;
			}
			float tempRatateY = transform.rotation.x + rotateNumber * 2.0f;
			if (tempRatateY > 40f) {
				tempRatateY = 40f;
			} else if (tempRatateX < -40f) {
				tempRatateY = -40f;
			}
			gameObject.transform.Rotate ( new Vector3 (0, tempRatateX, tempRatateY ));

			//gameObject.transform.Rotate ( new Vector3 (0, Mathf.Clamp(transform.rotation.x - rotateNumber * 2.5f,-30, 30 ), Mathf.Clamp(transform.rotation.z + rotateNumber,-40, 40 ) ));		
		}
		if (transform.rotation.x < 19) {
			gameObject.transform.Rotate (new Vector3 (0.5f, 0, 0));
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.transform.tag == "UpPiont")
		{
			GetComponent<Rigidbody>().AddForce(new Vector3( 0, upForce, 0));
		}
		if (other.transform.tag == "Ground")
		{
			Destroy (gameObject);
		}

	}
	void OnCollisionEnter(Collision collision) {
		if (collision.transform.tag == "Ground")
		{
			Destroy (gameObject);
		}
	}

	void OnTriggerStay(Collider other) {
		
		if (other.transform.tag == "UpPiont" && transform.rotation.x > -19f) {
			transform.Rotate (new Vector3 (-1f, 0, 0));
		}
	}
}
