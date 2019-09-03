using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	
	public Rigidbody bulletPrefabs;
	public GameObject cursor;
	public Transform shootPoint;
	public LayerMask layer;
	public LineRenderer lineVisual;
	public int lineSegment = 10;


	private Camera cam;


	// Use this for initialization
	void Start () 
	{
		cam = Camera.main;	
		lineVisual.positionCount = lineSegment;
	}
	
	// Update is called once per frame
	void Update () 
	{
		LaunchProjectile();
	}


	void LaunchProjectile()
	{
		Ray camRay = cam.ScreenPointToRay(Input.mousePosition);

		RaycastHit hit;

		if (Physics.Raycast(camRay, out hit, 100f, layer))
		{
			cursor.SetActive(true);
			cursor.transform.position = hit.point + Vector3.up * 0.1f;

			Vector3 Vo = CalculateVelocity(hit.point, shootPoint.position, 1f);

			Visualize(Vo);


			transform.rotation = Quaternion.LookRotation(Vo); //this is for the canon lo face the cursor

			if (Input.GetMouseButtonDown(0)) //the left click is pressed will stop shooting
			{
				Rigidbody obj = Instantiate(bulletPrefabs, shootPoint.position, Quaternion.identity);
				obj.velocity = Vo;
				
			}


		} else
		{
			cursor.SetActive(false);
		}

	}






	Vector3 CalculateVelocity(Vector3 target, Vector3 origin, float time)
	{
		//define the distance x and y first

		Vector3 distance = target - origin;
		Vector3 distanceXZ = distance;
		distanceXZ.y = 0f;

		//create a float that represents our distance
		float Sy = distance.y;
		float Sxz = distanceXZ.magnitude;

		float Vxz = Sxz / time;
		float Vy = Sy / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;

		Vector3 result = distanceXZ.normalized;
		result *= Vxz;
		result.y = Vy;

		return result;

	}


	void Visualize(Vector3 vo)
	{
		for (int i = 0; i < lineSegment; i++)
		{
			Vector3 pos = CalculatePosInTime(vo, i/(float)lineSegment);
			lineVisual.SetPosition(i, pos);
		}
	}


	Vector3 CalculatePosInTime(Vector3 vo, float time)
	{
		Vector3 Vxz = vo;
		Vxz.y = 0f;

		Vector3 result = shootPoint.position + vo * time;
		float sY = (-0.5f * Mathf.Abs(Physics.gravity.y) * Mathf.Pow(time, 2) ) + (vo.y * time) + shootPoint.position.y;
		
		result.y = sY;

		return result;
	}	

}
