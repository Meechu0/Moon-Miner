using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MainMenuCameraController : MonoBehaviour
{
    public float duration;

    public void LookAt(Transform target)
    {
        transform.DOLookAt(target.position, duration);
    }
}
