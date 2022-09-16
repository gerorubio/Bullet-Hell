using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour {
	public TMP_Text scoreText;
	private LevelSystem levelSystem;

	public void SetScore(int score) {
		scoreText.text = score.ToString("D20");
	}

}

