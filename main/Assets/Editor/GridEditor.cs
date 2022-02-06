using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(HexGrid))]
public class CubeEditor : Editor {

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		HexGrid hxg = (HexGrid)target;

		GUILayout.BeginHorizontal();

			if (GUILayout.Button("Generate Grid"))
			{
				hxg.GenerateGrid();
			}

			if (GUILayout.Button("Reset"))
			{
				hxg.Reset();
			}

		GUILayout.EndHorizontal();
	}

}
