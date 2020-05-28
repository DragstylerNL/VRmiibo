using UnityEngine;

public class FollowTarget : MonoBehaviour
{
	// ================================================================================================ Public Variables
	[Header("The Follow Target")]
	public Transform TARGET;
	[Header("Positions")] public bool followPosX;
	public bool followPosY, followPosZ;
	public Vector3 posOffset;
	[Header("Rotation")] public bool followRotX;
	public bool followRotY, followRotZ;
	public Vector3 rotOffset;

    // ========================================================================================================== Update
    private void Update()
    {
	    FollowPos();
	    FollowRot();
    }
    
    // ============================================================================================================= Pos
    private void FollowPos()
    {
	    Vector3 targetPosition = TARGET.position;
	    Vector3 pos = transform.position;
	    pos.x = followPosX ? targetPosition.x + posOffset.x : pos.x;
	    pos.y = followPosY ? targetPosition.y + posOffset.y : pos.y;
	    pos.z = followPosZ ? targetPosition.z + posOffset.z : pos.z;
	    transform.position = pos;
    }
    
    // ============================================================================================================= Rot
    private void FollowRot()
    {
	    Vector3 targetRot = TARGET.rotation.eulerAngles;
	    Vector3 rot = transform.rotation.eulerAngles;
	    rot.x = followRotX ? targetRot.x + rotOffset.x : rot.x;
	    rot.y = followRotY ? targetRot.y + rotOffset.y : rot.y;
	    rot.z = followRotZ ? targetRot.z + rotOffset.z : rot.z;
	    transform.rotation = Quaternion.Euler(rot);
    }
    
    // ====================================================================================================== SetAllTrue
    public void SetAllTrue()
    {
	    followPosX = followPosY = followPosZ = followRotX = followRotY = followRotZ = true;
    }
    
}
