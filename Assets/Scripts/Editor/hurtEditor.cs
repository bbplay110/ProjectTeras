using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityEngine.UI;
[CustomEditor(typeof(hurt))]
public class hurtEditor : Editor {
    hurt m_Target;
    private string Tag;
    private bool ExtraShild;
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        m_Target = (hurt)target;
        Tag = m_Target.tag;
        // Toggle(標題, 預設值)，勾選框元件
        ExtraShild = EditorGUILayout.Toggle("HasExtraShild", m_Target.HasExtraShild);
        m_Target.HasExtraShild = ExtraShild;
        // 從 BeginDisabledGroup(Boolean) 到 EndDisabledGroup() 中間的範圍是否可以被選取
        // 取決於 BeginDisabledGroup 傳入的布林參數

        EditorGUI.BeginDisabledGroup(Tag == "Player");
        //一個可以拉東西進去的窗,用法:EditorGUILayout.ObjectField("Label",實際去控制的物件,typeof(物件類型),是否允許指定Scene對象)as GameObject
        m_Target.Win = EditorGUILayout.ObjectField("勝利畫面", m_Target.Win, typeof(GameObject), true) as GameObject;
        EditorGUI.EndDisabledGroup();
        EditorGUI.BeginDisabledGroup(ExtraShild == false);
        m_Target.TotalExtraShild = EditorGUILayout.FloatField("護盾總量", m_Target.TotalExtraShild);
        m_Target.ExtraShildRecover = EditorGUILayout.FloatField("護盾回復量", m_Target.ExtraShildRecover);

        m_Target.ExtraShildRecoverTime = EditorGUILayout.FloatField("護盾回復時間", m_Target.ExtraShildRecoverTime);
        m_Target.ExtraShiledBar = EditorGUILayout.ObjectField("護盾血條", m_Target.ExtraShiledBar, typeof(Image), true) as Image;
        EditorGUI.EndDisabledGroup();

        EditorGUI.BeginDisabledGroup(Tag != "Player");
        m_Target.hurtArrow = EditorGUILayout.ObjectField("HurtArrow", m_Target.hurtArrow, typeof(GameObject), true) as GameObject;
       // m_Target.Lose=EditorGUILayout.Fie
        m_Target.Lose = EditorGUILayout.ObjectField("死亡畫面", m_Target.Lose, typeof(GameObject), true) as GameObject;
        EditorGUI.EndDisabledGroup();
    }

}
