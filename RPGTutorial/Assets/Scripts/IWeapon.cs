using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IWeapon {
	List<BaseStat> Stats {get; set; }
	int CurrentDamage {get; set; }
	void PerformAttack(int damage);
	void PerformSpecialAttack();
}
