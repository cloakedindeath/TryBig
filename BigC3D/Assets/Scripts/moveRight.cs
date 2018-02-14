using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveRight : MonoBehaviour {
	public GameObject player;
	public int speed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.touchCount > 0)
		{
			var touch = Input.GetTouch(0);
			switch (touch.phase)
			{
			case TouchPhase.Began:
				// do something when touch began like :
				player.transform.Translate(-Vector3.right * speed * Time.deltaTime);
				print(touch.position.x);
				break;
			case TouchPhase.Ended:
				// do something when touch end like :
				print(touch.position.x);
				break;
			}
		}
	}
}
