using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FixedJoystick))]
public class FixedJoystickEditor : JoystickEditor
{
    private SerializedProperty output;

    protected override void OnEnable()
    {
        base.OnEnable();
        output = serializedObject.FindProperty("Output");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (background != null)
        {
            RectTransform backgroundRect = (RectTransform)background.objectReferenceValue;
            backgroundRect.anchorMax = Vector2.zero;
            backgroundRect.anchorMin = Vector2.zero;
            backgroundRect.pivot = center;
        }
    }

    protected override void DrawComponents()
    {
        base.DrawComponents();
        EditorGUILayout.ObjectField(output, new GUIContent("Output", "Output"));
    }
}