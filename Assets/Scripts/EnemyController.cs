using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable enable

public class EnemyController : TopDown2DController {
	public AbilityController? abilityController;

	private void OnEnable() {
		target = FindObjectOfType<PlayerController>().transform;
	}

	public float targetDistance = 2f;
	public bool	maintainDistance = false;
	public float aimCone = 20f;
	public float maxRange = 4f;

	void ChooseMovement() {
		movement = Vector2.zero;
		var delta = target!.position - transform.position;
		if (delta.magnitude > targetDistance || maintainDistance) {
			var dir = delta.normalized;
			var targetDelta = delta - dir * targetDistance;
			movement = Vector2.ClampMagnitude(targetDelta, 1f);
		}
	}

	bool aimedShot { get {
		var delta = target!.position - transform.position;
		if (delta.magnitude > maxRange) return false;
		var angle = Vector2.Angle(delta, transform.up);
		return angle * 2 < aimCone;
	} }

	override protected void Update() {
		base.Update();
		ChooseMovement();
		if (abilityController!.abilities.Length >= 0) abilityController.TrySetActivity(0, aimedShot);
	}

	private void FixedUpdate() {
		UpdateControls(Time.fixedDeltaTime);
	}

}
