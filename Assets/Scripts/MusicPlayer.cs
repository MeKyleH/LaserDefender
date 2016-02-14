using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {
	static MusicPlayer instance = null;

	public AudioClip startClip;
	public AudioClip gameClip;
	public AudioClip endClip;

	private bool startClipPlaying = true;
	private AudioSource music;

	void Start () {

		if (instance != null && instance != this) {
			Destroy (gameObject);
			print ("Duplicate music player self-destructing!");
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
			music = GetComponent<AudioSource> ();
			music.clip = startClip;
			music.loop = true;
			music.Play ();
			Debug.Log ("Music Player Start else block: " + startClip); 
		}
	}

	void OnLevelWasLoaded(int level) {
		Debug.Log ("MusicPlayer loaded level " + level);
		if (level != 1 && level != 2 && startClipPlaying) {
			return;
			Debug.Log ("Start music is playing, no change");
		}
		music.Stop ();

		if (level == 0) {
			music.clip = startClip;
			startClipPlaying = true;
		} else if (level == 1) {
			music.clip = gameClip;
			startClipPlaying = false;
		} else if (level == 2) {
			music.clip = endClip;
		} 
		music.loop = true;
		music.Play ();
	}
}
