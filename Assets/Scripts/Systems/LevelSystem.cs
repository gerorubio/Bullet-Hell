using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem {

	public int level;
	private int totalScore;
	private int score;
	private int scoreToNextLevel;
	public Canvas canvas;

    public LevelSystem() {
		level = 1;
		totalScore = 0;
		score = 0;
		scoreToNextLevel = 100;
	}

	public void AddExperience(int amount) {
		totalScore += amount;
		canvas.GetComponent<UI>().SetScore(totalScore);
		score += amount;
		if (score >= scoreToNextLevel) {
			level++;
			score -= scoreToNextLevel; 
		}
	}

}

