using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessController : MonoBehaviour
{
    private PostProcessVolume _postProcessVolume;
    private ColorGrading _colorGrading;
    // Start is called before the first frame update
    void Start()
    {
        _postProcessVolume = GetComponent<PostProcessVolume>();
        if(_postProcessVolume == null)
        {
            Debug.LogError("Post process volumen not found.");
        } else
        {
            _postProcessVolume.profile.TryGetSettings(out _colorGrading);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateEffect(int score)
    {
        if(_colorGrading != null)
        {
            int min = -100;
            int max = 100;
            int range = max - min;
            _colorGrading.temperature.value = min + Math.Abs(((score + range) % (range * 2)) - range);
        }
    }
}
