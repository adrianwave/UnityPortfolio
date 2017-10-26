using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {

	public int currentLevel;

	public int currentExp;

	public int[] toLevelUp;

	private PlayerHealthManager thePlayerHealth;

	public int[] HPLevels;

	public int[] attackLevels;
	public int[] defenseLevels;

	public int currentHP;
	public int currentAttack;
	public int currentDefense;

	// Use this for initialization
	void Start () {

		currentHP = HPLevels [1];
		currentAttack = attackLevels [1];
		currentDefense = defenseLevels [1];
		thePlayerHealth = FindObjectOfType<PlayerHealthManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (currentExp >= toLevelUp [currentLevel]) {

			//currentLevel++;
//			if (currentLevel != 1) {
//				healthUp.playerMaxHealth = healthUp.playerMaxHealth * 2;
//				healthUp.playerCurrentHealth = healthUp.playerMaxHealth;
//			}
		
			LevelUp ();

		}
	}

	public void AddExperience(int experienceToAdd)
	{
		currentExp += experienceToAdd;
	}

	public void LevelUp(){
	
		currentLevel++;
		currentHP = HPLevels [currentLevel];

		thePlayerHealth.playerMaxHealth = currentHP;
		thePlayerHealth.playerCurrentHealth += currentHP - HPLevels [currentLevel - 1];

		currentAttack = attackLevels [currentLevel];
		currentDefense = defenseLevels [currentLevel];
	}
}
