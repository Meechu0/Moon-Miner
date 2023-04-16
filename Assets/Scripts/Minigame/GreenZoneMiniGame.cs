using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Minigame
{
    public class GreenZoneMiniGame : MonoBehaviour
    {
        public GameObject encounter;
        [SerializeField] private int goldValueMin;
        [SerializeField] private int goldValueMax;
        [SerializeField] private int resourceValueMin;
        [SerializeField] private int resourseValueMax;
        
        [Header("Game End Text")] 
        [SerializeField] private GameObject successText;
        [SerializeField] private GameObject failText;
        
        [Header("Arrow Setup")]
        [SerializeField] private RectTransform arrowTransform;
        [SerializeField] private float smoothTime;
        [SerializeField] private float maxSpeed;

        private PlayerResources _playerResources;
        private RectTransform _redBar;
        private float _redRange;
        private float _arrowPosY;
        private float _currentVel;
        private bool _stop;
        private bool _gameFinished;
        private RoverMovement _roverMovement;

        private void Awake()
        {
            _roverMovement = FindObjectOfType<RoverMovement>();
            _playerResources = FindObjectOfType<PlayerResources>();
            _redBar = GetComponent<RectTransform>();
            _redRange = _redBar.rect.size.y / 2f;
        }

        private void Start()
        {
            GenerateGreenZones();
        }

        private void Update()
        {
            
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) _stop = true;

            if (!_stop)
            {
                MoveArrow();
                _roverMovement.enabled = false;
            }
            else
            {
                _roverMovement.enabled = true;
            }
            
            if (_stop && !_gameFinished)
            {
                _gameFinished = true;
                var pass = PassFail();
                if(pass) Instantiate(successText, transform);
                else Instantiate(failText, transform);
                
                Destroy(encounter);
                Destroy(gameObject, 2f);
            }
        }

        private void MoveArrow()
        {
            if ((_arrowPosY >= _redRange - 0.01 && _redRange > 0) || (_arrowPosY <= _redRange + 0.01 && _redRange < 0))
                _redRange = -_redRange;

            _arrowPosY = Mathf.SmoothDamp(_arrowPosY, _redRange, ref _currentVel, smoothTime, maxSpeed, Time.deltaTime);
            arrowTransform.localPosition = new Vector2(42.25f, _arrowPosY);
        }

        private bool PassFail()
        {
            var hasPassed = false;

            foreach (var greenZone in _greenZones)
            {
                if (_arrowPosY < greenZone.localPosition.y + greenZone.rect.size.y / 2 &&
                    _arrowPosY > greenZone.localPosition.y - greenZone.rect.size.y / 2)
                {
                    hasPassed = true;
                    _playerResources.AddGold(Random.Range(goldValueMin, goldValueMax));
                    _playerResources.AddResource(Random.Range(resourceValueMin, resourseValueMax));
                    break;
                }

            }
            
            return hasPassed;
        }
    
        [Header("Green Zone Setup")]
        [SerializeField] private GameObject greenZonePrefab;
        [SerializeField] private int minZones;
        [SerializeField] private int maxZones;
        [SerializeField] private int minSize;
        [SerializeField] private int maxSize;
    
        private List<RectTransform> _greenZones;

        private void GenerateGreenZones()
        {
            _greenZones = new List<RectTransform>();
        
            var numOfZones = Random.Range(minZones, maxZones + 1);

            for (int i = 0; i < numOfZones; i++)
            {
                var zone = Instantiate(greenZonePrefab, transform.position, Quaternion.identity, transform);
                var zoneRect = zone.GetComponent<RectTransform>();
                zoneRect.sizeDelta = new Vector2(50, Random.Range(minSize, maxSize));
                zoneRect.localPosition = new Vector2(0, Random.Range(-_redRange + maxSize, _redRange - maxSize));
                _greenZones.Add(zoneRect);
            }
        }
    }
}
