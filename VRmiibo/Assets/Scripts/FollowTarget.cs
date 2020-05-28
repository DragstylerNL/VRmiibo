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

    // ========================================================================================================== Update
    private void Update()
    {
	    Vector3 targetPosition = TARGET.position;
	    Vector3 pos = transform.position;
	    pos.x = followPosX ? targetPosition.x + posOffset.x : pos.x;
	    pos.y = followPosX ? targetPosition.y + posOffset.y : pos.y;
	    pos.z = followPosX ? targetPosition.z + posOffset.z : pos.z;
	    transform.position = pos;
    }
}
