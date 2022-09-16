using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {

	public enum SpawnState { SPAWNING, WAITING, COUNTING };

	[System.Serializable]
	public class Wave {
		public string name;
		public Transform enemy;
		public int count;
		public float rate;
	}

	public Wave[] waves;
	private int nextWave = 0;

	public float timeBetweenWaves = 2f;
	public float waveCountdown;

	private float searchCountdown = 1f;

	public SpawnState state = SpawnState.COUNTING;

    // Start is called before the first frame update
    void Start() {
		waveCountdown = timeBetweenWaves;
    }

    // Update is called once per frame
    void Update() {

		if (state == SpawnState.WAITING) {
			if (EnemyIsAlive()) {
				WaveCompleted();
				return;
			} else {
				return;
			}
		}

		if (waveCountdown <= 0) {
			if (state != SpawnState.SPAWNING) {
				StartCoroutine(SpawnWave(waves[nextWave]));
			} 
		} else {
			waveCountdown -= Time.deltaTime;
		}
	}

	bool EnemyIsAlive() {

		searchCountdown -= Time.deltaTime;
		if (searchCountdown <= 0f) {
			searchCountdown = 1f;
			if(GameObject.FindGameObjectsWithTag("Enemy").Length <= 0) {
				return true;
			}
		}
		return false;
	}

	
	IEnumerator SpawnWave(Wave _wave) {
		state = SpawnState.SPAWNING;

		for (int i = 0; i < _wave.count; i++) {
			SpawnEnemy(_wave.enemy);
			yield return new WaitForSeconds(1f / _wave.rate);
		}

		state = SpawnState.WAITING;
		yield break;
	}

	void SpawnEnemy(Transform _enemy) {
		Instantiate(_enemy, transform.position, _enemy.rotation);
	}

	void WaveCompleted() {
		state = SpawnState.COUNTING;
		waveCountdown = timeBetweenWaves;

		if(nextWave + 1 > waves.Length - 1) {
			return;
		} else {
			nextWave++;
		}
	}
}

