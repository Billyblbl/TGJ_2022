using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

#nullable enable

public class PlayerController : TopDown2DController {

	public PlayerInput?	controls;
	public AbilityController? abilityController;
	public float deathDuration = .6f;
	public float movementSensibility = 1f;

	override protected void Update() {
		base.Update();

		var movementInput = controls!.actions["Fly"].ReadValue<Vector2>();

		for (int i = 0; i < abilityController!.abilities.Length; i++) {
			abilityController.TrySetActivity(i, controls!.actions[string.Format("Ability{0}", 1+i)].ReadValue<float>() > float.Epsilon);
		}

		if (controls!.currentControlScheme == "Keyboard & Mouse")
			movement = Vector2.Lerp(movement, movementInput, Time.deltaTime * movementSensibility);
		else
			movement = movementInput;
	}

	private void FixedUpdate() {
		UpdateControls(Time.fixedDeltaTime);
	}

	public void OnDeath() {
		StartCoroutine(Die());
	}

	IEnumerator	Die() {
		yield return new WaitForSeconds(deathDuration);
		gameObject.SetActive(false);
	}


}
