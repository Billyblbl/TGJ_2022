using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

#nullable enable

public class Preview : MonoBehaviour {

	public Renderer[]	previewVFX = new Renderer[0];
	public UnityEvent<float>	OnRelease = new();
	float startTime;

	private void OnEnable() {
		startTime = Time.time;
		foreach (var vfx in previewVFX) vfx.enabled = true;
	}

	private void OnDisable() {
		foreach (var vfx in previewVFX) vfx.enabled = false;
		OnRelease?.Invoke(Time.time - startTime);
	}
}
