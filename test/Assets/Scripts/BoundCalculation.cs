using UnityEngine;
using System.Collections;

public class BoundCalculation : MonoBehaviour
{
    public static double perc;
    public   double pre_Perc;

    public static double multiplier;
 public static   BoundCalculation instance;
    void Awake()
    {
        instance = this;
    }
    
  
  
    void Start()
    {

        float height = Camera.main.orthographicSize * 2.0f;
        float width = height * Screen.width / Screen.height;
        transform.localScale =new Vector3(width/10.9f,.1f, height/11.2f);
    }

    void Update()
    {
        if (pre_Perc==0)
        {
            pre_Perc = CalcScreenPercentage();
        }
        else
        {
            multiplier = 100 / pre_Perc;
        }
    }

  public  double CalcScreenPercentage()
    {
       

        var minX = Mathf.Infinity;
        var minY = Mathf.Infinity;
        var maxX = -Mathf.Infinity;
        var maxY = -Mathf.Infinity;

        var bounds = GetComponent< MeshFilter > ().mesh.bounds;
        var v3Center = bounds.center;
        var v3Extents = bounds.extents;

        Vector3[] corners   = new Vector3[8];

        corners[0] = new Vector3(v3Center.x - v3Extents.x, v3Center.y + v3Extents.y, v3Center.z - v3Extents.z);  // Front top left corner
        corners[1] = new Vector3(v3Center.x + v3Extents.x, v3Center.y + v3Extents.y, v3Center.z - v3Extents.z);  // Front top right corner
        corners[2] = new Vector3(v3Center.x - v3Extents.x, v3Center.y - v3Extents.y, v3Center.z - v3Extents.z);  // Front bottom left corner
        corners[3] = new Vector3(v3Center.x + v3Extents.x, v3Center.y - v3Extents.y, v3Center.z - v3Extents.z);  // Front bottom right corner
        corners[4] = new Vector3(v3Center.x - v3Extents.x, v3Center.y + v3Extents.y, v3Center.z + v3Extents.z);  // Back top left corner
        corners[5] = new Vector3(v3Center.x + v3Extents.x, v3Center.y + v3Extents.y, v3Center.z + v3Extents.z);  // Back top right corner
        corners[6] = new Vector3(v3Center.x - v3Extents.x, v3Center.y - v3Extents.y, v3Center.z + v3Extents.z);  // Back bottom left corner
        corners[7] = new Vector3(v3Center.x + v3Extents.x, v3Center.y - v3Extents.y, v3Center.z + v3Extents.z);  // Back bottom right corner

        for (var i = 0; i < corners.Length; i++)
        {
            var corner = transform.TransformPoint(corners[i]);
            corner = Camera.main.WorldToScreenPoint(corner);
            if (corner.x > maxX) maxX = corner.x;
            if (corner.x < minX) minX = corner.x;
            if (corner.y > maxY) maxY = corner.y;
            if (corner.y < minY) minY = corner.y;
            minX = Mathf.Clamp(minX, 0, Screen.width);
            maxX = Mathf.Clamp(maxX, 0, Screen.width);
            minY = Mathf.Clamp(minY, 0, Screen.height);
            maxY = Mathf.Clamp(maxY, 0, Screen.height);
        }

        var width = maxX - minX;
        var height = maxY - minY;
        var area = width * height;
        var percentage = area / (Screen.width * Screen.height) * 100.0;
        return percentage ;
    }
}