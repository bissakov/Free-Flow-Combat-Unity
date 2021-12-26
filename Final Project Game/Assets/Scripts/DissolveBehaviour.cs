using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveBehaviour : MonoBehaviour
{
    [SerializeField] GameObject _mainCharacter;
    [SerializeField] Material _dissolveMat;
    [SerializeField] float _duration;
    float currentTimeDuration;

    private void Awake()
    {
    }

    private void Start()
    {
        StartCoroutine(Dissolve());
    }

    // private void Update()
    // {
    //     currentTimeDuration += (Time.deltaTime * 0.1f);
    //     _dissolveMat.SetFloat("_TimeValue", currentTimeDuration);
    // }

    IEnumerator Dissolve()
    {
        
        yield return new WaitForSeconds(_duration);
        _mainCharacter.SetActive(true);
        gameObject.SetActive(false);
    }
}
