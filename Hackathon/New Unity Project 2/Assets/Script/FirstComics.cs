using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FirstComics : MonoBehaviour {

	public Image[] images;

	// Use this for initialization
	void Start () {
		foreach (Image image in images) {
			image.gameObject.GetComponent<RectTransform> ().SetSizeWithCurrentAnchors (RectTransform.Axis.Vertical, Screen.height);
			image.gameObject.GetComponent<RectTransform> ().SetSizeWithCurrentAnchors (RectTransform.Axis.Horizontal, Screen.width);
			image.gameObject.SetActive (false);
		}
		StartCoroutine (Comics ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator Comics() {
		images [0].gameObject.SetActive (true);
		yield return new WaitForSeconds (2);
		images [1].gameObject.SetActive (false);
		images [1].gameObject.SetActive (true);
		yield return new WaitForSeconds (2);
		images [1].gameObject.SetActive (false);
		images [2].gameObject.SetActive (true);
		yield return new WaitForSeconds (2);
		Application.LoadLevel (1);
	}
}
