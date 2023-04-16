using System;
using UnityEngine;

namespace Minigame
{
    public class MinigameEncounter : MonoBehaviour
    {
        [SerializeField] private Transform canvas;
        [SerializeField] private GameObject minigamePrefab;

        private GameObject _minigameInstance;
        private bool _encounterStarted;

        private void OnTriggerEnter(Collider other)
        {
            if (_encounterStarted) return;
            _encounterStarted = true;
           _minigameInstance = Instantiate(minigamePrefab, canvas);
           if (_minigameInstance.GetComponent<GreenZoneMiniGame>() != null)
           {
               _minigameInstance.GetComponent<GreenZoneMiniGame>().encounter = gameObject;
           }
           else if (_minigameInstance.GetComponent<InputMinigame>() != null)
           {
               _minigameInstance.GetComponent<InputMinigame>().encounter = gameObject;
           }
        }

        private void OnTriggerExit(Collider other)
        {
            _encounterStarted = false;
            Destroy(_minigameInstance);
        }
    }
}