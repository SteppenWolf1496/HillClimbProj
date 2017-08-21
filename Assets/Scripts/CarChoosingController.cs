using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarChoosingController : SingletonePattern<CarChoosingController>
{

    private static List<TruckControll> cars = new List<TruckControll>();
    public static int index = 0;
    public static TruckControll currentCar = null;
    [SerializeField] protected MainGUI mainGui;

    public string CurrentCarKey
    {
        get { return currentCar.Key; }
    }
	// Use this for initialization
	void Start ()
	{
	    cars = new List<TruckControll>(GetComponentsInChildren<TruckControll>());
	    foreach (var car in cars)
	    {
	        car.gameObject.SetActive(false);

        }

	    currentCar = cars[index];
	    
	    currentCar.gameObject.SetActive(true);
	    mainGui.UpdateButtons();


        StartCoroutine(StartCar());
    }

    private IEnumerator StartCar()
    {
        while (currentCar.inited == false)
        {
            yield return new WaitForSeconds(1);

        }
        currentCar.makeDEMO();
    }

    public void NextCar()
    {
        index++;
        if (index >= cars.Count)
        {
            index = 0;
        }
        Vector3 tmpPos = currentCar.transform.position;
        currentCar.gameObject.SetActive(false);
        currentCar = cars[index];
        currentCar.makeDEMO();
        currentCar.transform.position = tmpPos + Vector3.up * 3;
        currentCar.gameObject.SetActive(true);
    }

    public void PrewCar()
    {
        index--;
        if (index < 0)
        {
            index = cars.Count-1;
        }
        Vector3 tmpPos = currentCar.transform.position;
        currentCar.gameObject.SetActive(false);
        currentCar = cars[index];
        currentCar.makeDEMO();
        currentCar.transform.position = tmpPos + Vector3.up * 3;
        currentCar.gameObject.SetActive(true);
    }
}
