using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

#nullable enable

public class Weapon : MonoBehaviour {
	public UnityEvent<Rigidbody2D>	OnFire = new();
	public Transform?	spawn;
	public Rigidbody2D?	holder;
	public Rigidbody2D? proj;
	public bool auto = true;
	public bool scaleByWeapon = false;

	private void Start() {
		enabled = false;
	}

	public Item.Stats stats;
	float lastShot;

	private void OnEnable() {
		if (CanFire(stats)) Fire();
	}

	public void Fire() {
		lastShot = Time.time;
		Vector2 direction = spawn!.TransformVector(Vector2.up);

		for (int i = 0; i < stats.instancesPerActivation; i++) {
			var randomRotation = Quaternion.Euler(0f,0f, Random.Range(-stats.angle, stats.angle));
			var rb = Instantiate(proj!, spawn!.position, spawn!.rotation);
			if (scaleByWeapon) rb.transform.localScale = transform.lossyScale;
			rb.velocity = randomRotation * direction * stats.speed;
			rb.GetComponent<Damage>().value = stats.damage;
			OnFire?.Invoke(rb);
		}
		if (holder != null)
			holder!.velocity -= direction * stats.recoil;
	}
	public bool CanFire(Item.Stats modifiers) => Time.time > lastShot + stats.activationDelay;


	private void Update() {
		if (auto && CanFire(stats)) Fire();
	}

}
