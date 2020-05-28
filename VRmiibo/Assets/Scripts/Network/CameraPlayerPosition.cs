using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayerPosition : MonoBehaviour
{
	// ================================================================================================ Public Variables
	public static Dictionary<string, GameObject> ActivePlayerCameras = new Dictionary<string, GameObject>();
	public static bool start = false;

	// ======================================================================================== SerializeField Variables
	
	// =============================================================================================== Private Variables
	private NetworkClient CLIENT;
	private Transform _camera;
	private Transform _hub;
	public void SetHub(Transform tr)
	{
		_hub = tr;
	}
	
	// =========================================================================================================== Awake
	private void Awake()
	{
		CLIENT = GetComponent<NetworkClient>();
	}
	
    // =========================================================================================================== Start
    private void Update()
    {
	    if (start)
	    {
		    start = false;
		    _camera = Camera.main.transform;
		    StartCoroutine(CalculatePosition());
	    }
    }
    // ========================================================================================================== Update
    IEnumerator CalculatePosition()
    {
	    while (true)
	    {
		    Vector3 pos = _hub.position - _camera.position;
		    Vector3 rot = _camera.rotation.eulerAngles;
		    CLIENT.SetCamera(pos, rot);
		    yield return new WaitForSeconds(0.2f);
	    }
    }
}
