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

		rb.AddForce(new Vector3(20,10,0)*10);
	}


	void FixedUpdate()
	{
		if(EnZonaViento)
		{
			rb.AddForce(direccion*fuerza);
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
