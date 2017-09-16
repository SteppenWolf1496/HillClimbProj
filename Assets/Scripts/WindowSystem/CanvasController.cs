using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public static Canvas MainCanvas;

	// Use this for initialization
	void Awake ()
	{

	    MainCanvas = GetComponent<Canvas>();
	}


}
