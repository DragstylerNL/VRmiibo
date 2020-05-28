using UnityEngine;

public class CarTrigger : MonoBehaviour
{
    [SerializeField] private float knockback;
    [SerializeField] private float knockbackBoost;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("Trigger") && name == "TriggerFront")
        {
            CarMovement movement = GetComponentInParent<CarMovement>();
            FindObjectOfType<CarCollisionManager>().Bump(movement.CurrentDirection, other.GetComponentInParent<CarMovement>());
            if (movement.Boosting)
            {
                movement.CurrentDirection = Vector3.zero;//new Vector3(0,0,-0.001f) * knockbackBoost;
                movement.tempStop = true;
                movement.stopTimer = Time.time + 3f;
            }
            else
            {
                movement.CurrentDirection = Vector3.back * knockback;
            }
            Debug.Log("" + name + " hit " + other.name);
        }
    }
}
