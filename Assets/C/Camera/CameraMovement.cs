using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private float zoomStep, minCamSize, maxCamSize;
    [SerializeField] private SpriteRenderer mapRenderer;
    [SerializeField] GameObject mapoint;

    private Vector3 dragOrigin;
    private float mapMinX, mapMaxX, mapMinY, mapMaxY;

    private void Awake()
    {
        mapMinX = mapRenderer.transform.position.x - mapRenderer.bounds.size.x / 2f;
        mapMaxX = mapRenderer.transform.position.x + mapRenderer.bounds.size.x / 2f;

        mapMinY = mapRenderer.transform.position.y - mapRenderer.bounds.size.y / 2f;
        mapMaxY = mapRenderer.transform.position.y + mapRenderer.bounds.size.y / 2f;
    }

    private void Update()
    {
        if (!MiniMapClick.Inst.MapClick && !StoryCam.Inst.CamMove_sub) //미니맵을 클릭 중이 아니라면
            PanCamera();

        float wheelInput = Input.GetAxis("Mouse ScrollWheel");
        if (wheelInput > 0)
        {
            ZoomIn();
        }
        else if (wheelInput < 0)
        {
            ZoomOut();
        }
    }

    private void PanCamera()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            if (dragOrigin.z <= -200)
            {
                Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);

                cam.transform.position = ClampCamera(cam.transform.position + difference);
            }
        }
    }

    public void ZoomIn()
    {
        float newSize = cam.orthographicSize - zoomStep;
        cam.orthographicSize = Mathf.Clamp(newSize, minCamSize, maxCamSize);

        mapoint.transform.localScale = new Vector3(cam.orthographicSize * 0.5f, cam.orthographicSize * 0.3f, 1);
        cam.transform.position = ClampCamera(cam.transform.position);
    }

    public void ZoomOut()
    {
        float newSize = cam.orthographicSize + zoomStep;
        cam.orthographicSize = Mathf.Clamp(newSize, minCamSize, maxCamSize);

        mapoint.transform.localScale = new Vector3(cam.orthographicSize * 0.5f, cam.orthographicSize * 0.3f, 1);
        cam.transform.position = ClampCamera(cam.transform.position);
    }

    private Vector3 ClampCamera(Vector3 targetPosition)
    {
        float camHeight = cam.orthographicSize;
        float camWidth = cam.orthographicSize * cam.aspect;

        float minX = mapMinX + camWidth;
        float maxX = mapMaxX - camWidth;
        float minY = mapMinY + camHeight;
        float maxY = mapMaxY - camHeight;

        float newX = Mathf.Clamp(targetPosition.x, minX, maxX);
        float newY = Mathf.Clamp(targetPosition.y, minY, maxY);

        return new Vector3(newX, newY, targetPosition.z);
    }
}