using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class TuningSlot : MonoBehaviour
{
    [SerializeField] private TextField header;

    [SerializeField] private Button change;

	/*// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}*/

    public void SetData(BaseCarModificator _modifData)
    {
        header.text = _modifData.Title;
    }
}
