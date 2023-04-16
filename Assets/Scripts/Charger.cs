using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charger : MonoBehaviour
{
    public bool connected;
    [SerializeField] private bool chargeRange;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Satelite") && connected)
        {
            Debug.Log(other.name);
            other.GetComponent<SateliteInteraction>().Interact();
        }

        if (other.CompareTag("Player"))
        {
            other.GetComponent<RoverMovement>().FillBattery();
        }
    }
}
