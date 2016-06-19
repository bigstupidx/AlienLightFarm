using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour {


    public AudioSource click;
    public AudioSource click2;
    public AudioSource build;
    public AudioSource clickButton;
    public AudioSource death;
    public AudioSource born;
    public AudioSource jump;
    public AudioSource heal;
    public AudioSource blackHole;

    bool isOnAudio;

    Library library;
	// Use this for initialization
	void Awake () {
        library = GameObject.FindObjectOfType<Library>();
	}
	
	// Update is called once per frame
	void Start () {
        isOnAudio = PreferencesSaver.GetSound();
	}

    public void Click()
    {
        if(isOnAudio)
        click.Play();
    }

    public void Click2()
    {
        if (isOnAudio)
            click2.Play();
    }

    public void Build()
    {
        if (isOnAudio)
            build.Play();
    }

    public void ClickButton()
    {
        if (isOnAudio)
            clickButton.Play();
    }

    public void Death()
    {
        if (isOnAudio)
            death.Play();
    }

    public void Born()
    {
        if (isOnAudio)
            born.Play();
    }

    public void Jump()
    {
        if (isOnAudio)
            jump.Play();
    }

    public void BlackHole()
    {
        if (isOnAudio)
            blackHole.Play();
    }

    public void Heal()
    {
        if (isOnAudio)
            heal.Play();
    }

    public void ChangeAudio()
    {
        if (isOnAudio)
            OffAudio();
        else
            OnAudio();
    }

    public void OffAudio()
    {
        isOnAudio = false;
        library.soundButton.OffAudio();

        PreferencesSaver.SetSound(isOnAudio);
    }

    public void OnAudio()
    {
        isOnAudio = true;
        library.soundButton.OnAudio();
        PreferencesSaver.SetSound(isOnAudio);

    }

}
