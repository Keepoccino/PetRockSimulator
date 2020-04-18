using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Rope : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public Transform Point1;
    public Transform Point2;
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPositions(new[] { Point1.position, Point2.position });
    }
}
