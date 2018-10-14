using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript_ex00 : MonoBehaviour {

	static Vector3[] resetPositions = {
		new Vector3(-9f, 0.60f, 0f),
		new Vector3(-10f, 0f, 0f),
		new Vector3(-9.5f, 1.5f, 0f),
	};
	static float[] moveSpeeds = {
		0.015f, 0.025f, 0.01f
	};
	static float[] jumpPower = {
		180f, 220f, 150f
	};

	public int playerIndex;
	public bool selected;

	new Rigidbody2D rigidbody2D;
	new Collider2D collider2D;

	bool grounded = false;
	float jumpInterval = 0.2f;
	float lastJumpTime = 0.0f;
	public Transform groundCheck;
	public LayerMask whatIsGround;
	float groundLength;

	// Use this for initialization
	void Start() {
		rigidbody2D = gameObject.GetComponent<Rigidbody2D>() as Rigidbody2D;
		collider2D = gameObject.GetComponent<Collider2D>() as Collider2D;
		BoxCollider2D boxCollider2D = collider2D as BoxCollider2D;
		groundLength = boxCollider2D.size.x;
		Reset();
		selected = (0 == playerIndex);
	}

	// Update is called once per frame
	void Update() {
		UpdateCharacterSelection();

		if (Input.GetKey(KeyCode.R)) {
			Reset();
		}
	}

	private void FixedUpdate() {
		if (selected) UpdateCharacterAction();
		if (!selected && System.Math.Abs(Vector3.Magnitude(rigidbody2D.velocity)) > 0.01f) {
			rigidbody2D.velocity = new Vector3(0, rigidbody2D.velocity.y, 0);
		}

		Collider2D[] colliders = new Collider2D[10];
		ContactFilter2D filter = new ContactFilter2D();
		filter.SetLayerMask(whatIsGround);
		int num = Physics2D.OverlapArea(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y) + new Vector2(groundLength - 0.02f, -0.01f), filter, colliders);
		if (num > 1) {
			grounded = true;
		} else {
			grounded = false;
		}
		if (gameObject.transform.position.y < -20) {
			Reset();
		}
	}

	void UpdateCharacterSelection() {
		if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1)) {
			selected = (0 == playerIndex);
		} else if (Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2)) {
			selected = (1 == playerIndex);
		} else if (Input.GetKeyDown(KeyCode.Keypad3) || Input.GetKeyDown(KeyCode.Alpha3)) {
			selected = (2 == playerIndex);
		}
	}

	void UpdateCharacterAction() {
		if (grounded && Time.time - lastJumpTime > jumpInterval && Input.GetKey(KeyCode.Space)) {
			rigidbody2D.AddForce(Vector2.up * jumpPower[playerIndex], ForceMode2D.Force);
			grounded = false;
			lastJumpTime = Time.time;
		}
		if (Input.GetKey(KeyCode.RightArrow)) {
			rigidbody2D.velocity = new Vector3(moveSpeeds[playerIndex] * 100, rigidbody2D.velocity.y, 0);
		} else if (Input.GetKey(KeyCode.LeftArrow)) {
			rigidbody2D.velocity = new Vector3(-moveSpeeds[playerIndex] * 100, rigidbody2D.velocity.y, 0);
		} else {
			rigidbody2D.velocity = new Vector3(0, rigidbody2D.velocity.y, 0);
		}
	}

	void Reset() {
		gameObject.transform.position = resetPositions[playerIndex];
		rigidbody2D.velocity = new Vector3(0f, 0f, 0f);
	}
}
