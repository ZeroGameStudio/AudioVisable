using UnityEngine;

using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]

public class AudioTimeTest : MonoBehaviour {

    public AudioClip audioClip;

    public Text audioTimeText;

    public Text audioTimeName;

    public Slider audioTimeSlider;

// Use this for initialization

void Start () {

        audioSource = GetComponent<AudioSource>();

        audioSource.clip = audioClip;

        audioTimeName.text = audioClip.name;

        clipHour = (int) audioSource.clip.length / 3600;

        clipMinute = (int) (audioSource.clip.length - clipHour * 3600)/ 60;

        clipSecond = (int) (audioSource.clip.length - clipHour * 3600 - clipMinute * 60);

        audioSource.Play();

        audioTimeSlider.onValueChanged.AddListener(

            (delegate {

                SetAudioTimeValueChange();

            })

        );

    }

// Update is called once per frame

void Update () {
        ShowAudioTime();
        audioTimeName.text = audioClip.name;
    }

    private void ShowAudioTime() {

        currentHour = (int)audioSource.time / 3600;

        currentMinute = (int)(audioSource.time - currentHour * 3600) / 60;

        currentSecond = (int)(audioSource.time - currentHour * 3600 - currentMinute * 60);

        audioTimeText.text = string.Format("{0:D2}:{1:D2}:{2:D2} / {3:D2}:{4:D2}:{5:D2}",

            currentHour, currentMinute, currentSecond, clipHour, clipMinute, clipSecond);

        audioTimeSlider.value = audioSource.time / audioClip.length;

    }

    private void SetAudioTimeValueChange() {

        audioSource.time = audioTimeSlider.value * audioSource.clip.length;

    }

    private AudioSource audioSource;

    private int currentHour;

    private int currentMinute;

    private int currentSecond;

    private int clipHour;

    private int clipMinute;

    private int clipSecond;

}