using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnPointInteraction : MonoBehaviour
{
    public GameObject[] satelites;
    private bool allSatelitesPowered;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < satelites.Length; i++)
        {
            satelites[i].GetComponent<SateliteInteraction>().isPowered = false;
        }
    }

    public bool checkSatelitesPower()
    {
        for(int i = 0; i < satelites.Length; i++)
        {
            if (!satelites[i].GetComponent<SateliteInteraction>().isPowered)
            {
                return false;
            }
        }
        return true;
    }

    public void Interact()
    {
        allSatelitesPowered = checkSatelitesPower();

        if (allSatelitesPowered)
        {
            Debug.Log("Satelites powered, returning to earth");
        }else
        {
            Debug.Log("not all satelites powered, keep exploring");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
