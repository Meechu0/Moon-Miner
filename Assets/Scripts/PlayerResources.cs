using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerResources : MonoBehaviour
{
    public TextMeshProUGUI GoldText;
    public TextMeshProUGUI resourceText;
    public int gold;
    public int resource;
    public TextMeshPro goldTextPrefab;
    public TextMeshPro resourceTextPrefab;
    void Start()
    {

    }
    void Update()
    {

    }
    //add gold function 
    public void AddGold(int amount)
    {
        if (amount == 0) return;
        gold += amount;
        GoldText.text = "Copper: " + gold.ToString();
        // spawn text object above players head
        if (goldTextPrefab != null)
        {
            // text position, text rotation
            Vector3 textPosition = transform.position + Vector3.up * 2f; 
            Quaternion textRotation = Quaternion.identity; 
            // spawn text object
            TextMeshPro text = Instantiate(goldTextPrefab, textPosition, textRotation);
            // replace text with amount recieved
            text.text = "+" + amount + " Copper"; 
            // start fade anim
            StartCoroutine(MoveAndFadeText(text));
        }
    }

    // add resource function
    public void AddResource(int amount)
    {
        if (amount == 0) return;
        resource += amount;
        resourceText.text = "Lithium: " + resource.ToString();
        // spawn text object above players head
        if (goldTextPrefab != null)
        {
            // text position, text rotation
            Vector3 textPosition = transform.position + Vector3.up * 2f;
            Quaternion textRotation = Quaternion.identity;
            // spawn text object
            TextMeshPro text = Instantiate(resourceTextPrefab, textPosition, textRotation);
            // replace text with amount recieved
            text.text = "+" + amount + " Lithium";
            // start fade anim
            StartCoroutine(MoveAndFadeText(text));
        }
    }

    // courutine for text animation
    IEnumerator MoveAndFadeText(TextMeshPro text)
    {
        float elapsedTime = 0f;
        float duration = 2f; 
        float fadeDuration = 1f; 
        Vector3 startPosition = text.transform.position;
        // add motion 
        Vector3 endPosition = text.transform.position + Vector3.up * 2f; 
        Color startColor = text.color;
        // fade color by changing alpha color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0f); 
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float normalizedTime = elapsedTime / duration;
            // lerp position
            text.transform.position = Vector3.Lerp(startPosition, endPosition, normalizedTime);
            // lerp color
            text.color = Color.Lerp(startColor, endColor, elapsedTime / fadeDuration); 
            yield return null;
        }
        Destroy(text.gameObject);
    }

    public void updateValuebles()
    {
        gold = 0;
        resource = 0;
        resourceText.text = "Lithium: " + resource.ToString();
        GoldText.text = "Copper: " + gold.ToString();

    }
}