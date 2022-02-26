using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Motion))]
public class MotionEditor : Editor {

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		Motion mt = (Motion)target;

		if (GUILayout.Button("Test"))
		{
			mt.Test();
		}
	}
}
