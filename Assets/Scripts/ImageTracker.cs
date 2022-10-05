using System.Collections; 
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;

public class ImageTracker : MonoBehaviour
{
    ARTrackedImageManager imageManager;
    // Start is called before the first frame update
    private void Awake()
    {
        imageManager = FindObjectOfType<ARTrackedImageManager>();
    }
    //private void OnEnable()
    //{
    //    imageManager.trackedImagesChanged += OnImageChanged;
    //}
    //private void OnDisable()
    //{
    //    imageManager.trackedImagesChanged -= OnImageChanged;

    //}
    void OnImageChanged(ARTrackedImagesChangedEventArgs args)
    {
        foreach(var trackedImage in args.added)
        {
            Debug.Log(trackedImage.name);
        }
    }
}
