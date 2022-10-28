
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;
using Unity.VisualScripting;

[CustomEditor(typeof(PowerUp))]
public class PowerupInspector : Editor
{
    
    SerializedProperty method;
    SerializedProperty sprite;

    private void OnEnable()
    {
        method = serializedObject.FindProperty("methodIndex");
        sprite = serializedObject.FindProperty("sprite");
    }


    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        serializedObject.Update();

        EditorGUILayout.PropertyField(sprite);
        method.intValue = EditorGUILayout.Popup("Power up method:", method.intValue, PowerUp.methodNames);
        
        serializedObject.ApplyModifiedProperties();
    }

}
