using UnityEngine;
using UnityEngine.UI;

public class CropPicture : RawImage
{
    public enum Shape
    {
        Circle,
        Starlike,
        Trapezium
    }
    
    public Shape shape = Shape.Circle;
    public int segment = 100;
    public float radiusRatio  = 1f;
    public float shrink = 0.0f;

    public float height = 0.5f;
    public float top = 0.5f;
    public float bottom = 0.5f;
    private float m_width;
    private float m_height;
    private Vector2 m_originPos;
    private Vector2 m_convertRatio;
    private Vector2 m_uvCenter;

    protected override void OnPopulateMesh(VertexHelper toFill)
    {
        // base.OnPopulateMesh(toFill);
        toFill.Clear();
        m_width = rectTransform.rect.width;
        m_height = rectTransform.rect.height;
        
        Vector2 uv = new Vector2(1, 1);
        m_convertRatio = new Vector2(uv.x / m_width, uv.y / m_height);
        m_uvCenter = new Vector2(0.5f, 0.5f);
        m_originPos = new Vector2(m_width * (0.5f - rectTransform.pivot.x), m_height * (0.5f - rectTransform.pivot.y));
        
        UIVertex origin = new UIVertex();
        origin.position = m_originPos;
        origin.uv0 = new Vector2(m_uvCenter.x,  m_uvCenter.y);
        origin.color = new Color32(255, 255, 255, 255);
        toFill.AddVert(origin);
        
        if (shape == Shape.Circle)
        {
            Circle(toFill);
        }else if (shape == Shape.Starlike)
        {
            Starlike(toFill);
        }else if (shape == Shape.Trapezium)
        {
            Trapezium(toFill);
        }

        for (int i = 1; i < toFill.currentVertCount-1; i++)
        {
            toFill.AddTriangle(i,0,i+1);
        }
    }

    private void Trapezium(VertexHelper toFill)
    {
        Vector2 xy = new  Vector2(m_width / 2 * top, m_height/2 * height) ;
        UIVertex uvTemp = new UIVertex();
        uvTemp.position = xy + m_originPos;
        uvTemp.uv0 = new Vector2(xy.x * m_convertRatio.x + m_uvCenter.x, xy.y * m_convertRatio.y + m_uvCenter.y);
        uvTemp.color = new Color32(255, 255, 255, 255);
        toFill.AddVert(uvTemp);
        uvTemp = new UIVertex();
        xy.x *= -1;
        uvTemp.position = xy + m_originPos;
        uvTemp.uv0 = new Vector2(xy.x * m_convertRatio.x + m_uvCenter.x, xy.y * m_convertRatio.y + m_uvCenter.y);
        uvTemp.color = new Color32(255, 255, 255, 255);
        toFill.AddVert(uvTemp);
        xy = new  Vector2(-m_width / 2 * bottom, -m_height/2 * height) ;
        uvTemp = new UIVertex();
        uvTemp.position = xy + m_originPos;
        uvTemp.uv0 = new Vector2(xy.x * m_convertRatio.x + m_uvCenter.x, xy.y * m_convertRatio.y + m_uvCenter.y);
        uvTemp.color = new Color32(255, 255, 255, 255);
        toFill.AddVert(uvTemp);
        xy.x *= -1;
        uvTemp = new UIVertex();
        uvTemp.position = xy + m_originPos;
        uvTemp.uv0 = new Vector2(xy.x * m_convertRatio.x + m_uvCenter.x, xy.y * m_convertRatio.y + m_uvCenter.y);
        uvTemp.color = new Color32(255, 255, 255, 255);
        toFill.AddVert(uvTemp);
        xy = new  Vector2(m_width / 2 * top, m_height/2 * height) ;
        uvTemp = new UIVertex();
        uvTemp.position = xy + m_originPos;
        uvTemp.uv0 = new Vector2(xy.x * m_convertRatio.x + m_uvCenter.x, xy.y * m_convertRatio.y + m_uvCenter.y);
        uvTemp.color = new Color32(255, 255, 255, 255);
        toFill.AddVert(uvTemp);
    }

    private void Circle(VertexHelper toFill)
    {
        float radian = Mathf.PI * 2 / segment;
        float curRadian = 0;
        float radius = m_width * 0.5f;
        for(int i = 0; i < segment + 1; i++)
        {
            float x = Mathf.Cos(curRadian) * radius * radiusRatio;
            float y = Mathf.Sin(curRadian) * radius * radiusRatio;
            curRadian += radian;
            Vector2 xy = new Vector2(x, y);
            UIVertex uvTemp = new UIVertex();
            uvTemp.position = xy + m_originPos;
            uvTemp.uv0 = new Vector2(xy.x * m_convertRatio.x + m_uvCenter.x, xy.y * m_convertRatio.y + m_uvCenter.y);
            uvTemp.color = new Color32(255, 255, 255, 255);
            toFill.AddVert(uvTemp);
            
        }
    }
    
    
    private void Starlike(VertexHelper toFill)
    {
        float radian = Mathf.PI * 2 / segment;
        float curRadian = 0;
        float radius = m_width * 0.5f;
        for(int i = 0; i < segment + 1; i++)
        {
            float x = Mathf.Cos(curRadian) * radius * radiusRatio * ( i % 2 == 1 ? shrink:1);
            float y = Mathf.Sin(curRadian) * radius * radiusRatio * ( i % 2 == 1 ? shrink:1);
            curRadian += radian;
            Vector2 xy = new Vector2(x, y);
            UIVertex uvTemp = new UIVertex();
            uvTemp.position = xy + m_originPos;
            uvTemp.uv0 = new Vector2(xy.x * m_convertRatio.x + m_uvCenter.x, xy.y * m_convertRatio.y + m_uvCenter.y);
            uvTemp.color = new Color32(255, 255, 255, 255);
            toFill.AddVert(uvTemp);
            
        }
    }
}
