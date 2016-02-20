using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OptionsController : MonoBehaviour {
	public Slider playerSlider;

	public LevelManager levelManager;


	void Start () {
		playerSlider.value = PlayerPrefsManager.GetPlayers ();
	}
	
	void Update () {
	}

	public void SaveAndExit() {
		PlayerPrefsManager.SetPlayers (playerSlider.value);
		levelManager.LoadLevel ("Start Menu");
	}

	public void SetDefaults() {
		playerSlider.value = 2f;
	}
}
