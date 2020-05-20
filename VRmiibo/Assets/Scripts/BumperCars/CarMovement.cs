using UnityEngine;

public class CarMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 0.1f;
    [SerializeField] private float rotationSpeed = 2f;
    [SerializeField] private float boostSpeed = 0.5f;
    private bool boosting = false;

    void Update()
    {
        if (boosting){
            transform.position += transform.forward * boostSpeed; 
        }
        else
        {
            if (Input.GetKey(KeyCode.A))
            {
                TurnLeft();
            }
            if (Input.GetKey(KeyCode.D))
            {
                TurnRight();
            }

            if (Input.GetKey(KeyCode.B))
            {
                SetBoost(true);
            }
            transform.position += transform.forward * moveSpeed;
        }

    }

    private void TurnLeft()
    {
        transform.Rotate(Vector3.down * rotationSpeed);
    }
    
    private void TurnRight()
    {
        transform.Rotate(Vector3.up * rotationSpeed);
    }

    private void SetBoost(bool activate)
    {
        boosting = activate;
    }
}
