using UnityEngine;

public class CarTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("Trigger") && name == "TriggerFront")
        {
            //FindObjectOfType<CarCollisionManager>().Bump(GetComponentInParent<CarMovement>().CurrentDirection, other.GetComponentInParent<CarMovement>());
            //GetComponentInParent<CarMovement>().CurrentDirection = Vector3.back * 0.5f;
            Debug.Log("" + name + " hit " + other.name);
        }
    }
}
