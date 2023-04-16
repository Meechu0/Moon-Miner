using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using TMPro;
using UnityEngine.UI;

namespace Minigame
{
    public class InputMinigame : MonoBehaviour
    {
        [SerializeField] private int minGold;
        [SerializeField] private int maxGold;
        [SerializeField] private int minResources;
        [SerializeField] private int maxResources;
        [SerializeField] private float timeLimit;
        [SerializeField] private Image timer;
        [SerializeField] private GameObject inputPrefab;
        [SerializeField] private Transform layoutGroup;
        [SerializeField] private GameObject passText;
        [SerializeField] private GameObject failText;

        [SerializeField] private int minInputs;
        [SerializeField] private int maxInputs;
        private const string InputOptions = "abcdfghijkmnoprstuvwxyz";

        private List<GameObject> _inputs;
        private float _remainingTimeLimit;
        private PlayerResources _playerResources;
        private bool gameFinished;
        private RoverMovement _roverMovement;

        private void Awake()
        {
            _roverMovement = FindObjectOfType<RoverMovement>();
            _playerResources = FindObjectOfType<PlayerResources>();
            _remainingTimeLimit = timeLimit;
        }

        private void Start()
        {
            InitializeMinigame();
        }

        private void Update()
        {
            if (gameFinished) return;
            if (_inputs.Count == 0)
            {
                gameFinished = true;
                _playerResources.AddGold(Random.Range(minGold, maxGold));
                _playerResources.AddResource(Random.Range(minResources, maxResources));
                passText.SetActive(true);
                Destroy(gameObject, 2f);
                _roverMovement.enabled = true;
                return;
            }
            if (_remainingTimeLimit <= 0)
            {
                failText.SetActive(true);
                Destroy(gameObject, 2f);
                _roverMovement.enabled = true;
                return;
            }
            
            RunTimer();
            
            if (ProcessInput() == _inputs[0].GetComponentInChildren<TMP_Text>().text)
            {
                var input = _inputs[0];
                _inputs.RemoveAt(0);
                Destroy(input);
            }
        }

        private void RunTimer()
        {
            _roverMovement.enabled = false; 
            _remainingTimeLimit -= Time.deltaTime;
            timer.fillAmount = _remainingTimeLimit / timeLimit;
        }
        
        private void InitializeMinigame()
        {
            _inputs = new List<GameObject>();
            
            var randNumOfInputs = Random.Range(minInputs, maxInputs);
            
            for (int i = 0; i < randNumOfInputs; i++)
            {
                var input = Instantiate(inputPrefab, layoutGroup);
                input.GetComponentInChildren<TMP_Text>().text = InputOptions[Random.Range(0, InputOptions.Length - 1)].ToString();
                _inputs.Add(input);
            }
            _inputs[0].GetComponent<RawImage>().color = Color.green;
        }

        private string ProcessInput()
        {
            // maybe parse char to keycode?
            if (Input.GetKeyDown(KeyCode.A))
                return "a";
            if (Input.GetKeyDown(KeyCode.B))
                return "b";
            if (Input.GetKeyDown(KeyCode.C))
                return "c";
            if (Input.GetKeyDown(KeyCode.D))
                return "d";
            if (Input.GetKeyDown(KeyCode.E))
                return "e";
            if (Input.GetKeyDown(KeyCode.F))
                return "f";
            if (Input.GetKeyDown(KeyCode.G))
                return "g";
            if (Input.GetKeyDown(KeyCode.H))
                return "h";
            if (Input.GetKeyDown(KeyCode.I))
                return "i";
            if (Input.GetKeyDown(KeyCode.J))
                return "j";
            if (Input.GetKeyDown(KeyCode.K))
                return "k";
            if (Input.GetKeyDown(KeyCode.L))
                return "l";
            if (Input.GetKeyDown(KeyCode.M))
                return "m";
            if (Input.GetKeyDown(KeyCode.N))
                return "n";
            if (Input.GetKeyDown(KeyCode.O))
                return "o";
            if (Input.GetKeyDown(KeyCode.P))
                return "p";
            if (Input.GetKeyDown(KeyCode.Q))
                return "q";
            if (Input.GetKeyDown(KeyCode.R))
                return "r";
            if (Input.GetKeyDown(KeyCode.S))
                return "s";
            if (Input.GetKeyDown(KeyCode.T))
                return "t";
            if (Input.GetKeyDown(KeyCode.U))
                return "u";
            if (Input.GetKeyDown(KeyCode.V))
                return "v";
            if (Input.GetKeyDown(KeyCode.W))
                return "w";
            if (Input.GetKeyDown(KeyCode.X))
                return "x";
            if (Input.GetKeyDown(KeyCode.Y))
                return "y";
            if (Input.GetKeyDown(KeyCode.Z))
                return "z";

            return null;
        }
    }
}
