using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawWaterStream : MonoBehaviour {
    public LineRenderer lineRenderer;

    public void StartPosition(Vector3 position) {
        Vector3 renderVector = new Vector3(position.x, position.y, 100);
        lineRenderer.SetPosition(0, renderVector);
    }

    public void EndPosition(Vector3 position) {
        Vector3 renderVector = new Vector3(position.x, position.y, 100);
        lineRenderer.SetPosition(1, renderVector);
    }

    // Use this for initialization
    void Start () {
        if (lineRenderer == null) {
            lineRenderer = GetComponent<LineRenderer>();
        }

        if (lineRenderer != null) {
            lineRenderer.positionCount = 2;
            lineRenderer.startColor = Color.blue;
            lineRenderer.endColor = Color.blue;
            lineRenderer.sortingLayerName = "Foreground";
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate() {
        
    }
}
