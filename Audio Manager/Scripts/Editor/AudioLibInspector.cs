using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AudioLibrary)), CanEditMultipleObjects]
public class AudioLibInspector : Editor
{
    //AudioLibrary _target;

    SerializedProperty _playOnStart;
    SerializedProperty _nameOrIndex;

    SerializedProperty _name;
    SerializedProperty _index;

    void OnEnable()
    {
        _playOnStart = serializedObject.FindProperty("playOnStart");
        _nameOrIndex = serializedObject.FindProperty("nameOrIndex");

        _name = serializedObject.FindProperty("awakeSoundName");
        _index = serializedObject.FindProperty("awakeSoundIndex");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        #region Play On Start
        EditorGUI.BeginChangeCheck();

        EditorGUI.showMixedValue = _playOnStart.hasMultipleDifferentValues;
        AudioLibrary.PlayOnStart pos = (AudioLibrary.PlayOnStart)EditorGUILayout.EnumPopup("Play On Start", (AudioLibrary.PlayOnStart)_playOnStart.enumValueIndex);
        EditorGUI.showMixedValue = false;

        if (EditorGUI.EndChangeCheck())
        {
            _playOnStart.enumValueIndex = (int)pos;
            serializedObject.ApplyModifiedProperties();
        }
        #endregion

        if (!_playOnStart.hasMultipleDifferentValues && pos != AudioLibrary.PlayOnStart.None)
        {
            #region Name Or Index
            EditorGUI.BeginChangeCheck();

            EditorGUI.showMixedValue = _nameOrIndex.hasMultipleDifferentValues;
            AudioLibrary.PlayType noi = (AudioLibrary.PlayType)EditorGUILayout.EnumPopup("Name Or Index", (AudioLibrary.PlayType)_nameOrIndex.enumValueIndex);
            EditorGUI.showMixedValue = false;

            if (EditorGUI.EndChangeCheck())
            {
                _nameOrIndex.enumValueIndex = (int)noi;
                serializedObject.ApplyModifiedProperties();
            }
            #endregion

            if (!_nameOrIndex.hasMultipleDifferentValues && noi != AudioLibrary.PlayType.PlayAll)
            {
                if (noi == AudioLibrary.PlayType.Name)
                {
                    EditorGUI.BeginChangeCheck();

                    EditorGUI.showMixedValue = _name.hasMultipleDifferentValues;
                    string nm = EditorGUILayout.TextField("Name", _name.stringValue);
                    EditorGUI.showMixedValue = false;

                    if (EditorGUI.EndChangeCheck())
                    {
                        _name.stringValue = nm;
                        serializedObject.ApplyModifiedProperties();
                    }
                }
                else if (noi == AudioLibrary.PlayType.Index)
                {
                    EditorGUI.BeginChangeCheck();

                    EditorGUI.showMixedValue = _index.hasMultipleDifferentValues;
                    int ind = EditorGUILayout.IntField("Index", _index.intValue);
                    EditorGUI.showMixedValue = false;

                    if (EditorGUI.EndChangeCheck())
                    {
                        _index.intValue = ind;
                        serializedObject.ApplyModifiedProperties();
                    }
                }
            }
        }

    }
}


/* if (targets.Length == 1)
        {
            AudioLibrary lib = target as AudioLibrary;

            lib.playOnStart = (AudioLibrary.PlayOnStart)EditorGUILayout.EnumPopup("Play On Start", lib.playOnStart);

            if (lib.playOnStart == AudioLibrary.PlayOnStart.PlayOnStart || lib.playOnStart == AudioLibrary.PlayOnStart.PlayOnEnable)
            {
                lib.nameOrIndex = (AudioLibrary.PlayType)EditorGUILayout.EnumPopup("Name Or Index", lib.nameOrIndex);

                if (lib.nameOrIndex == AudioLibrary.PlayType.Name)
                    lib.awakeSoundName = EditorGUILayout.TextField("Name", lib.awakeSoundName);
                else if (lib.nameOrIndex == AudioLibrary.PlayType.Index)
                    lib.awakeSoundIndex = EditorGUILayout.IntField("Index", lib.awakeSoundIndex);
            }
        }
    */
