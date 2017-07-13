using UnityEngine;
using System.Collections;

public class Size : MonoBehaviour {

    public static double Amount;
   public int tris;
    public float siz;
    public float pri_Siz;
    // Use this for initialization
    void Start () {

        pri_Siz = gameObject.GetComponent<Collider>().bounds.size.sqrMagnitude;

    }
    public  Size other;
	// Update is called once per frame
	void Update () {
        tris = gameObject.GetComponent<MeshFilter>().mesh.triangles.Length;
        siz = gameObject.GetComponent<Collider>().bounds.size.sqrMagnitude;

       Size[] others = GameObject.FindObjectsOfType<Size>();
        if(others[0].tris!=gameObject.GetComponent<Size>().tris)
            {
            other = others[0];
        }else
        {
            if(others.Length>=2)
            other = others[1];
        }
        if (other != null)
        {
            if (other.tris < gameObject.GetComponent<Size>().tris)
            {
                Destroy(other.gameObject);
                //Debug.Log("%%%%" + (tris * 100) / 600);
                var loc_siz = (siz * 100) / pri_Siz;


                var loc = (tris * 100) / 600;
                if (loc >= 100)  loc = 100 - (loc - 100);
                if (loc > Canvas_controller.per) loc = loc - (loc - Canvas_controller.per);
                if (loc == Canvas_controller.per) loc = loc - 1;

                Canvas_controller.per = loc;
                var pppp= gameObject.GetComponent<BoundCalculation>().CalcScreenPercentage() * BoundCalculation.multiplier;
                if (loc > 70)
                {
                    Amount = (100 - (loc * pppp / 100));
                    //   Canvas_controller.per_String = "" + loc + "pp:" + pppp + "\n Mul" + loc * pppp / 100
                    Canvas_controller.per_String = "" +Amount.ToString("F2");
                }
                else
                {
                    // Canvas_controller.per_String = "" + loc + "pp:" + pppp + "\n Mul" + 80 * pppp / 100;
                    Amount = (100 - (80 * pppp / 100));
                    Canvas_controller.per_String = "" +Amount.ToString("F2");
                }
                }
        }
       
    }
}
