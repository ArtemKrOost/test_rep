using UnityEngine;
using System.Collections;

//просто коммент

public class Player_Movement_Controller : MonoBehaviour
{
    public float speed;
    public Vector3 endPoint;
    public float rotSpeed;
    public bool flag_debug;
    public GameObject pref_test_obj;
    public LayerMask LM;
    public bool flag_use_mask;
    public float stop_range;

    void Start()
    {
        LM = new LayerMask();
        endPoint = this.transform.position;
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rh;
            if (!flag_use_mask)
            {
                if (Physics.Raycast(ray, out rh))
                {
                    if (flag_debug) Debug.Log("Raycast сработал");
                    if (rh.collider.tag == "Terrain")
                    {
                        endPoint = rh.point;
                        if (flag_debug) Debug.Log("Raycast попал в обьект с тэгом Terrain");
                    }
                }
            }
            else
            {
                if (Physics.Raycast(ray, out rh, LM))
                {
                    if (flag_debug) Debug.Log("Raycast сработал");
                    if (rh.collider.tag == "Terrain")
                    {
                        endPoint = rh.point;
                        if (flag_debug) Debug.Log("Raycast попал в обьект с тэгом Terrain");
                    }
                }
            }
            if (flag_debug)
            {
                if(flag_debug) Debug.Log("Нажатие задетектил. endPoint = " + endPoint);
                if (pref_test_obj)
                {
                    GameObject go = Instantiate(pref_test_obj, endPoint, new Quaternion()) as GameObject;
                    Destroy(go, 2f);
                }
            }
        }

        //_______________________________________________________________________________________________________________________________________________________________________________
        if ((this.transform.position - endPoint).magnitude > 0.3f)
        {
            if (flag_debug) Debug.Log("!=");
            if (Vector3.Angle(this.transform.forward, endPoint - this.transform.position) > 5f)
            {
                if (flag_debug) Debug.Log("Вертим на хую");
                this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, Quaternion.LookRotation(endPoint - this.transform.position), rotSpeed * Time.deltaTime);
            }
            else
            {
                if (Physics.Raycast(this.transform.position, this.transform.forward, stop_range))
                {
                    endPoint = this.transform.position;
                }
                else
                {
                    if (flag_debug) Debug.Log("Ходим");
                    this.transform.position += speed * Time.deltaTime * this.transform.forward;
                    //this.transform.position = Vector3.MoveTowards(this.transform.position, endPoint, speed * Time.deltaTime);
                }
            }
        }
    }


	
}
