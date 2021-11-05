using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(EffectView.EffectIcon))]
public class EffectIconDrawer : SerializableDictionaryPropertyDrawer {}

[CustomPropertyDrawer(typeof(ResourceConfig.PredicateIconDict))]
public class PredicateIconDictDrawer : SerializableDictionaryPropertyDrawer {}
