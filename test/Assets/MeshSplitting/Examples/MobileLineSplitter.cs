using MeshSplitting.Splitables;
using MeshSplitting.Splitters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeshSplitting.Examples
{
    [AddComponentMenu("Mesh Splitting/Examples/Mobile Line Splitter")]
    [RequireComponent(typeof(Camera))]
    [RequireComponent(typeof(LineRenderer))]
    [RequireComponent(typeof(PolygonCollider2D))]
    public class MobileLineSplitter : MonoBehaviour
    {
        public float CutPlaneDistance = 5f;
        public float CutPlaneSize = 10f;
        public float MinSplitDistance = 20f;

        private LineRenderer _lineRenderer;
        private Camera _camera;
        private Transform _transform;

        private bool _hasStartPos = false;
        private Vector3 _startPos;
        private Vector3 _endPos;

        public Vector2 View = new Vector2(0f, 0f);
        public float Distance = 5f;
        public Vector3 Target = Vector3.up;
        public float ForcePush = 1f;

        public GUISkin GuiSkin;
        public Texture2D[] SplitableIcons;
        public GameObject[] SplitablePrefabs;

        private Rect[] _rects;
        private bool _mouseDown = false;

        private void Awake()
        {
            _transform = GetComponent<Transform>();
            _lineRenderer = GetComponent<LineRenderer>();
            p_col = GetComponent<PolygonCollider2D>();
            _camera = GetComponent<Camera>();

            _lineRenderer.enabled = false;

            //if (SplitablePrefabs.Length > 0)
            //{
            //    Instantiate(SplitablePrefabs[0], Vector3.up * 2f, Quaternion.identity);
            //}

            _rects = new Rect[SplitableIcons.Length + 2];
            int width = Screen.width, height = Screen.height;
            int width20th = width / 20, height20th = height / 20;
            int i;

            for (i = 0; i < SplitableIcons.Length; i++)
            {
                int offset = width20th * (i * 2 + 1);
                _rects[i] = new Rect(offset, height20th, width20th * 2, width20th * 2);
            }

            _rects[i++] = new Rect(width - height20th - 50, height20th, 50, height - height20th * 5);
            _rects[i] = new Rect(height20th, height - height20th - 50, width - height20th * 5, 50);

        }
        public bool not_Hitting;
        public static bool hitting = false;



        private void Update()
        {
            if (!Canvas_controller.gameOver)
            {

                if (move.smash)
                {
                    _lineRenderer.enabled = false;
                    p_col.enabled = false;
                    // 



                }
                else
                {

                    RaycastHit hitInfo_;
                    Ray ray_ = _camera.ScreenPointToRay(Input.mousePosition);
                    if (!Physics.Raycast(ray_, out hitInfo_))
                    {
                        Debug.Log("Not Hitted with collider");
                        not_Hitting = true;
                        if (hitting == false)
                            hitting = false;

                    }
                    else
                    {
                        not_Hitting = false;
                        hitting = true;
                    }


                  //  if (Input.GetKey(KeyCode.Escape)) Application.Quit();

                    Vector3 pos;
                    CalcPosition(out pos);
                    _transform.position = pos;
                    _transform.LookAt(Target);


                    if (Input.GetMouseButtonDown(0))
                    {
                        _mouseDown = true;

                        StartCoroutine(m_Coroutine());




                        Vector3 mousePos = Input.mousePosition;
                        mousePos.y = Screen.height - mousePos.y;
                        for (int i = 0; i < _rects.Length; i++)
                        {
                            Rect rect = _rects[i];
                            int dX = (int)(mousePos.x - rect.x),
                                dY = (int)(mousePos.y - rect.y);
                            if (0 < dX && dX < rect.width && 0 < dY && dY < rect.height)
                            {
                                //   _mouseDown = false;
                                break;
                            }
                        }
                    }




                    if (Input.GetMouseButtonDown(0) && _mouseDown && not_Hitting)
                    {
                        _startPos = Input.mousePosition;
                        _hasStartPos = true;


                    }

                    else if (_hasStartPos && Input.GetMouseButtonUp(0) && _mouseDown && not_Hitting && hitting)
                    {


                        _endPos = Input.mousePosition;






                        if (Vector3.Distance(_startPos, _endPos) > MinSplitDistance)
                        {


                           CreateCutPlane(pos_S[0], pos_S[pos_S.ToArray().Length/2-1 ]);
                           //  CreateCutPlane( pos_S[pos_S.ToArray().Length / 2 ], pos_S[pos_S.ToArray().Length  - 1]);


                        }
                        else
                        {
                            Debug.Log("Not enopugh");
                            /*
                                RaycastHit hitInfo;
                              Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                                if (Physics.Raycast(ray, out hitInfo))
                                {
                                    Rigidbody body = hitInfo.collider.GetComponent<Rigidbody>();
                                    if (body != null)
                                    {
                                        body.AddForce(ray.direction * body.mass * ForcePush, ForceMode.Impulse);
                                    }
                                }
                                */
                        }

                        _hasStartPos = false;
                        _lineRenderer.enabled = false;
                        p_col.enabled = false;

                    }


                    if (_hasStartPos && interrrupted == false)
                    {

                        _lineRenderer.enabled = true;


                        //    _lineRenderer.SetPosition(0,new Vector3( GetPosInWorld(_startPos).x,GetPosInWorld(_startPos).y,-0.5f));
                        //   _lineRenderer.SetPosition(1, new Vector3(GetPosInWorld(Input.mousePosition).x, GetPosInWorld(Input.mousePosition).y, -0.5f)) ;



                    }
                }
            }
        }
        /*
        private void OnGUI()
        {
            if (GuiSkin != null) GUI.skin = GuiSkin;

            View.x = GUI.VerticalScrollbar(_rects[SplitableIcons.Length], View.x, 7, 70, 0);
            View.y = GUI.HorizontalScrollbar(_rects[SplitableIcons.Length + 1], View.y, 36, -180, 180);

            for (int i = 0; i < SplitableIcons.Length; i++)
            {
                if (GUI.Button(_rects[i], SplitableIcons[i]))
                {
                    CreateNewObject(i);
                }
            }
            
        }
        */
        /*
                private void CreateNewObject(int i)
                {
                    if (SplitablePrefabs[i] != null)
                    {
                        Splitable splitable = FindObjectOfType(typeof(Splitable)) as Splitable;
                        if (splitable != null)
                        {
                            if (splitable.transform.parent == null)
                                Destroy(splitable.gameObject);
                            else
                                Destroy(splitable.transform.parent.gameObject);
                        }

                        Instantiate(SplitablePrefabs[i], Vector3.up * 2f, SplitablePrefabs[i].transform.rotation);
                    }
                }
                */

        private void CalcPosition(out Vector3 position)
        {
            Vector3 direction = Vector3.forward * -Distance;
            Quaternion rotation = Quaternion.Euler(View.x, View.y, 0);
            position = Target + rotation * direction;
        }

        private Vector3 GetPosInWorld(Vector3 pos)
        {
            Ray ray = _camera.ScreenPointToRay(pos);
            // return ray.origin + ray.direction * CutPlaneDistance;
            return ray.origin + CutPlaneDistance * ray.direction; 
        }

        void CreateCutPlane(Vector3 l_startPos, Vector3 l_endPos)
        {

            Canvas_controller.attempts++;
             Vector3 startPos = GetPosInWorld(l_startPos);
            Vector3 endPos = GetPosInWorld(l_endPos);
          
            Vector3 center = Vector3.Lerp(startPos, endPos, .5f);
            Vector3 cut = (endPos - startPos).normalized;
            Vector3 fwd = (center - _transform.position).normalized;
            Vector3 normal = Vector3.Cross(fwd, cut).normalized;
            
            
            GameObject goCutPlane = new GameObject("Any name", typeof(BoxCollider), typeof(Rigidbody), typeof(SplitterSingleCut)); //calling for SplitterSingleCut class, this class is a child of Splitter class
           
            #region No need
            /*
           goCutPlane.GetComponent<Collider>().isTrigger = true;
           Rigidbody bodyCutPlane = goCutPlane.GetComponent<Rigidbody>();
           bodyCutPlane.useGravity = false;
           bodyCutPlane.isKinematic = true;
           */
            #endregion
            Transform transformCutPlane = goCutPlane.transform;

            transformCutPlane.position = center;//This is the pos 
            transformCutPlane.localScale = new Vector3(CutPlaneSize, .01f, CutPlaneSize/2);
            transformCutPlane.rotation = _transform.rotation;
            transformCutPlane.up = normal;//this is normalized pos
            
            
        }


        PolygonCollider2D p_col;
        public List<Vector3> pos_S;

        public static bool interrrupted = false;
        private Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
        {

            float u = 1 - t;
            float tt = t * t;
            float uu = u * u;
            float uuu = uu * u;
            float ttt = tt * t;

            Vector3 p = uuu * p0;
            p += 3 * uu * t * p1;
            p += 3 * u * tt * p2;
            p += ttt * p3;

            return p;
        }


        IEnumerator m_Coroutine()
        {
            interrrupted = false;
            List<Vector3> pos = new List<Vector3>();
            pos_S = new List<Vector3>();
            _lineRenderer.SetWidth(1f, 1f);
            while (Input.GetMouseButton(0) && (interrrupted == false))
            {


                pos.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition) + Vector3.forward * (3));
                pos_S.Add(Input.mousePosition);

                _lineRenderer.SetVertexCount(pos.Count);
                  _lineRenderer.SetPositions(pos.ToArray());
               

                List<Vector2> pos2 = new List<Vector2>();
                for (int i = 0; i < pos.Count; i++)
                {
                    pos2.Add(new Vector2(pos[i].x, pos[i].y));
                   
                    //   pos_S.Add( new Vector3(pos[i].x,pos[i].y,0));

                }

                //   p_col = _lineRenderer.gameObject.GetComponent<PolygonCollider2D>();

                if (_lineRenderer.enabled == true) p_col.enabled = true;
                else p_col.enabled = false;

                p_col.points = pos2.ToArray();


                yield return new WaitForSeconds(0);
            }
            if (Input.GetMouseButtonUp(0))
            {

                _lineRenderer.enabled = false;
                p_col.enabled = false;
                interrrupted = true;
                _hasStartPos = false;
                Debug.Log("Interrrupted");

            }
        }
    }
}
