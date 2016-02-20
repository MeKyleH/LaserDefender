using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerPrefsManager : MonoBehaviour {
	const string PLAYERS_KEY = "players";


	public static void SetPlayers(float players) {
		if(players >= 1f && players <= 4f) {
			PlayerPrefs.SetFloat (PLAYERS_KEY, players);
		} else {
			Debug.LogError("Players out of range");
		}
	}

	public static float GetPlayers() {
		return PlayerPrefs.GetFloat (PLAYERS_KEY);
	}
}
