using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SoundButton : MonoBehaviour {

    public Sprite onAudioImage;
    public Sprite offAudioImage;

    Library library;

    void Awake()
    {
        library = GameObject.FindObjectOfType<Library>();
    }


	public void OnAudio()
    {
        GetComponent<Image>().sprite = onAudioImage;
    }

    public void OffAudio()
    {
        GetComponent<Image>().sprite = offAudioImage;

    }
}
