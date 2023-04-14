using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BaseInteraction : MonoBehaviour
{
    public PlayerResources _PlayerResources;
    public GameObject uiPanel;
    public GameObject tabPanel;
    public int gold;
    public int resources;
    private bool isCollidingWithBase; 
    // Update is called once per frame
    void Update()
    {
        if(isCollidingWithBase && !uiPanel.activeSelf)
        {
            tabPanel.SetActive(true);
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
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Base"))
        {
            print("left base");
            isCollidingWithBase = false; 
        }
    }

    public TextMeshProUGUI goldText;
    public TextMeshProUGUI resourcesText;
    public void TransferResourcesToBase()
    {
        gold += _PlayerResources.gold;
        resources += _PlayerResources.resource;
        resourcesText.text = "Resources: " + resources.ToString();
        goldText.text = "Gold: " + gold.ToString();
        _PlayerResources.updateValuebles();
    }
}