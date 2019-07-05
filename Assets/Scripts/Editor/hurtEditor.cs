using UnityEngine;
using UnityEditor;
using System.Collections;
[CustomEditor(typeof(hurt))]
public class hurtEditor : Editor {
    hurt m_Target;
    private string Tag;
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        m_Target = (hurt)target;

    }

}
