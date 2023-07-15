using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using DG.Tweening;

[RequireComponent(typeof(PostProcessVolume))]
public class PostProcessHandler : MonoBehaviour
{
    private PostProcessVolume volume;
    private Vignette vignette;
    private ColorGrading colorGrading;

    [SerializeField] private Material material;
    [SerializeField] private GameObject cube;
    private float maxVignetteIntensity = 0.5f;
    private float minVignetteIntensity = 0.1f;
    private float duration = 1.0f;

    private void Awake()
    {
        volume = GetComponent<PostProcessVolume>();
        // Try to get the Vignette effect from the profile and store it in the defined vignette variable
        volume.profile.TryGetSettings<Vignette>(out vignette);
        volume.profile.TryGetSettings<ColorGrading>(out colorGrading);  
    }

    private void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.Space))
        {
            cube.transform.DOScale(2.0f, 5.0f);
            material.DOColor(Color.red, 2.0f);
            DOTween.To(
                // getter
                () => vignette.intensity.value,
                // setter
                x => vignette.intensity.value = x,
                // targetValue
                maxVignetteIntensity,
                //duration
                duration);
            //StartCoroutine(ChangeVignette(maxVignetteIntensity, 0.20f));
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            DOTween.To(() => vignette.intensity.value, x => vignette.intensity.value = x, minVignetteIntensity, duration);
            //StartCoroutine(ChangeVignette(minVignetteIntensity, 0.20f));
        }
    }

    /*
    private IEnumerator ChangeVignette(float newValue, float duration)
    {
        float elapsedTime = 0;
        float originalValue = vignette.intensity.value;
        while(elapsedTime < duration)
        {
            vignette.intensity.value = Mathf.Lerp(originalValue, 
                newValue, elapsedTime/duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        vignette.intensity.value = newValue;
    }*/
}
