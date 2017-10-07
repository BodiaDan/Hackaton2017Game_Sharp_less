using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour {

	public float speedPlayer = 20f;
	public bool isInJump = false;
	public float upForce = 200f;
	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame

    void Update () {
		if (Input.GetKey (KeyCode.D)) {
			transform.position += Vector3.right * speedPlayer * Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.A)) {
			transform.position += Vector3.left * speedPlayer * Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.W) && !isInJump) {
			Debug.Log ("jump");
			isInJump = true;
			GetComponent<Rigidbody>().AddForce(new Vector3( 0, upForce, 0));
		}
		if (Input.GetKey (KeyCode.S) && !isInJump) {
			Debug.Log ("duck");
		}
	}

    void OnCollisionEnter(Collision collision) {
		Debug.Log (collision.transform.tag);
		if (collision.transform.root.tag == "Ground") {
			isInJump = false;
		}
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "DeathZone")
        {
            Destroy(obj: gameObject);
        }
        
    }

}
