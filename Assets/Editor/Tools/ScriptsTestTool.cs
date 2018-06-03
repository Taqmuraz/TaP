using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Linq;
using System.Reflection;
using Behaviours;
using Classes;
using Classes.SaveLoadSystem;
using Behaviours.Alives;

public class ScriptsTestTool : EditorWindow {

	[MenuItem ("Window/Test scripts tool")]
	private static void ShowWindow () {
		EditorWindow.GetWindow<ScriptsTestTool> ();
	}

	private MonoBehaviour script;
	private string methodName;
	private object[] methodParameters;

	public void OnGUI () {
		script = (MonoBehaviour)EditorGUILayout.ObjectField ("Script", script, typeof(MonoBehaviour), true);
		if (script) {
			methodName = EditorGUILayout.TextField ("Method name", methodName);
			if (GUILayout.Button("Commit method")) {
				script.GetType ().GetMethod (methodName).Invoke (script, new object[0]);
			}
		}
	}
}
