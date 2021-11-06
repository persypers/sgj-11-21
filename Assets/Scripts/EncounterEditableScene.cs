using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fancy;

[ExecuteInEditMode]
public class EncounterEditableScene : MonoSingleton<EncounterEditableScene>
{
	public string predicatePrefabFolder = "Assets/Scene/Encounters";
	public GameObject predicatePrefabTemplate;

	public EncounterView view;
}
