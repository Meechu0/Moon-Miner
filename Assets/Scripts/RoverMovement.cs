using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoverMovement : MonoBehaviour
{
    [SerializeField] 
    private LayerMask ground;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float boostedMoveSpeed; // use shift for speed boost
    [SerializeField]
    private float turnSpeed;

    [SerializeField]
    private float batteryCharge; // battery charge in seconds
    [SerializeField]
    private bool isMoving; // bool for movement

    public float horizontalInput;
    public float verticalInput;

    [SerializeField]
    private Image batteryFillImage; // battery charge ui image
    void Update()
    {
        Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, Mathf.Infinity, ground);
        transform.up = hit.normal;
        
        //refill battery (for testing)
        if (Input.GetKeyDown(KeyCode.Q))
        {
            FillBattery();
        }

        if (batteryCharge > 0)
        {
            processInputs();
        }
    }

    private void processInputs()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (verticalInput > 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        bool isBoosting = Input.GetKey(KeyCode.LeftShift);

        float moveDistance;
        float rotationAngle = turnSpeed * Time.deltaTime * horizontalInput;

        if (isMoving) // check if player is moving
        {
            if (isBoosting)
            {
                moveDistance = boostedMoveSpeed * Time.deltaTime * verticalInput;
            }
            else
            {
                moveDistance = moveSpeed * Time.deltaTime * verticalInput;
            }

            // decrease battery when moving
            batteryCharge -= Time.deltaTime;
            if (batteryCharge <= 0f)
            {
                batteryCharge = 0f;
                isMoving = false; // stop moving when battery is used
            }
        }
        else
        {
            moveDistance = 0f;
        }

        transform.Translate(Vector3.forward * moveDistance);
        transform.Rotate(Vector3.up * rotationAngle);

        //update ui image
        UpdateBatteryFill();

    }

    private void UpdateBatteryFill()
    {
        // update battery image based on charge
        if (batteryFillImage != null)
        {
            float fillAmount = batteryCharge / 10f; 
            batteryFillImage.fillAmount = fillAmount;
        }
    }

    private void FillBattery()
    {
        batteryCharge = 10f;
        isMoving = true;
    }
}