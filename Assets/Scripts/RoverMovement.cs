using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoverMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float boostedMoveSpeed; // use shift for speed boost
    [SerializeField]
    private float turnSpeed;

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        bool isBoosting = Input.GetKey(KeyCode.LeftShift);

        float moveDistance;
        float rotationAngle = turnSpeed * Time.deltaTime * horizontalInput;
        if (isBoosting)
        {
            moveDistance = boostedMoveSpeed * Time.deltaTime * verticalInput;
        }
        else
        {
            moveDistance = moveSpeed * Time.deltaTime * verticalInput;
        }

        transform.Translate(Vector3.forward * moveDistance);
        transform.Rotate(Vector3.up * rotationAngle);
    }
}
