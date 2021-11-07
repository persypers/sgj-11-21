using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fancy;

public class WinState : GameState
{
	public string text = "Оправдан\nВ этот раз тебе повезло.";
	TMPro.TMP_Text label;
	protected override void OnEnable()
	{
		label = Global.Instance.barkLabel.GetComponent<TMPro.TMP_Text>();
		label.text = text;
		label.gameObject.Show();

		Global.Instance.gameUi.Hide();

		Ben.Instance.SetTarget(Ben.Instance.winAnchor);
		Ben.Instance.isWalking = true;

		base.OnEnable();
	}

	protected override void OnDisable()
	{
		label.gameObject.Hide();
		base.OnDisable();
	}

	protected override void Update()
	{
		base.Update();
	}
}
