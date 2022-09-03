using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PosprocessingLerp : MonoBehaviour
{
    public PostProcessVolume Volume;
    private Bloom Bloom;
    [SerializeField]private float LerpSpeed = 5;
    [SerializeField]private float LerpValue;
    private void Awake() {
        Volume.profile.TryGetSettings(out Bloom);
    }
    private void Update() {
         Bloom.intensity.value = Mathf.Lerp(Bloom.intensity.value, LerpValue, .5f * Time.deltaTime);
    }
}
