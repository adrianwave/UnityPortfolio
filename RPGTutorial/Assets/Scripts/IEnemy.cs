using UnityEngine;
using System.Collections;

public interface IEnemy {

	int Experience {get; set;}
	void TakeDamage(int amount);
	void PerformAttack();
	void Die();

}
