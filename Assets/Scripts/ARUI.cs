using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARUI : MonoBehaviour
{

    private GameObject ARObject;

    public void GetObject(GameObject _getObject)
    {
        ARObject = _getObject;
    }
    public void PressLeft()
    {
        if (ARObject != null)
            ARObject.transform.Rotate(Vector3.down * 20, Space.World);
        else
            return;
    }
    public void PressRight()
    {
        if (ARObject != null)
            ARObject.transform.Rotate(Vector3.down * -20, Space.World);
        else
            return;
    }
}
