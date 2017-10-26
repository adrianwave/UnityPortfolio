using UnityEngine;
using System.Collections;

public class PlayerWeaponController : MonoBehaviour {
	public GameObject playerHand;
	public GameObject EquippedWeapon { get; set; }

	Transform spawnProjectile;
	IWeapon equippedWeapon;
	Item currentEquipedItem;
	CharacterStats characterStats;

	void Start()
	{
		spawnProjectile = transform.Find("ProjectileSpawn");
		characterStats = GetComponent<Player>().characterStats;
	}

	public void EquipWeapon(Item itemToEquip)
	{
		if (EquippedWeapon != null)
		{
			UnequipWeapon();
		}


		EquippedWeapon = (GameObject)Instantiate(Resources.Load<GameObject>("Weapons/" + itemToEquip.ObjectSlug), 
			playerHand.transform.position, playerHand.transform.rotation);
		equippedWeapon = EquippedWeapon.GetComponent<IWeapon>();
		if (EquippedWeapon.GetComponent<IProjectileWeapon>() != null)
			EquippedWeapon.GetComponent<IProjectileWeapon>().ProjectileSpawn = spawnProjectile;

		EquippedWeapon.transform.SetParent(playerHand.transform);
		equippedWeapon.Stats = itemToEquip.Stats;
		currentEquipedItem = itemToEquip;
		characterStats.AddStatBonus(itemToEquip.Stats);
		UIEventHandler.ItemEquipped(itemToEquip);
		UIEventHandler.StatsChanged();
	}

	public void UnequipWeapon()
	{
		if(currentEquipedItem.ObjectSlug != null){
			InventoryController.instance.GiveItem(currentEquipedItem.ObjectSlug);
			Debug.Log(currentEquipedItem.ObjectSlug + " given.");
		}
		characterStats.RemoveStatBonus(equippedWeapon.Stats);
		Destroy(EquippedWeapon.transform.gameObject);
		UIEventHandler.StatsChanged();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.X))
			PerformWeaponAttack();
		if (Input.GetKeyDown(KeyCode.Z))
			PerformWeaponSpecialAttack();
	}

	public void PerformWeaponAttack()
	{
		equippedWeapon.PerformAttack(calculateDamage());
	}
	public void PerformWeaponSpecialAttack()
	{
		equippedWeapon.PerformSpecialAttack();
	}

	private int calculateDamage() {
		int damageToDeal = (characterStats.GetStat(BaseStat.BaseStatType.Power).GetCalculatedStatValue() * 2) + Random.Range(2, 8);
		if(calculateCrit(damageToDeal) > 0) {
		
			damageToDeal += calculateCrit(damageToDeal);
		} else {
			damageToDeal += calculateCrit(damageToDeal);
		}
		//damageToDeal += calculateCrit(damageToDeal);
		return damageToDeal;
	}

	private int calculateCrit(int damage) {

		if(Random.value <= .30f){
			int critDamage = (int)(damage * Random.Range (.5f, .75f));
			return critDamage;
		}

		return 0;
	}
}