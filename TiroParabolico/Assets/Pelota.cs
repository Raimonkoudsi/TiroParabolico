using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pelota : MonoBehaviour {

	public float fuerza;
	public Vector3 direccion;

	public bool EnZonaViento=false;
	Rigidbody rb;


	private void Start()
	{
		rb=GetComponent<Rigidbody>();

	}


	void FixedUpdate()
	{
		if(EnZonaViento)
		{
			rb.AddForce(direccion*fuerza);
		}
	}


	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Space))
        {
			this.rb.isKinematic=false;

			rb.AddForce(new Vector3(60,40,0)*10);
        }
	}


	void OnTriggerEnter(Collider colision)
	{
		if(colision.gameObject.tag=="Viento")
		{
			EnZonaViento=true;
		}
	}

	void OnTriggerExit(Collider colision)
	{
		if(colision.gameObject.tag=="Viento")
		{
			EnZonaViento=false;
		}
	}
}
