using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Projectile : MonoBehaviour {

	
	public Rigidbody bulletPrefabs;
	public GameObject plane;
	public Transform shootPoint;
	public LayerMask layer;
	public LineRenderer lineVisual;
	public int lineSegment = 10;

    Ball ball;

    public Text VoX, VoY, VoZ, VientoX, VientoZ;


	private Camera cam;


	// Use this for initialization
	void Start () 
	{
		cam = Camera.main;	
		lineVisual.positionCount = lineSegment;
        ball = bulletPrefabs.gameObject.GetComponent<Ball>();
    }

    float desaceleration = 0;
    float desacelerationz = 0;

    // Update is called once per frame
    void Update () 
	{
        //LaunchProjectile();
        if (Input.GetKeyDown(KeyCode.Return)) {
            ClickCargar();
        }
	}

    void FixedUpdate() {

    }

    Vector3 Vo;
    Vector3 Viento;

    public void ClickCargar()
    {
        float vientox;
        if (VientoX.text == "") {
            vientox = 0;
        }
        else {
            vientox = float.Parse(VientoX.text);
        }
        float vientoz;
        if (VientoZ.text == "")
        {
            vientoz = 0;
        }
        else {
            vientoz = float.Parse(VientoZ.text);
        }


        Viento = new Vector3(vientox, 0, vientoz);

        desaceleration = (Viento.x) / bulletPrefabs.mass;
        desacelerationz = (Viento.z) / bulletPrefabs.mass;

        float vox;
        if (VoX.text == "")
        {
            vox = 0;
        }
        else {
            vox = float.Parse(VoX.text);
        }
        float voy;
        if (VoY.text == "")
        {
            voy = 0;
        }
        else
        {
            voy = float.Parse(VoY.text);
        }
        float voz;
        if (VoZ.text == "")
        {
            voz = 0;
        }
        else
        {
            voz = float.Parse(VoZ.text);
        }

        Vo = new Vector3(vox, voy, voz);
        Visualize(Vo);
        transform.rotation = Quaternion.LookRotation(Vo);
    }

    public void ClickDisparar() {
        Rigidbody obj = Instantiate(bulletPrefabs, shootPoint.position, Quaternion.identity);
        obj.velocity = Vo;
        obj.gameObject.GetComponent<Ball>().direccion = new Vector3(Viento.x, 0, Viento.z);
        Destroy(obj.gameObject, 10);
    }

    void LaunchProjectile()
    {


        Vector3 Vo = new Vector3(float.Parse(VoX.text), float.Parse(VoY.text), float.Parse(VoZ.text));

        //float vertdestime = Vo.y / Physics.gravity.y;
        //float vertdesdist = (-0.5f * Mathf.Abs(Physics.gravity.y) * Mathf.Pow(vertdestime, 2)) + (Vo.y * vertdestime) + shootPoint.position.y;
        //float diffdist = Mathf.Abs(plane.transform.position.y - vertdesdist);
        //float vertacctime = Mathf.Sqrt((2 * diffdist) / -Physics.gravity.y);
        //float finaltime = vertdestime + vertacctime;

        //Vector3 finalpos = CalculatePosInTime(Vo, finaltime);


        Visualize(Vo);


        transform.rotation = Quaternion.LookRotation(Vo); //this is for the canon lo face the cursor

        if (Input.GetMouseButtonDown(0)) //the left click is pressed will stop shooting
        {
            Rigidbody obj = Instantiate(bulletPrefabs, shootPoint.position, Quaternion.identity);
            obj.velocity = Vo;

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

		
		float sY = (-0.5f * Mathf.Abs(Physics.gravity.y) * Mathf.Pow(time, 2) ) + (vo.y * time) + shootPoint.position.y;
        float sX = (0.5f * desaceleration * Mathf.Pow(time, 2)) + (vo.x * time) + shootPoint.position.x;
        float sZ = (0.5f * desacelerationz * Mathf.Pow(time, 2)) + (vo.z * time) + shootPoint.position.z;

        Vector3 result = new Vector3(sX, sY, sZ);

        return result;
	}	

}
