using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(RocketController))]
public class RocketEditor : Editor {

	public override void OnInspectorGUI()
	{
		RocketController rc = (RocketController) target;
		DrawDefaultInspector();
		GUILayout.Label("VelcityAngle: "+rc.VelocityAngle);
	}
}
