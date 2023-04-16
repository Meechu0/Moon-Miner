using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    public int maxNumberOfBatteries;
    public int numberOfBatteries;
    public TextMeshProUGUI BatteryCountText;

    [SerializeField]
    public float batteryCharge; // battery charge in seconds
    [SerializeField]
    private bool isMoving; // bool for movement

    public float horizontalInput;
    public float verticalInput;

    //satelite
    private SateliteInteraction _sateliteScript;
    //return interaction point
    public ReturnPointInteraction _returnPoint;
    private void Start()
    {
        numberOfBatteries = maxNumberOfBatteries;
        BatteryCountText.text = numberOfBatteries.ToString();
    }

    [SerializeField]
    private Image batteryFillImage; // battery charge ui image
    void Update()
    {
        
        //refill battery (for testing)
        if (Input.GetKeyDown(KeyCode.Q))
        {
            FillBattery();
            numberOfBatteries += 1;
            BatteryCountText.text = numberOfBatteries.ToString();
        }

        if (batteryCharge > 0)
        {
            processInputs();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if(_sateliteScript != null && Vector3.Distance(transform.position, _sateliteScript.transform.position) < 15f)
            {
                _sateliteScript.Interact();
                Debug.Log("interacted with satelite");
            }
            else
            {
                //Debug.Log("interaction failed");
            }
            if (_returnPoint != null && Vector3.Distance(transform.position, _returnPoint.transform.position) < 15f)
            {
                _returnPoint.Interact();
               // Debug.Log("interacted with return point");
            }
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

            if (numberOfBatteries > 0)
            {
                if (batteryCharge <= 0f)
                {

                    batteryCharge = 10f;
                    numberOfBatteries -= 1;
                    BatteryCountText.text = numberOfBatteries.ToString();

                }
            }
            else
            {
                batteryCharge = 0f;
                isMoving = false; // stop moving when battery is used
                moveDistance = 0f;
            }


            transform.Translate(Vector3.forward * moveDistance);
            transform.Rotate(Vector3.up * rotationAngle);

            //update ui image
            UpdateBatteryFill();
        }
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Satelite"))
        {
            _sateliteScript = other.GetComponent<SateliteInteraction>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Satelite"))
        {
            _sateliteScript = null;
        }
    }

}