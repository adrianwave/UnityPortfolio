using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public CharacterStats characterStats;
	public int currentHealth;
	public int maxHealth;
	public PlayerLevel PlayerLevel { get; set; }
	void Start()
	{
		PlayerLevel = GetComponent<PlayerLevel>();
		this.currentHealth = this.maxHealth;
		characterStats = new CharacterStats(10, 10, 10);
		//UIEventHandler.HealthChanged(this.currentHealth, this.maxHealth);
	}

	public void TakeDamage(int amount){
		currentHealth -= amount;
		if(currentHealth <= 0) {
			Die();
		}
		UIEventHandler.HealthChanged(this.currentHealth, this.maxHealth);
	}

	void Die() {

		Debug.Log("Player has parished.");
		this.currentHealth = this.maxHealth;
		UIEventHandler.HealthChanged(this.currentHealth, this.maxHealth);
	}
}
