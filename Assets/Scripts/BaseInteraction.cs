using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BaseInteraction : MonoBehaviour
{
    public PlayerResources _PlayerResources;
    public RoverMovement _RoverMovementScript;
    public GameObject uiPanel;
    public GameObject tabPanel;
    public GameObject shopPanel;
    public int gold;
    public int resources;
    private bool isCollidingWithBase; 
    // Update is called once per frame
    void Update()
    {
        if(isCollidingWithBase && !uiPanel.activeSelf && !shopPanel.activeSelf)
        {
            tabPanel.SetActive(true);
            updateText();
        }
        else
        {
            tabPanel.SetActive(false);
        }
        // press tab to open base interaction menu when coliding with base
        if (isCollidingWithBase && Input.GetKeyDown(KeyCode.Tab))
        {
            if (uiPanel.activeSelf)
            {
                uiPanel.SetActive(false); 
            }else
            {
                uiPanel.SetActive(true);
                updateText();
                tabPanel.SetActive(false);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        TransferResourcesToBase();
        if (other.CompareTag("Base"))
        {
            Debug.Log("entered base");
            isCollidingWithBase = true;
            _RoverMovementScript.numberOfBatteries = _RoverMovementScript.maxNumberOfBatteries;
            _RoverMovementScript.batteryCharge = 10f;


        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Base"))
        {
            print("left base");
            isCollidingWithBase = false;
            _RoverMovementScript.numberOfBatteries = _RoverMovementScript.maxNumberOfBatteries;
            _RoverMovementScript.batteryCharge = 10f;

            if (uiPanel.activeSelf)
            {
                uiPanel.SetActive(false);
            }
            if (shopPanel.activeSelf)
            {
                shopPanel.SetActive(false);
            }


        }
    }

    public TextMeshProUGUI goldText;
    public TextMeshProUGUI resourcesText;
    public void TransferResourcesToBase()
    {
        gold += _PlayerResources.gold;
        resources += _PlayerResources.resource;
        updateText();
        _PlayerResources.updateValuebles();
    }

    public void updateText()
    {
        resourcesText.text = "Resources: " + resources.ToString();
        goldText.text = "Gold: " + gold.ToString();
    }

    public void openShop()
    {
        shopPanel.SetActive(true);
        if (uiPanel.activeSelf)
        {
            uiPanel.SetActive(false);
        }

    }

}