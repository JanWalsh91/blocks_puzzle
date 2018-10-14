using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript_ex01 : MonoBehaviour {

	public Vector3 resetPositions;
	static float[] moveSpeeds = {
		0.015f, 0.025f, 0.01f
	};
	static float[] jumpPower = {
		180f, 220f, 150f
	};

	public int playerIndex;
	public GameObject endBox;
	public bool selected;
	public bool success;
	public bool onPlatform = false;
	public GameObject players;
	public GameObject gameManager;

	new Rigidbody2D rigidbody2D;
	new Collider2D collider2D;

	bool grounded = false;
	float jumpInterval = 0.2f;
	float lastJumpTime = 0.0f;
	public Transform groundCheck;
	public LayerMask whatIsGround;
	float groundLength;
	float successTime = 0;
	float successFreezeTime = 0.2f;
	float maxSpeed = 5f;

	// Use this for initialization
	void Start() {
		rigidbody2D = gameObject.GetComponent<Rigidbody2D>() as Rigidbody2D;
		collider2D = gameObject.GetComponent<Collider2D>() as Collider2D;
		BoxCollider2D boxCollider2D = collider2D as BoxCollider2D;
		groundLength = boxCollider2D.size.x;
		Reset();
		selected = (0 == playerIndex);
		success = false;
	}

	// Update is called once per frame
	void Update() {
		UpdateCharacterSelection();

		if (Vector2.Distance(transform.position, endBox.transform.position) < 0.1f) {
			if (!success) {
				transform.position = endBox.transform.position;
				endBox.GetComponent<SpriteRenderer>().color = Color.magenta;
				success = true;
				successTime = Time.time;
			}
		} else {
			success = false;
			endBox.GetComponent<SpriteRenderer>().color = Color.white;
		}

		if (Input.GetKey(KeyCode.R)) {
			Reset();
		}
	}

	void FixedUpdate() {
		// if success and not enough time past since your success, you cannot move
		if (success && (Time.time - successTime < successFreezeTime)) {
			rigidbody2D.velocity = new Vector3(0, rigidbody2D.velocity.y, 0);
			return;
		}
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
		if (gameObject.transform.position.y < -10) {
			if (gameManager) {
				gameManager.GetComponent<GameManager>().Reset();
			} else {
				Reset();
			}
		}

		rigidbody2D.velocity = Vector3.ClampMagnitude(rigidbody2D.velocity, maxSpeed);
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

	public void Reset() {
		gameObject.transform.position = resetPositions;
		if (rigidbody2D) {
			rigidbody2D.velocity = Vector3.zero;
		}
		transform.parent = players.transform;
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.CompareTag("MovingPlatform")) {
			// set parent of current object to what it moving platform it collided with
			transform.parent = collision.gameObject.transform;
			onPlatform = true;
		} else if (collision.gameObject.CompareTag("Player") && collision.gameObject.transform.parent.gameObject.CompareTag("MovingPlatform")) {
			transform.parent = collision.gameObject.transform.parent.transform;
		} else if (collision.gameObject.CompareTag("Death")) {
			gameManager.GetComponent<GameManager>().Reset();
		}
	}

	void OnCollisionExit2D(Collision2D collision) {
		if (collision.gameObject.CompareTag("MovingPlatform")) {
			foreach (Transform child in transform.parent) {
				if (child.CompareTag("Player")) {
					child.parent = players.transform;
				}
			}
			onPlatform = false;
		}
		if (collision.gameObject.CompareTag("Player") && !onPlatform) {
			transform.parent = players.transform;
		}
	}
}
