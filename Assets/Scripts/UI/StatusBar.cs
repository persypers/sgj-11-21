using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fancy;

public class StatusBar : MonoBehaviour
{
	public TMPro.TMP_Text sanityLabel;
	public TMPro.TMP_Text innocenceLabel;

	public TMPro.TMP_Text sanityLabelFloat;
	public TMPro.TMP_Text innocenceLabelFloat;
	public Animator sanityIcon;
	public Animator innocenceIcon;
	public Animator floater;

	public void AnimateFloater(int sanity, int innocence)
	{
		sanityLabelFloat.gameObject.SetActive(sanity != 0);
		sanityLabelFloat.text = sanity.ToString();
		innocenceLabelFloat.gameObject.SetActive(innocence != 0);
		innocenceLabelFloat.text = innocence.ToString();
		floater.gameObject.Show();
	}

	public void AnimateIcons(int sanity, int innocence)
	{
		if(sanity != 0)
		{
			sanityIcon.SetTrigger(sanity > 0 ? "Gain" : "Lose");
		}
		if(innocence != 0)
		{
			innocenceIcon.SetTrigger(innocence > 0 ? "Gain" : "Lose");
		}
	}
}
