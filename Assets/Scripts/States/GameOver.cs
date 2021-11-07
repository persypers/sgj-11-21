using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fancy;

public class GameOver : GameState
{
	public string text = "Поражение";
	TMPro.TMP_Text label;
	protected override void OnEnable()
	{
		label = Global.Instance.barkLabel.GetComponent<TMPro.TMP_Text>();
		label.text = text;
		base.OnEnable();
		StartCoroutine(StateRoutine());
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

	public IEnumerator StateRoutine()
	{
		Ben.Instance.isWalking = false;
		Ben.Instance.die = true;
		yield return new WaitForSeconds(1f);

		label.gameObject.Show();
		yield return new WaitForSeconds(1f);
		Ben.Instance.SetTarget(Ben.Instance.appearAnchor);
		Global.Instance.gameUi.Hide();
		label.gameObject.Hide();
		while(!Ben.Instance.IsTargetReached())
		{
			yield return null;
		}

		GameState.SwitchState<IntroWalk>();
	}
}
