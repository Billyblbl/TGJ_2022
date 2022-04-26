using UnityEngine;

[CreateAssetMenu(menuName = "TwinStickRoguelike/Item")]
public class Item : ScriptableObject {
	[System.Serializable] public struct Stats {
		[Tooltip("Degrees")] public float angle;
		public float activationDelay;
		public float recoil;
		public float damage;
		public float speed;
		public int instancesPerActivation;
		public static Stats operator +(Stats a, Stats b) {
			Stats c;
			c.activationDelay = a.activationDelay + b.activationDelay;
			c.angle = a.angle + b.angle;
			c.recoil = a.recoil + b.recoil;
			c.damage = a.damage + b.damage;
			c.speed = a.speed + b.speed;
			c.instancesPerActivation = a.instancesPerActivation + b.instancesPerActivation;
			return c;
		}
	}

	public Stats stats;
	public Sprite inGameSprite;
}
