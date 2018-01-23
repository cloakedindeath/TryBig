using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProto : MonoBehaviour 
{
	public Rigidbody projectile;
	public float speed = 20;

	void Update()
	{
		if(Input.GetMouseButtonDown(0))
		{
			Rigidbody instantiatedProjectile = Instantiate (projectile,
				                                   transform.position, 
													transform.rotation)
														as Rigidbody;
			instantiatedProjectile.velocity = transform.TransformDirection (new Vector3 (0, 0, speed));
		}
	}

} 
