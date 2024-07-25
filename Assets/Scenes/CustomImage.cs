using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UI;

public class CustomImage : Graphic
{
    protected override void OnPopulateMesh(VertexHelper toFill)
    {
        base.OnPopulateMesh(toFill);
        toFill.Clear();
 
        toFill.Clear();
        toFill.AddVert(new Vector3(0, 0), Color.black, Vector2.zero);
        toFill.AddVert(new Vector3(0, 100), Color.black, Vector2.zero);
        toFill.AddVert(new Vector3(100, 0), Color.black, Vector2.zero);
 
        toFill.AddTriangle(0, 1, 2);
    }
    
    

}
