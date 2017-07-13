using UnityEngine;
using System.Collections;

public class move : MonoBehaviour {
    Rigidbody2D r2;

    Vector3 Direction ;
    public float x, y=0;

    Vector3 destination;
    float[] values = new float[] { 3f, -3f, 2f, -2f, 1.5f, -1.5f };


    private float loc_pos_Z;
	// Use this for initialization
	void Start () {
        destination = transform.position;
        r2 = gameObject.GetComponent < Rigidbody2D>() ;
        x = -.5f;
        Direction = new Vector3(x, y, 0);
        loc_pos_Z = gameObject.transform.position.z;
	}
    public GameObject raycastObject;
    // Update is called once per frame
    void Update()
    {
      gameObject.transform.Rotate(Vector3.up * Time.deltaTime*100);
    //    gameObject.transform.Rotate(Vector3.right * Time.deltaTime*100);



        RaycastHit objectHit;

        Vector3 fwd = raycastObject.transform.TransformDirection(-Vector3.up);
        Debug.DrawRay(raycastObject.transform.position, fwd * 50, Color.green);
        if (Physics.Raycast(raycastObject.transform.position, fwd, out objectHit, 100))
        {

            Debug.Log("RayCast Stay");
             r2.gameObject.transform.position += Direction * Time.deltaTime;
        
          
          
        }
        else
        {
            if(!destroy_called)    StartCoroutine(_destroy());
            r2.gameObject.transform.position -= Direction * Time.deltaTime;
           x =values[ Random.Range(0,values.Length)];
            y = values[Random.Range(0, values.Length)];
            Direction = new Vector3(x, y, 0);
        }





    }

    public bool destroy_called = false;
    public static bool smash=false;

    void OnCollisionEnter2D(Collision2D coll)
    {


        Handheld.Vibrate();
        // Destroy(coll.gameObject);
        Canvas_controller.life_num--;
        // r2.gameObject.transform.position -= Direction * Time.deltaTime;
        Vector3 g = GameObject.FindObjectOfType<Size>().gameObject.GetComponent<Renderer>().bounds.center;
        gameObject.transform.position =new Vector3( g.x,g.y,loc_pos_Z);
        MeshSplitting.Examples.MobileLineSplitter.interrrupted = true;
        smash = true;

        Debug.Log("smash"+smash);

        StartCoroutine(sma());

    }

    IEnumerator sma()
    {
        yield return new WaitForSeconds(1);
        smash = false;
    }

    IEnumerator _destroy()
    {
        Debug.Log("_destroy");
        destroy_called = true;
        yield return new WaitForSeconds(3);

        RaycastHit objectHit;

        Vector3 fwd = raycastObject.transform.TransformDirection(-Vector3.up);

        if (!Physics.Raycast(raycastObject.transform.position, fwd, out objectHit, 50))
        {
            Vector3 g = GameObject.FindObjectOfType<Size>().gameObject.GetComponent<Renderer>().bounds.center;
            gameObject.transform.position = new Vector3(g.x, g.y, loc_pos_Z);
            destroy_called = false;
        }
        else
        {
            destroy_called = false;
        }
    }
}
