using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARTapToPlaceObject : MonoBehaviour
{
    public GameObject placementIndicator;
    public float speed;
    
    private ARRaycastManager _raycastManager;
    private NetworkClient _networkClient;
    private GameObject _player;
    private Pose _targetPose;
    private Pose _placementPose;
    private bool _placementPoseIsValid = false;
    void Start()
    {
        _raycastManager = FindObjectOfType<ARRaycastManager>();
        _networkClient = GameObject.FindWithTag("NETWORKCLIENT").GetComponent<NetworkClient>();
    }
    
    void Update()
    {
        if (_player == null)
            AddPlayer();
        
        UpdatePlacementPose();
        UpdatePlacementIndecator();
        float step =  speed * Time.deltaTime;

        if (_placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetKeyDown(KeyCode.A))
        {
                _targetPose = _placementPose;
        }
        
        _player.transform.position = Vector3.MoveTowards(_player.transform.position, _targetPose.position, step);
    }

    private void AddPlayer()
    {
        _player = PlayerCollection.GetPlayer(_networkClient.NETWORKID);
    }

    private void UpdatePlacementIndecator()
    {
        if (_placementPoseIsValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(_placementPose.position, _placementPose.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }

    private void UpdatePlacementPose()
    {
        Vector2 screenCenter = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        _raycastManager.Raycast(screenCenter, hits,TrackableType.Planes);
        _placementPoseIsValid = hits.Count > 0;
        if (_placementPoseIsValid)
        {
            _placementPose = hits[0].pose;

            var cameraForward = Camera.main.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x,0, cameraForward.z).normalized;
            _placementPose.rotation = Quaternion.LookRotation(cameraBearing);
        }
    }
}
