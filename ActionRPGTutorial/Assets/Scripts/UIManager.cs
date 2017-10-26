using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public Slider healthBar;
	public Text HPText;
	public Text levelText;
	public PlayerHealthManager playerHealth;
	private PlayerStats thePS;
	private static bool UIExists;

	// Use this for initialization
	void Start () {

		if(!UIExists)
		{
			UIExists = true;
			DontDestroyOnLoad(transform.gameObject);
		} else {
			Destroy (gameObject);
		}

		thePS = GetComponent<PlayerStats> ();
	}
	
	// Update is called once per frame
	void Update () {
		healthBar.maxValue = playerHealth.playerMaxHealth;
		healthBar.value = playerHealth.playerCurrentHealth;
		HPText.text = "HP: " + playerHealth.playerCurrentHealth + "/" + playerHealth.playerMaxHealth;
		levelText.text = "Level: " + thePS.currentLevel;

		if (healthBar.value <= 0) {
		
			HPText.text = "HP: 0 / 100";

		}
	}
}
