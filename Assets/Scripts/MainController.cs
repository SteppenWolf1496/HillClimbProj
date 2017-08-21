using UnityEngine;
using System.Collections;
using Endless2DTerrain;

public class MainController : MonoBehaviour
{

	// Use this for initialization
	public static Camera mainCamera;
    //[SerializeField] public GameObject LobbyGui;
   // [SerializeField] public GameObject Gamegui;
    //[SerializeField] public Canvas GuiCanvas;
	private Vector3 cameraStartPos;
    private static MainController inst;

    public static int carNum;

    private TerrainDisplayer terrain = null;

    public void resetCamera ()
	{
			mainCamera.transform.position = cameraStartPos;
	}

    public static MainController instance ()
	{
			return inst;
	}

    void Start ()
	{
		mainCamera = Camera.main;
		Application.targetFrameRate = 30;
	    Random.InitState(1);
		inst = this;
	    cameraStartPos = transform.position;
        //ScreenManager.showScreen (ScreenManager.Screens.START_MENU);
       
	    terrain = GetComponent<TerrainDisplayer>();
        if(terrain)
	    terrain.Setup();

        StartCoroutine(StartTerrain());

    }

    private IEnumerator StartTerrain()
    {
        while (terrain == null)
        {
            yield return new WaitForEndOfFrame();
            terrain = GetComponent<TerrainDisplayer>();
        }
        terrain.Setup();
    }




    // Update is called once per frame
    void Update ()
	{

	}
}
