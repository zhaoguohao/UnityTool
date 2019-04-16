using UnityEditor;
using UnityEngine;

public class EditorWindowTemolate : EditorWindow
{
    string m_Lable = "";
    float m_SliderValue = 0;
    Color m_color;
    AnimationCurve m_anim;
    bool m_toggleValue1, m_toggleValue2, m_toggleValue3, m_toggleValue4;
    int m_selectedIndex;

    [MenuItem("Temolate/EditorWindowTemolate", false, 0)]
    public static void AddWindow()
    {
        var window = EditorWindow.GetWindow<EditorWindowTemolate>(true, "EditorWindowTemolate");
        window.minSize = new Vector2(800, 400);
        window.ShowUtility();
    }

    private void OnGUI()
    {
        //文本
        m_Lable = EditorGUILayout.TextField("Lable", m_Lable);
        EditorGUILayout.TextField("Lable");
        //滑动条
        m_SliderValue = EditorGUILayout.Slider(m_SliderValue, 0, 10);
        //颜色
        m_color = EditorGUILayout.ColorField(m_color);
        //动画曲线
        m_anim = EditorGUILayout.CurveField(m_anim);

        //按钮
        if (GUILayout.Button("按钮"))
        {

        }
        //下拉框
        m_selectedIndex = EditorGUILayout.Popup(m_selectedIndex, new string[] { "红", "橙", "黄", "绿", "青", "蓝", "紫", });
        EditorGUILayout.BeginToggleGroup("多选", true);
        //选择框                                                                          
        m_toggleValue1 = EditorGUILayout.Toggle("红", m_toggleValue1);
        m_toggleValue2 = EditorGUILayout.Toggle("橙", m_toggleValue2);
        m_toggleValue3 = EditorGUILayout.Toggle("黄", m_toggleValue3);
        m_toggleValue4 = EditorGUILayout.Toggle("绿", m_toggleValue4);
        EditorGUILayout.EndToggleGroup();
    }
}