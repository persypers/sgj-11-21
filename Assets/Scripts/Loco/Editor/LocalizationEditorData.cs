using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[FilePath("Scripts/Loco/Editor/LocoData.asset", FilePathAttribute.Location.ProjectFolder)]
public class LocalizationEditorData : ScriptableSingleton<LocalizationEditorData>
{
	public Localization localization;
}
