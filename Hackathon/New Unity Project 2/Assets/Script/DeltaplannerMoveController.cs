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
		/*gameObject.transform.position += Vector3.forward * deltaPlanSpeed * Time.deltaTime;

		if (Input.GetKey(KeyCode.D)) {
			float tempRatateZ =  -rotateNumber * 2.5f;
			if (transform.localRotation.z > 30f) {
				tempRatateZ = 0f;
			} else if (transform.localRotation.z < -30f) {
				tempRatateZ = 0f;
			}
			float tempRatateY = rotateNumber * 2.0f;
			if (transform.localRotation.y > 40f) {
				tempRatateY = 0;
			} else if (transform.localRotation.y < -40f) {
				tempRatateY = 0f;
			}
			gameObject.transform.Rotate ( new Vector3 (0, tempRatateY, tempRatateZ ));
		}

		if (Input.GetKey(KeyCode.A)) {
			float tempRatateZ = rotateNumber * 2.5f;
			if (transform.localRotation.z > 30f) {
				tempRatateZ = 0;
			} else if (transform.localRotation.z < -30f) {
				tempRatateZ = 0;
			}
			float tempRatateY = -rotateNumber * 2.0f;
			if (transform.localRotation.y > 40f) {
				tempRatateY = 0;
			} else if (transform.localRotation.y < -0) {
				tempRatateY = -0;
			}
			gameObject.transform.Rotate ( new Vector3 (0, tempRatateY, tempRatateZ ));

			//gameObject.transform.Rotate ( new Vector3 (0, Mathf.Clamp(transform.rotation.x - rotateNumber * 2.5f,-30, 30 ), Mathf.Clamp(transform.rotation.z + rotateNumber,-40, 40 ) ));		
		}
		if (transform.localRotation.x < 19f) {
			gameObject.transform.Rotate (new Vector3 (1f, 0, 0));
		}*/
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
		
		if (other.transform.tag == "UpPiont" && transform.localRotation.x > -19f) {
			transform.Rotate (new Vector3 (1f, 0, 0));
		}
	}
}
