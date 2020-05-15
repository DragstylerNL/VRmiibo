using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class ARTapScaleAndSpawn : MonoBehaviour
{
    [SerializeField]private GameObject _placable;
    private GameObject _placedObject;
    private ARRaycastManager _raycastManager;
    private Pose _placementPose;
    private bool _placementPoseIsValid = false;
    private bool _placed = false;
    private ARRaycastHit _arRaycastHit;
    
    public GameObject placementIndicator;
    
    void Start()
    {
        _raycastManager = FindObjectOfType<ARRaycastManager>();
        Debug.Log(_raycastManager);
    }

    void Update()
    {
        UpdatePlacementPose();
        UpdatePlacementIndecator();

        if (_placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetKeyDown(KeyCode.A))
        {
            if (!_placed)
                PlaceObject();
           
        }
    }

    private void SwitchPlane()
    {
        _placedObject.transform.position = _placementPose.position;
        _placedObject.transform.rotation = _placementPose.rotation;
        RayIt();
    }

    private void PlaceObject()
    {
        _placedObject = Instantiate(_placable, _placementPose.position, _placementPose.rotation);
        _placed = true;
        RayIt();
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
            _arRaycastHit = hits[0];
            _placementPose = _arRaycastHit.pose;

            var cameraForward = Camera.main.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x,0, cameraForward.z).normalized;
            _placementPose.rotation = Quaternion.LookRotation(cameraBearing);
        }
    }

    private void RayIt()
    {
        Ray raycast = Camera.main.ScreenPointToRay(new Vector3(0.5f, 0.5f));
        //_placedObject.transform.localScale = Vector3.zero;
        if (Physics.Raycast(raycast, out RaycastHit raycastHit))
        {
            if(raycastHit.collider.gameObject.CompareTag("plane"))
            //_placedObject.transform.localScale = new Vector3(_placedObject.transform.localScale.x / raycastHit.collider.gameObject.transform.localScale.x,_placedObject.transform.localScale.y / raycastHit.collider.gameObject.transform.localScale.y,_placedObject.transform.localScale.z / raycastHit.collider.gameObject.transform.localScale.z);
            _placedObject.transform.localScale = raycastHit.collider.gameObject.transform.lossyScale;
        }
    }    
}
