using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ReturnPointInteraction : MonoBehaviour
{
    public GameObject textPrefab;
    public float textFadeDuration;
    private GameObject currentTextPrefab;
    public GameObject[] satellites;
    private bool allSatellitesPowered;
    public GameObject playerPosition; 
    private bool isTextFading; 

    void Start()
    {
        for (int i = 0; i < satellites.Length; i++)
        {
            satellites[i].GetComponent<SateliteInteraction>().isPowered = false;
        }
    }

    public bool CheckSatellitesPower()
    {
        for (int i = 0; i < satellites.Length; i++)
        {
            if (!satellites[i].GetComponent<SateliteInteraction>().isPowered)
            {
                return false;
            }
        }
        return true;
    }

    public BaseInteraction _BaseInteractionScript;
    public void Interact()
    {
        allSatellitesPowered = CheckSatellitesPower();

        if (allSatellitesPowered)
        {
            Debug.Log("Satellites powered, returning to Earth");
            _BaseInteractionScript.FinishGame();
        }
        else
        {
            if (currentTextPrefab != null && isTextFading)
            {
                return;
            }

            if (currentTextPrefab != null)
            {
                StopCoroutine(FadeOutText());
                Destroy(currentTextPrefab);
            }

            if (textPrefab != null && playerPosition != null)
            {
                Vector3 positionAbovePlayer = playerPosition.transform.position;
                positionAbovePlayer.y = positionAbovePlayer.y + 2.0f; 
                Quaternion playerRotation = Quaternion.Euler(0.0f, Camera.main.transform.rotation.eulerAngles.y, 0.0f);
                currentTextPrefab = Instantiate(textPrefab, positionAbovePlayer, playerRotation);
            }

            if (currentTextPrefab != null)
            {
                TextMeshPro textMesh = currentTextPrefab.GetComponent<TextMeshPro>();
                textMesh.text = " Connect all satelites!";
                textMesh.alpha = 1.0f;

                StartCoroutine(FadeOutText());
            }
            
        }
    }

    IEnumerator FadeOutText()
    {
        if (currentTextPrefab != null)
        {
            isTextFading = true; // Set the flag to true indicating text is fading
            TextMeshPro textMesh = currentTextPrefab.GetComponent<TextMeshPro>();
            float alpha = 1.0f;

            // Gradually decrease the alpha value to fade out the text
            while (alpha > 0.0f)
            {
                alpha -= Time.deltaTime / textFadeDuration;
                textMesh.alpha = alpha;
                yield return null;
            }

            // Set the alpha value to 0 to ensure it's fully transparent
            textMesh.alpha = 0.0f;

            // Wait for the next frame to ensure the text is fully transparent before destroying the object
            yield return new WaitForEndOfFrame();

            if (currentTextPrefab != null)
            {
                Destroy(currentTextPrefab);
                currentTextPrefab = null;

            }
        }
    }
}