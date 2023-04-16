using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpMenu : MonoBehaviour
{
    public GameObject helpPanel; // Reference to the UI panel GameObject

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.H))
        {
            if (helpPanel.activeSelf)
            {
                helpPanel.SetActive(false);
            }
            else
            {
                helpPanel.SetActive(true);
            }
        }
    }

    public void TurnOffHelpPanel()
    {
        helpPanel.SetActive(false);
    }
    public void TurnOOnHelpPanel()
    {
        helpPanel.SetActive(true);
    }
}