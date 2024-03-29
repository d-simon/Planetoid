/// <Licensing>
/// � 2011 (Copyright) Path-o-logical Games, LLC
/// If purchased from the Unity Asset Store, the following license is superseded 
/// by the Asset Store license.
/// Licensed under the Unity Asset Package Product License (the "License");
/// You may not use this file except in compliance with the License.
/// You may obtain a copy of the License at: http://licensing.path-o-logical.com
/// </Licensing>
using UnityEditor;
using UnityEngine;
using System.Collections;


namespace PathologicalGames
{

    [CustomEditor(typeof(TurntableConstraint))]
    public class TurntableConstraintInspector : ConstraintFrameworkBaseInspector
    {
        protected override void OnInspectorGUIUpdate()
        {
            base.OnInspectorGUIUpdate();

            var script = (TurntableConstraint)target;

            script.speed = EditorGUILayout.FloatField("Speed", script.speed);
            script.randomStart = EditorGUILayout.Toggle("Random Start", script.randomStart);

        }
    }
}