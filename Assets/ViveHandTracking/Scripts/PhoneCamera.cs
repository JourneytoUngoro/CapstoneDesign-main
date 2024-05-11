using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneCamera : MonoBehaviour
{
    private bool camAvailable;
    private WebCamTexture backCam;
    private Texture defaultBackground;

    public RawImage background;
    public AspectRatioFitter aspectRatioFitter;

    private void Start()
    {
        defaultBackground = background.texture;
        WebCamDevice[] devices = WebCamTexture.devices;

        if (devices.Length == 0)
        {
            Debug.Log("No camera detected");
            camAvailable = false;
            return;
        }

        foreach (WebCamDevice webcam in devices)
        {
            if(!webcam.isFrontFacing)
            {
                backCam = new WebCamTexture(webcam.name, Screen.width, Screen.height);
                break;
            }
        }

        if (backCam == null)
        {
            Debug.Log("Unable to find back camera");
            return;
        }

        backCam.Play();
        background.texture = backCam;

        camAvailable = true;
    }

    private void Update()
    {
        if (!camAvailable) return;

        float ratio = (float)backCam.width / (float)backCam.height;
        aspectRatioFitter.aspectRatio = ratio;

        float scaleY = backCam.videoVerticallyMirrored ? -1.0f : 1.0f;
        background.rectTransform.localScale = new Vector3(1.0f, scaleY, 1.0f);

        int orient = -backCam.videoRotationAngle;
        background.rectTransform.localEulerAngles = new Vector3(0.0f, 0.0f, orient);
    }
}
