using UnityEngine;
using System.Collections;

public class HpRecoveryItem : Item {
	public int addHp = 10;

	public override void effect(Player player){
		player.hp += addHp;
		if (player.hp > player.hp_max) {
			player.hp = player.hp_max;
		}
	}
}
