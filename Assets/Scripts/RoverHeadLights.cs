using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoverHeadLights : MonoBehaviour
{
    public Light headlight; 
    
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.L))
        {
            headlight.enabled = !headlight.enabled;
        }
    }
}