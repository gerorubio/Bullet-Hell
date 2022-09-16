using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public float speed;
	public int damage;
	public GameObject hitAnimation;

	//Despawn variables
	private float objectWidth;
	private float objectHeight;
	private GameObject quad;
	private float quadWidth, quadHeight;

	public virtual void Start() {
		quad = GameObject.FindGameObjectWithTag("Background");
		quadWidth = quad.GetComponent<MeshRenderer>().bounds.extents.x;
		quadHeight = quad.GetComponent<MeshRenderer>().bounds.extents.y;

		objectWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x; //extents = size of width / 2
		objectHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y; //extents = size of height / 2
	}

	public virtual void Update() {
		DespwanCheck();
	}

	private void DespwanCheck() {
		if (transform.position.x - objectWidth > quad.transform.position.x + quadWidth || transform.position.x + objectWidth < quad.transform.position.x - quadWidth || transform.position.y - objectHeight > quad.transform.position.y + quadHeight || transform.position.y + objectHeight < quad.transform.position.y - quadHeight) {
			Destroy(gameObject);
		}
	}
}

