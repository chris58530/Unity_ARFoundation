using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]

public class ARTapToPlaneObject : MonoBehaviour
{
    [SerializeField]
    GameObject instantiateObject;

    private GameObject spawnObject;
    private ARRaycastManager arRaycastManager;
    private Vector2 touchPosition;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    [SerializeField]
    GameObject UI;
    bool isBlock;
    [SerializeField] Button button;
    void Awake()
    {
        arRaycastManager= GetComponent<ARRaycastManager>();
        UI.SetActive(false);
    }

    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }
        touchPosition = default;
        return false;
    }

    void Update()
    {
        //if (isBlock)
        //{
        //    var colors = GetComponent<Button>().colors;
        //    colors.normalColor = Color.red;
        //    GetComponent<Button>().colors = colors;

        //}
        //else
        //{
        //    var colors = GetComponent<Button>().colors;
        //    colors.normalColor = Color.white;
        //    GetComponent<Button>().colors = colors;

        //}
        if (!TryGetTouchPosition(out Vector2 touchPosition))
        {
            UI.SetActive(true);
     
            return;
        }
      
        if (arRaycastManager.Raycast(touchPosition, hits, trackableTypes: TrackableType.PlaneWithinPolygon) && !IsPointOverObject())
        {
            UI.SetActive(false);

            var hitPose = hits[0].pose;

            if(spawnObject == null)
            {
                spawnObject = Instantiate(instantiateObject, hitPose.position , hitPose.rotation);
            }
            else
            {
                if(!isBlock)
                    spawnObject.transform.position = hitPose.position;              
            }
        }

    }
    public void BlockObject()
    {
        isBlock = !isBlock;
     

    }
    public void PressLeft()
    {
        spawnObject.transform.Rotate(Vector3.down * 20, Space.World);
      
    }
    public void PressRight()
    {
        spawnObject.transform.Rotate(Vector3.down * -20, Space.World);      
    }
    private bool IsPointOverObject() //避免控制UI時點到畫面
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = new Vector2(Input.mousePosition.x,Input.mousePosition.y);

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, results);
        return results.Count > 0;
    }
}
