using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayerPosition : MonoBehaviour
{
	// ================================================================================================ Public Variables
	public static Dictionary<string, GameObject> ActivePlayerCameras = new Dictionary<string, GameObject>();
	
	// ======================================================================================== SerializeField Variables
	
	// =============================================================================================== Private Variables
	private NetworkClient CLIENT;
	[SerializeField]private Transform _hub, _camera, _testhub, _testPrefab;
	
	// =========================================================================================================== Awake
	private void Awake()
	{
		CLIENT = GetComponent<NetworkClient>();
	}
	
    // =========================================================================================================== Start
    private void Start()
    {
	    StartCoroutine(CalculatePosition());
    }

    // ========================================================================================================== Update
    private void Update()
    {
	    
    }
    
    // ========================================================================================================== Update
    IEnumerator CalculatePosition()
    {
	    while (true)
	    {
		    Vector3 pos = _camera.localPosition;
		    Vector3 rot = _camera.rotation.eulerAngles;
		    CLIENT.SetPhone(pos, rot);
		    yield return new WaitForSeconds(0.2f);
	    }
    }
}
