using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(LineRenderer))]
public class LineDrawer : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private List<Vector3> points = new List<Vector3>();

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }
    
   
    // Update is called once per frame
    void Update()
    {
        DrawLine();

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            CheckLineIntersections();
        }
    }

    void DrawLine()
    {
        if (Input.GetMouseButtonDown(0))
        {
            points.Clear();
            lineRenderer.positionCount = 0;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;

            if (points.Count == 0 || (points.Count > 0 && Vector3.Distance(points[points.Count - 1], mousePos) > 0.1f))
            {
                points.Add(mousePos);
                lineRenderer.positionCount++;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, mousePos);
            }
        }

        
    }

    void CheckLineIntersections()
    {
        for (int i = 0; i < points.Count - 1; i++)
        {
            Vector3 start = points[i];
            Vector3 end = points[i + 1];

            RaycastHit2D hit = Physics2D.Linecast(start, end);
            if (hit.collider != null && hit.collider.CompareTag("Circle")) 
            {
                Destroy(hit.collider.gameObject); 
            }
        }
    }
}
