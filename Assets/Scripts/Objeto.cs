using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objeto : MonoBehaviour {

	public float fuerza;
	public Vector3 direccion;

	public bool EnZonaViento=false;
	Rigidbody2D rb;


	private void Start()
	{
		rb=GetComponent<Rigidbody2D>();

		rb.AddForce(new Vector3(20,10,0)*10);
	}


	void FixedUpdate()
	{
		if(EnZonaViento)
		{
			rb.AddForce(direccion*fuerza);
		}
	}


	void OnTriggerEnter2D(Collider2D colision)
	{
		if(colision.gameObject.tag=="Viento")
		{
			EnZonaViento=true;
		}
	}

	void OnTriggerExit2D(Collider2D colision)
	{
		if(colision.gameObject.tag=="Viento")
		{
			EnZonaViento=false;
		}
	}

}
