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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            int amount = Random.Range(1, 11);
            AddGold(amount);
            Debug.Log("earned" + amount + "Gold");
        }
    }
    //add gold function 
    public void AddGold(int amount)
    {
        gold += amount;
        GoldText.text = "gold: " +gold.ToString();
        // spawn text object above players head
        if (goldTextPrefab != null)
        {
            // text position, text rotation
            Vector3 textPosition = transform.position + Vector3.up * 2f; 
            Quaternion textRotation = Quaternion.identity; 
            // spawn text object
            TextMeshPro text = Instantiate(goldTextPrefab, textPosition, textRotation);
            // replace text with amount recieved
            text.text = "+" + amount + " Gold"; 
            // start fade anim
            StartCoroutine(MoveAndFadeText(text));
        }
    }

    // add resource function
    public void AddResource(int amount)
    {
        resource += amount;
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
}