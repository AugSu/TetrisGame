using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CameraController 
{
    private Camera camera;
    public void Awake()
    {
        camera = Camera.main;
    }

    //摄像机视野变大
    public void ZoomOut()
    {
        camera.DOOrthoSize(21f, 1.0f);
    }

    public void ZoomIn()
    {
        camera.DOOrthoSize(13.46f, 1.0f);
    }
}
