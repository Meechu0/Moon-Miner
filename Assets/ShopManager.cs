using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public int chargerCost;
    
    public int BatteryUpgradesAvailable = 2;
    public int BatteryUpgradeCost;
    public RoverMovement _RoverMovementScript;
    public BaseInteraction _BaseInteractionScript;
    public TextMeshProUGUI batteryStockText;
    private ChargerPlacer chargerPlacer;

    private void Awake()
    {
        chargerPlacer = FindObjectOfType<ChargerPlacer>();
    }

    public void BatteryUpgrade()
    {
        if (_BaseInteractionScript.copper >= BatteryUpgradeCost && BatteryUpgradesAvailable > 0)
        {
            _BaseInteractionScript.copper -= BatteryUpgradeCost;
            _BaseInteractionScript.updateText();
            _RoverMovementScript.maxNumberOfBatteries += 1;
            BatteryUpgradesAvailable -= 1;
            Debug.Log("upgrade complete");
            batteryStockText.text = "Available: " + BatteryUpgradesAvailable;
            _RoverMovementScript.numberOfBatteries = _RoverMovementScript.maxNumberOfBatteries;
            _RoverMovementScript.BatteryCountText.text = _RoverMovementScript.numberOfBatteries.ToString();

        }
        else
        {
            Debug.Log("cant upgrade");
        }
    }

    public void BuyCharger()
    {
        if (_BaseInteractionScript.copper >= chargerCost)
        {
            chargerPlacer.BuyCharger();
            _BaseInteractionScript.copper -= BatteryUpgradeCost;
            _BaseInteractionScript.updateText();
        }
    }
}