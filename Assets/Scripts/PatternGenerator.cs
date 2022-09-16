using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternGenerator : MonoBehaviour {

	//[Header("Projectile Settings")]
	//public int numberOfProjectiles;
	//public int projectilePerRotation;
	//public float projectileSpeed;
	public GameObject bullet;
	private GameObject bulletArray;
	//public GameObject totalBulletArray; // Every bullet get stored in here

	//********************************//
	//Arryay
	public int patternArrays; //Total bullet arrays
	public int bulletsPerArrays; //Bullets per array

	//Angle variables
	public float spreadBetweenArray; //Spread between arrays
	public float spreadWithinArray; //Spread between the last and the first bullet of an array
	public float startAngle = 0f; //Start angle
	private float defaultAngle = 0;
	private float arrayAngle, bulletAngle;

	//Spinning Variables
	public float spinRate; // The rate at which the pattern is spinning
	public float spinModificator = 0f; // The modificator of the spin rate
	public bool invertSpin; // (true = spinRate gets inversed once SpinRate >= maxSpinRate  || false = Spin doesn't invert at all)
	public float maxSpinRate; //The max spin rate ->if SpinRate >= maxSpinRate --> inverts spin

	//Fire rate variables
	public float fireRate = 10f;
	private bool canShoot = true;

	//Offsets
	public float objectWidth; //Width of the bullet firing object
	public float objectHeight; //Height of the bullet firing object
	public float xOffset; //Shift spawn point of the bullets along the X-Axis
	public float yOffset; //Shift spawn point of the bullets along the Y-Axis

	//Bullet Variables
	public float bulletSpeed;
	public float bulletAcceleration;
	[Range(0f, 1f)]
	public float bulletCurve;
	/****************************************/
		
	private Vector3 startPoint;
	
	private void Start() {

		bulletArray = GameObject.Find("BulletArray");

		int bulletLength = bulletsPerArrays - 1;
		if (bulletLength <= 0) {
			bulletLength = 1;
		}

		int arrayLength = patternArrays - 1 * patternArrays;
		if (arrayLength <= 0) {
			arrayLength = 1;
		}

		arrayAngle = spreadWithinArray / bulletLength;
		bulletAngle = spreadBetweenArray / arrayLength;


	}

	private void Update() {
		if (canShoot) {
			int bulletLength = bulletsPerArrays - 1;
			if (bulletLength <= 0) {
				bulletLength = 1;
			}

			int arrayLength = patternArrays - 1 * patternArrays;
			if (arrayLength <= 0) {
				arrayLength = 1;
			}

			arrayAngle = spreadWithinArray / bulletLength;
			bulletAngle = spreadBetweenArray / arrayLength;
			startPoint = transform.position;
			for (int i = 0; i < patternArrays; i++) { //For each bullet array in pattern
				for (int j = 0; j < bulletsPerArrays; j++) { //For each bullet in bullet array
					calculation(i, j, arrayAngle, bulletAngle);
				}
			}

			//If Default Angle > 360 , set it to 0
			if (defaultAngle > 360) {
				defaultAngle = 0f;
			}
			defaultAngle += spinRate; //Make the pattern spin
			spinRate += spinModificator; //Apply the spin modifier

			//Invert the spin if set to 1
			if (invertSpin) {
				if (spinRate < -maxSpinRate || spinRate > maxSpinRate) {
					spinModificator = -spinModificator;
				}
			}

			canShoot = false;
			StartCoroutine(ShootDelay());
		}
	}

	private void calculation(int i, int j, float arrayAngle, float bulletAngle) {
		//Calcuate the X and Y vales of the spawning points of each bullet
		float x1 = xOffset + lengthdir_x(objectWidth, defaultAngle + (bulletAngle * i) + (arrayAngle * j) + startAngle);
		float y1 = yOffset + lengthdir_y(objectHeight, defaultAngle + (bulletAngle * i) + (arrayAngle * j) + startAngle);

		//Calculate the direction for each bullets which it will move along
		float direction = defaultAngle + (bulletAngle * i) + (arrayAngle * j) + startAngle;

		//Create bullet
		EnemyProjectile tmpObj = Instantiate(bullet, startPoint, Quaternion.identity).GetComponent<EnemyProjectile>();
		tmpObj.transform.parent = bulletArray.transform;
		tmpObj.Initialize(x1, y1, direction, bulletSpeed, bulletAcceleration, bulletCurve);

	}

	private float lengthdir_x(float dist, float angle) {
		return dist * Mathf.Cos((angle * Mathf.PI) / 180);
	}

	private float lengthdir_y(float dist, float angle) {
		return dist * -Mathf.Sin((angle * Mathf.PI) / 180);
	}

	IEnumerator ShootDelay() {
		yield return new WaitForSeconds(fireRate);
		canShoot = true;
	}

	//IEnumerator PatternTime() {
	//	yield return new WaitForSeconds(patternTime);
	//	patternCanShoot = true;
	//}
}
