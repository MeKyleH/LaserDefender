using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LivesKeeper : MonoBehaviour {
	public static int lives;
	private Text lifeText;

	void Start() {
		lifeText = GetComponent<Text> ();
		Reset ();
	}
	
	public void SetLives(int livesRemaining) {
		lives = livesRemaining;
		lifeText.text = lives.ToString ();
	}

	public static void Reset() {
		lives = 5;
	}
}
