using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovBall : MonoBehaviour
{
    public GameObject figuraPrefab;
    public Color colorFigura;

    private float lastTapTime;
    private Vector2 lastTapPosition;
    private Transform cositos;
    private Vector2 INT;
    private Vector2 ENDT;
    [SerializeField]
    private List<GameObject> array;


    void Update()
    {

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 xd = touch.position;

            Vector2 tapPosition = GetWorldPositionOnPlane(xd,0);
            transform.position = tapPosition;

            if (touch.phase == TouchPhase.Began)
            {
                INT = touch.position;
                RaycastHit2D hit = Physics2D.Raycast(tapPosition, Vector2.zero);

                if (touch.tapCount == 1)
                {
                    if (hit.collider != null)
                    {
                        cositos = hit.collider.transform;
                    }
                    else
                    {
                        Debug.Log("Aea manito");
                        CrearFigura(tapPosition);
                    }
                }
                else if (touch.tapCount == 2 && hit.collider != null)
                {
                    Debug.Log(hit.collider.gameObject);
                    Destroy(hit.collider.gameObject);
                }

            }

            if (touch.phase == TouchPhase.Moved)
            {
                if (cositos != null)
                {
                    cositos.transform.position = tapPosition;
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                cositos = null;
                ENDT = touch.position;
                Vector2 swipeD = ENDT - INT;

                float swipeM = swipeD.magnitude;
                //        if (swipeM > 0.6f && array != null)
                //        {
                //            for (int i = 0; i < array.Count; i++)
                //            {
                //                Destroy(array[i]);

                //            }
                //            array.Clear();
                //        }
                //    }
                //}      
            }
        }

    }
    public Vector3 GetWorldPositionOnPlane(Vector3 screenPosition, float z)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        Plane xy = new Plane(Vector3.forward, new Vector3(0, 0, z));
        float distance;
        xy.Raycast(ray, out distance);
        return ray.GetPoint(distance);
    }

    public void CrearFigura(Vector2 posicion)
    {
        GameObject Ball = Instantiate(figuraPrefab, posicion, Quaternion.identity);
        array.Add(Ball);
        Debug.Log("xd");
    }
}
