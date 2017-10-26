using UnityEngine;
using System.Collections;
using System;

public class Slime : MonoBehaviour, IEnemy {

	public LayerMask aggroLayerMask;
	public float currentHealth;
	public float maxHealth;
	public int Experience { get; set; }

	private Player player;
	private UnityEngine.AI.NavMeshAgent navAgent;
	private CharacterStats characterStats;
	private Collider[] withinAggroColliders;

	void Start()
	{
		Experience = 250;
		navAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		characterStats = new CharacterStats(6, 10, 2);
		currentHealth = maxHealth;

	}

	void FixedUpdate()
	{
		withinAggroColliders = Physics.OverlapSphere(transform.position, 10, aggroLayerMask);
		if(withinAggroColliders.Length > 0) {
		
			ChasePlayer(withinAggroColliders[0].GetComponent<Player>());

		}
	}

	public void PerformAttack()
	{
		player.TakeDamage(5);
	}


	public void TakeDamage(int amount)
	{
		currentHealth -= amount;
		if(currentHealth <= 0) {
			Die();
		}
	}

	void ChasePlayer(Player player) {
		navAgent.SetDestination(player.transform.position);
		this.player = player;
		if(navAgent.remainingDistance <= navAgent.stoppingDistance) {
			if(!IsInvoking("PerformAttack")) {
				InvokeRepeating("PerformAttack", .5f, 2f);
			}
		} else {
			CancelInvoke("PerformAttack");
		}
	}

	public void Die()
	{
		CombatEvents.EnemyDied(this);
		Destroy(gameObject);
	}
}
