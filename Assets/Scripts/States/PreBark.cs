using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fancy;

public class PreBark : GameState
{
	float t;
	TMPro.TMP_Text label;
	protected override void OnEnable()
	{
		Global.Instance.PopNextEncounter();

		base.OnEnable();
		var go = Global.Instance.barkLabel;
		go.Show();
		label = go.GetComponent<TMPro.TMP_Text>();
		label.text = Global.Instance.nextEncounter.preBark;
		label.color = Global.Instance.config.enemyBarkColor;
		t = 0.0f;
	}

	protected override void OnDisable()
	{
		base.OnDisable();
	}


	protected override void Update()
	{
		base.Update();
		t += Time.deltaTime;
		if(t >= Global.Instance.config.minBarkTime)
		{
			GameState.SwitchState<ApproachState>();
		}
	}
}
