using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerPlacer : MonoBehaviour
{
    [SerializeField] private Transform mainBase;
    [SerializeField] private float chargerMaxLength;
    [SerializeField] private GameObject chargerPrefab;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Material inRangeMat;
    [SerializeField] private Material outOfRangeMat;

    private Material _defaultMat;
    private bool _placeCharger;
    private Camera _camera;
    private GameObject _placeholderCharger;

    public List<Transform> _chargers = new List<Transform>();

    private void Awake()
    {
        _chargers.Add(mainBase);
        _defaultMat = chargerPrefab.GetComponent<Renderer>().sharedMaterial;
        _camera = Camera.main;
    }

    private void Update()
    {
        float dist = Mathf.Infinity;
        Transform closestCharger = null;
        
        if (Input.GetMouseButtonDown(1))
        {
            _placeCharger = !_placeCharger;
        }
        
        if (_placeCharger)
        {
            if (_placeholderCharger == null)
            {
                _placeholderCharger = Instantiate(chargerPrefab);
            }
            else
            {
                foreach (var charger in _chargers)
                {
                    var distToCharger = Vector3.Distance(_placeholderCharger.transform.position, charger.transform.position);

                    if (distToCharger < dist && distToCharger < chargerMaxLength)
                    {
                        closestCharger = charger;
                    }
                }
                if (closestCharger != null)
                {
                    _placeholderCharger.GetComponent<Renderer>().sharedMaterial = inRangeMat;
                    _placeholderCharger.GetComponentInChildren<LineRenderer>().SetPosition(0, _placeholderCharger.transform.GetChild(0).transform.position);
                    _placeholderCharger.GetComponentInChildren<LineRenderer>().SetPosition(1, closestCharger.GetChild(0).transform.position);
                }
                else
                {
                    closestCharger = null;
                    _placeholderCharger.GetComponent<Renderer>().sharedMaterial = outOfRangeMat;
                    _placeholderCharger.GetComponentInChildren<LineRenderer>().SetPosition(0, Vector3.zero);
                    _placeholderCharger.GetComponentInChildren<LineRenderer>().SetPosition(1, Vector3.zero);
                }
                
                
                
                
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundMask);

                _placeholderCharger.transform.position = hit.point;

                if (Input.GetMouseButtonDown(0) && closestCharger != null)
                {
                    _placeholderCharger.GetComponent<Charger>().connected = true;
                    _placeholderCharger.GetComponent<Renderer>().sharedMaterial = _defaultMat;
                    _chargers.Add(_placeholderCharger.transform);
                    _placeholderCharger = null;
                    _placeCharger = false;
                }
            }
        }

        if (!_placeCharger && _placeholderCharger != null)
        {
            Destroy(_placeholderCharger);
        }
    }
}
