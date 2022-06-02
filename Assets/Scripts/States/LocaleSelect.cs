using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fancy;
using UnityEngine.Playables;

public class LocaleSelect : GameState
{
	public GameObject screen;
	protected override void OnEnable()
	{
		base.OnEnable();
		screen.gameObject.SetActive(true);
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		screen.gameObject.SetActive(false);
	}

	protected override void Update()
	{
		base.Update();
	}

	public void Select(string locale)
	{
		Localization.SwitchLocale(locale);
		GameState.SwitchState<IntroState>();
	}
}
