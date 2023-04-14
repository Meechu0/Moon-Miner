using System.Collections.Generic;
using UnityEngine;

namespace Minigame
{
    public class GreenZoneMiniGame : MonoBehaviour
    {
        [Header("Arrow Setup")]
        [SerializeField] private RectTransform arrowTransform;
        [SerializeField] private float smoothTime;
        [SerializeField] private float maxSpeed;

        private RectTransform _redBar;
        private float _redRange;
        private float _arrowPosY;
        private float _currentVel;
        private bool _stop;

        private void Awake()
        {
            _redBar = GetComponent<RectTransform>();
            _redRange = _redBar.rect.size.y / 2f;

            GenerateGreenZones();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space)) _stop = true;
        
            if(!_stop) MoveArrow();
            if(_stop) Debug.Log(PassFail());
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
                    hasPassed = true;
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
            
            Destroy(gameObject, 2f);
        }
    }
}
