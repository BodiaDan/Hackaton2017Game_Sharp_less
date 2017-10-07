using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIElements : MonoBehaviour {

	public GameObject buttonA; 
	public GameObject buttonD; 
	public GameObject buttonW; 
	public GameObject buttonS; 
	public GameObject buttonE; 
	public GameObject buttonSpace; 


	// Use this for initialization
	void Start () {
		buttonE.SetActive (false);
		buttonS.SetActive (false);
		buttonSpace.SetActive (false);
		buttonW.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
