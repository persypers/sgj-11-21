using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fancy;

public class PreBark : GameState
{
	float t;
	protected override void OnEnable()
	{
		Global.Instance.PopNextEncounter();

		base.OnEnable();
		var go = Global.Instance.barkLabel;
		go.Show();
		go.GetComponent<TMPro.TMP_Text>().text = Global.Instance.nextEncounter.preBark;
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
