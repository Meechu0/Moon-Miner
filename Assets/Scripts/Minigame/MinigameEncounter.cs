using UnityEngine;

namespace Minigame
{
    public class MinigameEncounter : MonoBehaviour
    {
        [SerializeField] private Transform canvas;
        [SerializeField] private GameObject minigamePrefab;

        private bool _encounterStarted;

        private void OnTriggerEnter(Collider other)
        {
            if (_encounterStarted) return;
            _encounterStarted = true;
           Instantiate(minigamePrefab, canvas);
        }
    }
}