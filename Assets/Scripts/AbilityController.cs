using UnityEngine;
using System.Linq;

#nullable enable

public class AbilityController : MonoBehaviour {

	public Gauge energy;

	[System.Serializable] public struct Ability {
		public string name;
		public MonoBehaviour?[] effects;
		public float activationCost;
		public float drainRate;

		public bool wasEnabledThisUpdate { get; private set; }

		bool _enabled;
		public bool enabled { get => _enabled; set {
			wasEnabledThisUpdate = (!_enabled && value);
			_enabled = value;
			foreach (var effect in effects) if (effect != null) effect.enabled = value;
		}}
	}

	public Ability[] abilities = new Ability[0];

	public bool TrySetActivity(int i, bool value) {
		if (!value || abilities[i].activationCost < energy.current) {
			abilities[i].enabled = value;
			if (abilities[i].wasEnabledThisUpdate) energy.current -= abilities[i].activationCost;
		}
		return abilities[i].enabled;
	}

	private void Update() {
		energy.Update(Time.time, Time.deltaTime);

		foreach (var (ability, i) in abilities.Select((a, i) => (a, i))) if (ability.enabled) {
			var drain = ability.drainRate * Time.deltaTime;
			if (drain > energy.current) {
				abilities[i].enabled = false;
			} else {
				energy.current -= drain;
			}
		}
	}

}
