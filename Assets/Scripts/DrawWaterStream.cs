using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawWaterStream : MonoBehaviour {
    public LineRenderer lineRenderer;
    private float timeToLive = 0.3f;
    private bool drawing = false;

    public void StartPosition(Vector3 position) {
        Vector3 renderVector = new Vector3(position.x, position.y, 1f);
        lineRenderer.SetPosition(0, renderVector);
        drawing = true;
        timeToLive = 0.5f;
        lineRenderer.enabled = true;
    }

    public void EndPosition(Vector3 position) {
        Vector3 renderVector = new Vector3(position.x, position.y, 1f);
        lineRenderer.SetPosition(1, renderVector);
        drawing = true;
        timeToLive = 0.5f;
        lineRenderer.enabled = true;
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
		if (drawing) {
            timeToLive -= Time.deltaTime;
            if (timeToLive <= 0) {
                drawing = false;
                lineRenderer.enabled = false;
                //lineRenderer.SetPosition(0, new Vector3(0, 0, 0));
                //lineRenderer.SetPosition(1, new Vector3(0, 0, 0));
            }
        }
	}

    void FixedUpdate() {
        
    }
}
