using UnityEngine;

public class CameraPlayerPosition : MonoBehaviour
{
	// ================================================================================================ Public Variables

	// ======================================================================================== SerializeField Variables
	
	// =============================================================================================== Private Variables
	private NetworkClient CLIENT;
	[SerializeField]private Transform _hub, _camera, _testhub, _testPrefab;
	
	// =========================================================================================================== Awake
	private void Awake()
	{
		//CLIENT = GameObject.FindWithTag("NETWORKCLIENT").GetComponent<NetworkClient>();
	}
	
    // =========================================================================================================== Start
    private void Start()
    {
        
    }

    // ========================================================================================================== Update
    private void Update()
    {
	    CalculatePosition();
    }
    
    // ========================================================================================================== Update
    private void CalculatePosition()
    {
	    Vector3 pos = _camera.localPosition;
	    Vector3 rotation = _camera.rotation.eulerAngles;

	    Vector3 mirrorHubRot = _testhub.rotation.eulerAngles;
	    _testPrefab.rotation = Quaternion.Euler(rotation + mirrorHubRot);
	    _testPrefab.localPosition = pos;
    }
}
