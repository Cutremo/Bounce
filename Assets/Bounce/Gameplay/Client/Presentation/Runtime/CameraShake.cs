using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [Header("Default Shake")]
    [SerializeField] float duration;
    [SerializeField] float strength;
    [SerializeField] int vibrato;
    [Header("Drift Shake")]
    [SerializeField] float timeIncrementMultiplier;
    [SerializeField] float initialIncrementalStrength;
    [SerializeField] float maxIncrementalStrength;

    [SerializeField] GameObject test;
    private float currentIncrementalStrength;   
    bool incrementalShake;
    private float elapsedIncrementalTime;

    float decreaseFactor;

    public void Shake(float multiplier = 1f)
    {
        StopIncrementalShake();
        transform.DOKill(true);
        transform.DOShakePosition(duration, strength * multiplier, vibrato).SetUpdate(true);
    }

    public void StartIncrementalShake()
    {
        if(incrementalShake)
            return;
        
        transform.DOKill(true);
        incrementalShake = true;
        currentIncrementalStrength = initialIncrementalStrength;
        // FindObjectOfType<ControllerRumble>().SetControllerVibration();
    }

    public void StopIncrementalShake()
    {
        incrementalShake = false;
        // FindObjectOfType<ControllerRumble>().CutControllerVibration();
    }

    void Update()
    {
        ManualIncrementalShake();
    }

    private void ManualIncrementalShake()
    {
        if (incrementalShake)
        {
            currentIncrementalStrength = Mathf.Min(currentIncrementalStrength += elapsedIncrementalTime* (strength/200), maxIncrementalStrength);
            transform.localPosition = Random.insideUnitSphere * currentIncrementalStrength;
            elapsedIncrementalTime = Mathf.Min(elapsedIncrementalTime + Time.deltaTime*timeIncrementMultiplier, 3);
        }
        else
        {
            transform.localPosition = Vector3.zero;
            elapsedIncrementalTime = 0;
            currentIncrementalStrength = 0;
        }
    }
}
