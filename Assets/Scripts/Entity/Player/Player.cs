using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity {
	
	//Shoot variables
	public float bulletFireRate = 1f;
	public float missileFireRate = 1.5f;
	public float laserFireRate = 2f;
	private float currentFireRate;
	private bool canShoot = true;

	public LevelSystem levelSystem;
	[SerializeField]
	private int currentHP;

	//Weapon variables
	[System.Serializable]
	private enum SpecializedType {
		Unarmored,
		Armor,
		Shield
	}
	private SpecializedType currentWeapon = SpecializedType.Unarmored;
	
	public GameObject bullet, missile, laser;
	public GameObject[] firePoints;
	private GameObject currentFirePoint;

	private void Awake() {
		levelSystem = new LevelSystem();
		canvas = GameObject.FindGameObjectWithTag("Canvas");
		canvas.GetComponent<UI>().SetScore(0);
	}

	// Start is called before the first frame update
	void Start() {
		currentFireRate = bulletFireRate;
		currentHP = health;
		currentFirePoint = firePoints[levelSystem.level - 1];
    }

	public void Shoot() {
		if (canShoot) {
			foreach (Transform firePoint in currentFirePoint.transform) {
				switch (currentWeapon) {
					case SpecializedType.Unarmored:
						CreateProjectile(bullet, firePoint);
						break;
					case SpecializedType.Armor:
						CreateProjectile(missile, firePoint); 
						break;
					case SpecializedType.Shield:
						GameObject laserClone = CreateProjectile(laser, firePoint);
						laserClone.GetComponent<PlayerLaser>().firePoint = firePoint.gameObject;
						break;
					default:
						break;
				}
			}
			canShoot = false;
			StartCoroutine(ShootDelay());
		}
	}

	public void ChangeWeapon() {
		switch (currentWeapon) {
			case SpecializedType.Unarmored:
				currentFireRate = missileFireRate;
				currentWeapon = SpecializedType.Armor;
				break;
			case SpecializedType.Armor:
				currentFireRate = laserFireRate;
				currentWeapon = SpecializedType.Shield;
				break;
			case SpecializedType.Shield:
				currentFireRate = bulletFireRate;
				currentWeapon = SpecializedType.Unarmored;
				break;
			default:
				currentFireRate = bulletFireRate;
				currentWeapon = SpecializedType.Unarmored;
				break;
		}
	}

	public void CastSkill() {
		throw new NotImplementedException();
	}

	public void ChangeAbility() {
		throw new NotImplementedException();
	}

	public GameObject CreateProjectile(GameObject projectile, Transform firePoint) {
		return Instantiate(projectile, firePoint.transform.position, firePoint.transform.rotation);
	}

	IEnumerator ShootDelay() {
		yield return new WaitForSeconds(currentFireRate);
		canShoot = true;
	}

	public override void TakeDamage(int damage) {
		currentHP -= damage;
		if (currentHP <= 0) {
			//Score handling etc
			Destroy(gameObject);
		} else {
			// Clear near bullets
			// Set invulnerability for certain amount of time
		}
	}
}
