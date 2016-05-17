using UnityEngine;
using System.Collections;
using Enums;

public class ForcesController : MonoBehaviour
{
	Rigidbody rb;
	

	

	public float steerAngle = 32;
	public float maxTorque = 30000;
	

	public float force = 10000;

	public float GForce;

    

    // Use this for initialization
    void Start ()
	{
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	
	void Update ()
	{

		

	   

	}
}
