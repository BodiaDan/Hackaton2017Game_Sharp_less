using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyEnemyController : MonoBehaviour {

	public Vector3 startPosition;
	public Vector3 endPosition;
	public float speed;
	public bool rightDirection = true;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x <= startPosition.x && !rightDirection) {
            transform.Rotate(new Vector3(0f, 180f, 0f));
            Debug.Log("start");
            rightDirection = true;

        }
        else if (transform.position.x >= endPosition.x && rightDirection) {
            Debug.Log("end");
            transform.Rotate(new Vector3(x: 0f, y: 180f, z: 0f));
            rightDirection = false;
        }
        if (rightDirection)
        {
            transform.position += (new Vector3(x: 1, y: 0, z: 0)) * speed * Time.deltaTime;
        }
        else {
            transform.position += (new Vector3(x: -1, y: 0, z: 0)) * speed * Time.deltaTime;
        }
    }
}
