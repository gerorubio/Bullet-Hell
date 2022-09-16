using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaser : Projectile {

	public float laserDuration = 0.5f;
	public float damageCooldown = 0.1f;
	[HideInInspector]
	public GameObject firePoint;

	private bool canDamage = true;

	private LineRenderer line;

	public override void Start() {
		line = gameObject.GetComponent<LineRenderer>();
		StartCoroutine(LaserTime());
    }

    // Update is called once per frame
    public override void Update() {
		Vector3 firePosition = firePoint.transform.position;
		RaycastHit2D collision = Physics2D.Raycast(firePosition, firePoint.transform.up); //Detects collision with an object creating a raycast
		//Line size handler
		line.transform.position = firePosition;
		
		Enemy enemy = null;
		try {
			enemy = collision.transform.GetComponent<Enemy>();
		}
		#pragma warning disable 0168
		catch (NullReferenceException ex)
		#pragma warning restore 0168
		{

		}

		if (collision && enemy != null) {
			line.SetPosition(1, collision.point - new Vector2(firePosition.x, firePosition.y));
			//Instantiate(hitAnimation, collision.point - new Vector2(firePosition.x, firePosition.y), Quaternion.identity);
			//Damage Handler
			if (canDamage) {
				//Damage logic
				enemy.TakeDamage(damage);
				canDamage = false;
				StartCoroutine(DamageCooldown());
			}
		} else {
			line.SetPosition(1, firePoint.transform.up * 40);
		}
		
	}

	IEnumerator LaserTime() {
		yield return new WaitForSeconds(laserDuration);
		Destroy(gameObject);
	}

	IEnumerator DamageCooldown() {
		yield return new WaitForSeconds(damageCooldown);
		canDamage = true;
	}
}

