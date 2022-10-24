
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

[CustomEditor(typeof(PowerUpScript))]
public class PowerupInspector : Editor
{
    
    SerializedProperty powerUpIndex;


    private void OnEnable()
    {
        powerUpIndex = serializedObject.FindProperty("powerUpIndex");
    }


    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        serializedObject.Update();



        powerUpIndex.intValue = EditorGUILayout.Popup("Power up:", powerUpIndex.intValue, PowerUp.names);


        serializedObject.ApplyModifiedProperties();
    }

}
