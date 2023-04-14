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
        }

        private void OnTriggerExit(Collider other)
        {
            _encounterStarted = false;
            Destroy(_minigameInstance);
        }
    }
}