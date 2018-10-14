using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTower : MonoBehaviour {

	public string color;
	[Range(0.0f, 20f)]
	public float shootInterval;
	public GameObject bulletPrefab;
	public float bulletLifeTime;
	public Vector3 bulletInitialVelocity;
	public Vector3 bulletStartPosition;

	float lastShootTime;

	// Use this for initialization
	void Start () {
		lastShootTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - lastShootTime > shootInterval) {
			lastShootTime = Time.time;
			shootBullet();
		}
	}

	void shootBullet() {
		GameObject bullet = Instantiate(bulletPrefab, transform.position + bulletStartPosition, Quaternion.identity);
		// set color
		// set physical layer
		bullet.GetComponent<Renderer>().material.color = ColorLayerInfo.GetColorByLayerName(color);
		bullet.layer = LayerMask.NameToLayer(color);
		Rigidbody2D rb;
		rb = bullet.GetComponent<Rigidbody2D>() as Rigidbody2D;
		rb.velocity = bulletInitialVelocity;
		bullet.GetComponent<Bullet>().lifeTime = bulletLifeTime;

	}
}
