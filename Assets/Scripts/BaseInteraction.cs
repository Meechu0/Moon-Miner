using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseInteraction : MonoBehaviour
{
    public GameObject uiPanel; 
    private bool isCollidingWithBase; 
    // Update is called once per frame
    void Update()
    {
        // press tab to open base interaction menu when coliding with base
        if (isCollidingWithBase && Input.GetKeyDown(KeyCode.Tab))
        {
            if (uiPanel.activeSelf)
            {
                uiPanel.SetActive(false); 
            }else
            {
                uiPanel.SetActive(true); 
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
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
}