using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


[CustomEditor(typeof(Erosion))]
public class ErosionEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Erosion myTarget = (Erosion)target;

        myTarget.erosionCycles = EditorGUILayout.IntField("Erosion Cycles", myTarget.erosionCycles);
        myTarget.erosionFactor = EditorGUILayout.FloatField("Erosion Factor", myTarget.erosionFactor);

        if (GUILayout.Button("Erode Height Map"))
        {
            myTarget.ErodeHeightMap();
        }

        myTarget.FrequencyOne = EditorGUILayout.FloatField("Frequency One", myTarget.FrequencyOne);
        myTarget.AmplitudeOne = EditorGUILayout.FloatField("Amplitude One", myTarget.AmplitudeOne);
        myTarget.FrequencyTwo = EditorGUILayout.FloatField("Frequency Two", myTarget.FrequencyTwo);
        myTarget.AmplitudeTwo = EditorGUILayout.FloatField("Amplitude Two", myTarget.AmplitudeTwo);


        if (GUILayout.Button("Create Height Map"))
        {
            myTarget.CreateHeightMap();
        }
    }
}



