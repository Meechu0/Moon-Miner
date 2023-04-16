using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;

public class BaseInteraction : MonoBehaviour
{
    public ReturnPointInteraction _ReturnPointInteraction;
    public PlayerResources _PlayerResources;
    public RoverMovement _RoverMovementScript;
    public GameObject uiPanel;
    public GameObject tabPanel;
    public GameObject shopPanel;
    public GameObject rocketPanel;
    [FormerlySerializedAs("gold")] public int copper;
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


    public Image[] satelliteImages;
    public SateliteInteraction[] satelliteScripts;

    void UpdateSatelliteImage(SateliteInteraction sateliteScript, Image sateliteImage)
    {
        if (sateliteScript.isPowered)
        {
            // Set the image color to green if the satellite is powered
            sateliteImage.color = Color.green;
        }
        else
        {
            // Set the image color to red if the satellite is not powered
            sateliteImage.color = Color.red;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        // Update satellite images based on their corresponding scripts
        for (int i = 0; i < satelliteScripts.Length; i++)
        {
            UpdateSatelliteImage(satelliteScripts[i], satelliteImages[i]);
        }

        if (other.CompareTag("Base"))
        {
            TransferResourcesToBase();
            Debug.Log("entered base");
            isCollidingWithBase = true;
            _RoverMovementScript.numberOfBatteries = _RoverMovementScript.maxNumberOfBatteries;
            _RoverMovementScript.batteryCharge = 10f;
        }
        if (other.CompareTag("Rocket"))
        {
                
            rocketPanel.SetActive(true);
            Debug.Log("InRocketRange");
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

        if (other.CompareTag("Rocket"))
        {
            rocketPanel.SetActive(false);
        }
    }



    public TextMeshProUGUI goldText;
    public TextMeshProUGUI resourcesText;
    public void TransferResourcesToBase()
    {
        copper += _PlayerResources.gold;
        resources += _PlayerResources.resource;
        updateText();
        _PlayerResources.updateValuebles();
    }

    public void updateText()
    {
        resourcesText.text = "Resources: " + resources.ToString();
        goldText.text = "Gold: " + copper.ToString();
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