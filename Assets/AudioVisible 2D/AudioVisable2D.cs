using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AudioVisable2D : MonoBehaviour
{
    public AudioSource _audio;
    [Range(64, 128 * 2)]
    public int _sampleLenght = 128 * 2;
    private float[] _samples;
    private List<Image> _uiList = new List<Image>();
    public RectTransform _uiParentRect;
    public GameObject _prefab;
    public float _uiDistance;
    [Range(1, 30)]
    public float UpLerp = 12;

    void Start()
    {
        CreatUI();
        _samples = new float[_sampleLenght];
    }
    private void CreatUI()
    {
        for (int i = 0; i < _sampleLenght; i++)
        {
            GameObject _prefab_GO = Instantiate(_prefab, _uiParentRect.transform);
            _prefab_GO.name = string.Format("Sample[{0}]", i + 1);
            _uiList.Add(_prefab_GO.GetComponent<Image>());
            RectTransform _rectTransform = _prefab_GO.GetComponent<RectTransform>();
            _rectTransform.localPosition = new Vector3(_rectTransform.sizeDelta.x + _uiDistance * i, 0, 0);
        }
    }

    void Update()
    {
        _audio.GetSpectrumData(_samples, 0, FFTWindow.BlackmanHarris);
        for (int i = 0; i < _uiList.Count; i++)
        {
            Vector3 _v3 = _uiList[i].transform.localScale;
            _v3 = new Vector3(1, Mathf.Clamp(_samples[i] * (50 + i * i * 0.5f), 0, 50), 1);
            _uiList[i].transform.localScale = Vector3.Lerp(_uiList[i].transform.localScale, _v3, Time.deltaTime * UpLerp);
        }
    }


}