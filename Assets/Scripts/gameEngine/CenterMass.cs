using UnityEngine;
using System.Collections;

public class CenterMass : MonoBehaviour {

    public Transform COM;  

//-----------------------------

    void Start () {
       rigidbody.centerOfMass = COM.localPosition;
}
	
	// Update is called once per frame
	void Update () {
	
	}
}
