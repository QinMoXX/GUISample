using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CropPicture))]
public class CropInspector:Editor
{
    private CropPicture cropPicture;
    private int segment;
    private float radiusRatio;
    private float shrink;
    private float height;
    private float top;
    private float bottom;
    public void OnEnable()
    {
        cropPicture = (CropPicture)target;
        segment = cropPicture.segment;
        radiusRatio = cropPicture.radiusRatio;
        height = cropPicture.height;
        top = cropPicture.top;
        bottom = cropPicture.bottom;
    }
    public override void OnInspectorGUI()
    {
        // base.OnInspectorGUI();
        EditorGUI.BeginChangeCheck();
        EditorGUILayout.BeginVertical();
        EditorGUILayout.Space();
        EditorGUI.BeginChangeCheck();
        Texture texture = EditorGUILayout.ObjectField("Texture",cropPicture.texture,typeof(Texture),true) as Texture;
        CropPicture.Shape shape = (CropPicture.Shape)EditorGUILayout.EnumPopup("Shape",cropPicture.shape);
        if (shape == CropPicture.Shape.Circle)
        {
            segment = EditorGUILayout.IntSlider("Segment",cropPicture.segment,3, 100);
            radiusRatio = EditorGUILayout.Slider("Radius Ratio",cropPicture.radiusRatio,0.1f,1f);
        }
        else if (shape == CropPicture.Shape.Starlike)
        {
            segment = EditorGUILayout.IntSlider("Segment",cropPicture.segment,3, 100);
            radiusRatio = EditorGUILayout.Slider("Radius Ratio",cropPicture.radiusRatio,0.1f,1f);
            shrink = EditorGUILayout.Slider("Shrink",cropPicture.shrink,0.1f,1f);
        }else if (shape == CropPicture.Shape.Trapezium)
        {
            height = EditorGUILayout.Slider("Height",cropPicture.height,0f,1f);
            top = EditorGUILayout.Slider("Top",cropPicture.top,0f,1f);
            bottom = EditorGUILayout.Slider("Bottom",cropPicture.bottom,0f,1f);
        }
     
       
        EditorGUILayout.Space();
        EditorGUILayout.EndHorizontal();
        
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Segment");
            cropPicture.texture = texture;
            cropPicture.shape = shape;
            cropPicture.segment = segment;
            cropPicture.radiusRatio = radiusRatio;
            cropPicture.shrink = shrink;
            cropPicture.height = height;
            cropPicture.top = top;
            cropPicture.bottom = bottom;
        }
    }
}
