using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SateliteInteraction : MonoBehaviour
{
    public bool isPowered;

    public Light lightOne; 
    public Light lightTwo;

    public float minIntensity = 0.5f; 
    public float maxIntensity = 1.5f; 
    public float flickerSpeed = 1.0f; 
    private float timer; 

    public Color lightColorGreen;
    public Color lightColorRed;

    private void Start()
    {
        lightOne.color = lightColorRed;
        lightTwo.color = lightColorRed;
    }

    public void Interact()
    {
        if (!isPowered)
        {
            isPowered = true;
            lightOne.color = lightColorGreen;
            lightTwo.color = lightColorGreen;
            Debug.Log("Satelite power restored!");
        }
    }

    void Update()
    {
        timer += Time.deltaTime * flickerSpeed;
        float flickerIntensity = Mathf.Lerp(minIntensity, maxIntensity, Mathf.Sin(timer));
        lightOne.intensity = flickerIntensity;
        lightTwo.intensity = flickerIntensity;
    }
}
