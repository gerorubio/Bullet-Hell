using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

	[System.Serializable]
	public enum MovementType {
		Linear,
		Quadratic,
		Cubic,
		Quartic,
		Quintic,
		Sextic,
		Septic,
		Octic,
		Nonic,
		Decic
	}
	public MovementType movementType;
	
	[System.Serializable]
	public enum EnemyType {
		Fortress, Loop, Normal
	}
	public EnemyType enemyType;

	private bool isEntering = true;
	
	public float speed;
	public bool canLoop = true;
	public bool mirror;

	private float starTime;
	private float travelDistance = 0;
	private float distanceCovered;
	private bool end = false;

	public string A = "00", B = "00", C = "00", D = "00", E = "00", F = "00", G = "00", H = "00", I = "00", J = "00", K = "00";

	private Transform[] points = new Transform[11];
	private Transform enemy;

	// Start is called before the first frame update
	void Start() {
		UnityEngine.Random.InitState(System.DateTime.Now.Day + System.DateTime.Now.Month + System.DateTime.Now.Year + System.DateTime.Now.Hour + System.DateTime.Now.Minute + System.DateTime.Now.Second + System.DateTime.Now.Millisecond);

		starTime = Time.time;
		enemy = this.transform;

		points[0] = GameObject.Find("Point" + A).transform;
		points[1] = GameObject.Find("Point" + B).transform;
		points[2] = GameObject.Find("Point" + C).transform;
		points[3] = GameObject.Find("Point" + D).transform;
		points[4] = GameObject.Find("Point" + E).transform;
		points[5] = GameObject.Find("Point" + F).transform;
		points[6] = GameObject.Find("Point" + G).transform;
		points[7] = GameObject.Find("Point" + H).transform;
		points[8] = GameObject.Find("Point" + I).transform;
		points[9] = GameObject.Find("Point" + J).transform;
		points[10] = GameObject.Find("Point" + K).transform;

		if (mirror) {
			Mirror();
		}

		switch (movementType) {
			case MovementType.Decic:
				travelDistance += Vector3.Distance(points[9].position, points[10].position);
				goto case MovementType.Nonic;
			case MovementType.Nonic:
				travelDistance += Vector3.Distance(points[8].position, points[9].position);
				goto case MovementType.Octic;
			case MovementType.Octic:
				travelDistance += Vector3.Distance(points[7].position, points[8].position);
				goto case MovementType.Septic;
			case MovementType.Septic:
				travelDistance += Vector3.Distance(points[6].position, points[7].position);
				goto case MovementType.Sextic;
			case MovementType.Sextic:
				travelDistance += Vector3.Distance(points[5].position, points[6].position);
				goto case MovementType.Quintic;
			case MovementType.Quintic:
				travelDistance += Vector3.Distance(points[4].position, points[5].position);
				goto case MovementType.Quartic;
			case MovementType.Quartic:
				travelDistance += Vector3.Distance(points[3].position, points[4].position);
				goto case MovementType.Cubic;
			case MovementType.Cubic:
				travelDistance += Vector3.Distance(points[2].position, points[3].position);
				goto case MovementType.Quadratic;
			case MovementType.Quadratic:
				travelDistance += Vector3.Distance(points[1].position, points[2].position);
				goto case MovementType.Linear;
			case MovementType.Linear:
				travelDistance = Vector3.Distance(points[0].position, points[1].position);
				break;
		}
	}

	private void Mirror() {

		Dictionary<MovementType, int> degreeValues = new Dictionary<MovementType, int>() {
			{MovementType.Linear, 1},
			{MovementType.Quadratic, 2},
			{MovementType.Cubic, 3},
			{MovementType.Quartic, 4},
			{MovementType.Quintic, 5},
			{MovementType.Sextic, 6},
			{MovementType.Septic, 7},
			{MovementType.Octic, 8},
			{MovementType.Nonic, 9},
			{MovementType.Decic, 10}
		};
		int degree = degreeValues[movementType];

		string[] arreglo = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K" };
		Transform aux;
		string auxS;

		if (UnityEngine.Random.Range(0f, 1f) >= 0.5) {
			for (int i = 0; i <= degree - 1; i++) {
				aux = points[i];
				points[i] = points[degree];
				points[degree] = aux;
				auxS = arreglo[i];
				arreglo[i] = arreglo[degree];
				arreglo[degree] = auxS;
				degree--;
			}
		}
		Debug.Log("Points: " + String.Join("",
			new List<string>(arreglo)
			.ConvertAll(i => i.ToString())
			.ToArray()));
	}

    // Update is called once per frame
    void Update() {
		if (isEntering) {
			Enter();
		} else {
			Debug.Log("Not entering");
		}





		

	}

	private void Enter() {
		distanceCovered = (Time.time - starTime) * speed;
		float fractionOfJourney = distanceCovered / travelDistance;

		switch (movementType) {
			case MovementType.Linear:
				enemy.position = Vector3.Lerp(points[0].position, points[1].position, fractionOfJourney);
				break;
			case MovementType.Quadratic:
				enemy.position = QuadraticLerp(points[0].position, points[1].position, points[2].position, fractionOfJourney);
				break;
			case MovementType.Cubic:
				enemy.position = CubicLerp(points[0].position, points[1].position, points[2].position, points[3].position, fractionOfJourney);
				break;
			case MovementType.Quartic:
				enemy.position = QuarticLerp(points[0].position, points[1].position, points[2].position, points[3].position, points[4].position, fractionOfJourney);
				break;
			case MovementType.Quintic:
				enemy.position = QuinticLerp(points[0].position, points[1].position, points[2].position, points[3].position, points[4].position, points[5].position, fractionOfJourney);
				break;
			case MovementType.Sextic:
				enemy.position = SexticLerp(points[0].position, points[1].position, points[2].position, points[3].position, points[4].position, points[5].position, points[6].position, fractionOfJourney);
				break;
			case MovementType.Septic:
				enemy.position = SepticLerp(points[0].position, points[1].position, points[2].position, points[3].position, points[4].position, points[5].position, points[6].position, points[7].position, fractionOfJourney);
				break;
			case MovementType.Octic:
				enemy.position = OcticLerp(points[0].position, points[1].position, points[2].position, points[3].position, points[4].position, points[5].position, points[6].position, points[7].position, points[8].position, fractionOfJourney);
				break;
			case MovementType.Nonic:
				enemy.position = NonicLerp(points[0].position, points[1].position, points[2].position, points[3].position, points[4].position, points[5].position, points[6].position, points[7].position, points[8].position, points[9].position, fractionOfJourney);
				break;
			case MovementType.Decic:
				enemy.position = DecicLerp(points[0].position, points[1].position, points[2].position, points[3].position, points[4].position, points[5].position, points[6].position, points[7].position, points[8].position, points[9].position, points[10].position, fractionOfJourney);
				break;
			default:
				break;
		}

		if (fractionOfJourney >= 1.5f && canLoop) {
			starTime = Time.time;
		} else if (!canLoop) {
			end = true;
		}

		if (end) {
			return;
		}
	}

	private Vector3 QuadraticLerp(Vector3 a, Vector3 b, Vector3 c, float t) {
		Vector3 ab = Vector3.Lerp(a, b, t);
		Vector3 bc = Vector3.Lerp(b, c, t);

		return Vector3.Lerp(ab, bc, t);
	}

	private Vector3 CubicLerp(Vector3 a, Vector3 b, Vector3 c, Vector3 d, float t) {
		Vector3 ab_bc = QuadraticLerp(a, b, c, t);
		Vector3 bc_cd = QuadraticLerp(b, c, d, t);

		return Vector3.Lerp(ab_bc, bc_cd, t);
	}

	private Vector3 QuarticLerp(Vector3 a, Vector3 b, Vector3 c, Vector3 d, Vector3 e, float t) {
		Vector3 abc_bcd = CubicLerp(a, b, c, d, t);
		Vector3 bcd_cde = CubicLerp(b, c, d, e, t);

		return Vector3.Lerp(abc_bcd, bcd_cde, t);
	}

	private Vector3 QuinticLerp(Vector3 a, Vector3 b, Vector3 c, Vector3 d, Vector3 e, Vector3 f, float t) {
		Vector3 abcd_bcde = QuarticLerp(a, b, c, d, e, t);
		Vector3 bcde_cdef = QuarticLerp(b, c, d, e, f, t);

		return Vector3.Lerp(abcd_bcde, bcde_cdef, t);
	}

	private Vector3 SexticLerp(Vector3 a, Vector3 b, Vector3 c, Vector3 d, Vector3 e, Vector3 f, Vector3 g, float t) {
		Vector3 abcde_bcdef = QuinticLerp(a, b, c, d, e, f, t);
		Vector3 bcdef_cdefg = QuinticLerp(b, c, d, e, f, g, t);

		return Vector3.Lerp(abcde_bcdef, bcdef_cdefg, t);
	}

	private Vector3 SepticLerp(Vector3 a, Vector3 b, Vector3 c, Vector3 d, Vector3 e, Vector3 f, Vector3 g, Vector3 h, float t) {
		Vector3 abcdef_bcdefg = SexticLerp(a, b, c, d, e, f, g, t);
		Vector3 bcdefg_cdefgh = SexticLerp(b, c, d, e, f, g, h, t);

		return Vector3.Lerp(abcdef_bcdefg, bcdefg_cdefgh, t);
	}

	private Vector3 OcticLerp(Vector3 a, Vector3 b, Vector3 c, Vector3 d, Vector3 e, Vector3 f, Vector3 g, Vector3 h, Vector3 i, float t) {
		Vector3 abcdefg_bcdefgh = SepticLerp(a, b, c, d, e, f, g, h, t);
		Vector3 bcdefgh_cdefghi = SepticLerp(b, c, d, e, f, g, h, i, t);

		return Vector3.Lerp(abcdefg_bcdefgh, bcdefgh_cdefghi, t);
	}

	private Vector3 NonicLerp(Vector3 a, Vector3 b, Vector3 c, Vector3 d, Vector3 e, Vector3 f, Vector3 g, Vector3 h, Vector3 i, Vector3 j, float t) {
		Vector3 abcdefgh_bcdefghi = OcticLerp(a, b, c, d, e, f, g, h, i, t);
		Vector3 bcdefghi_cdefghij = OcticLerp(b, c, d, e, f, g, h, i, j, t);

		return Vector3.Lerp(abcdefgh_bcdefghi, bcdefghi_cdefghij, t);
	}

	private Vector3 DecicLerp(Vector3 a, Vector3 b, Vector3 c, Vector3 d, Vector3 e, Vector3 f, Vector3 g, Vector3 h, Vector3 i, Vector3 j, Vector3 k, float t) {
		Vector3 abcdefghi_bcdefghij = NonicLerp(a, b, c, d, e, f, g, h, i, j, t);
		Vector3 bcdefghij_cdefghijk = NonicLerp(b, c, d, e, f, g, h, i, j, k, t);

		return Vector3.Lerp(abcdefghi_bcdefghij, bcdefghij_cdefghijk, t);
	}

}

