using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowman : MonoBehaviour {
	public Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		animator.SetFloat ("stateParameter", 0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision col){
		if (col.transform.tag == "Snow") {
			Destroy (col.gameObject);
			animator.SetFloat ("stateParameter", 1);
		}
	}
}
