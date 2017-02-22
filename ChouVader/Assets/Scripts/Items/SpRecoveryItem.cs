using UnityEngine;
using System.Collections;

public class SpRecoveryItem : Item {
	public int addSp = 10;

	public override void effect(Player player){
		player.sp += addSp;
		if (player.sp > player.sp_max) {
			player.sp = player.sp_max;
		}
	}
}
