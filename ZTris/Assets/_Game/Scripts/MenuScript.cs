using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

    public AudioClip startClip;

    private AudioSource audio;

    // Use this for initialization
    void Start () {
        this.audio = GetComponent<AudioSource>();
    }

    void Awake()
    {
        this.audio = GetComponent<AudioSource>();
    }

	// Update is called once per frame
	void Update () {
	
	}

    public void StartGamePress()
    {
        PlaySoundWithCallback(startClip, LoadLevelOne);
        //audio.PlayOneShot(startClip);
    }

    public void LoadLevelOne()
    {
        SceneManager.LoadScene("GridBased");
    }

    public void ExitGamePress()
    {
        Application.Quit();
    }

    // sound cube
    public delegate void AudioCallback();
    public void PlaySoundWithCallback(AudioClip clip, AudioCallback callback)
    {
        audio.PlayOneShot(clip);
        StartCoroutine(DelayedCallback(clip.length, callback));
    }
    private IEnumerator DelayedCallback(float time, AudioCallback callback)
    {
        yield return new WaitForSeconds(time);
        callback();
    }

}
