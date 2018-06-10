using System.Collections;
using UnityEngine;
using UnityEngine.UI;

using Vuforia;
using Image = Vuforia.Image;

public class CameraImageAccess : MonoBehaviour
{
    private bool mAccessCameraImage = true;
    public GameObject Plane;
    private bool isTracked = false;
    
    // private Texture2D tex;

        // The desired camera image pixel format
        // private Image.PIXEL_FORMAT mPixelFormat = Image.PIXEL_FORMAT.RGB565;// or RGBA8888, RGB888, RGB565, YUV
        // Boolean flag telling whether the pixel format has been registered
    
#if UNITY_EDITOR
    private Image.PIXEL_FORMAT mPixelFormat = Vuforia.Image.PIXEL_FORMAT.GRAYSCALE;
#elif UNITY_ANDROID
   private Image.PIXEL_FORMAT mPixelFormat =  Vuforia.Image.PIXEL_FORMAT.RGB888;
#elif UNITY_IOS
    private Image.PIXEL_FORMAT mPixelFormat =  Vuforia.Image.PIXEL_FORMAT.RGB888;
#endif

    private bool mFormatRegistered = false;

    void Start()
    {

        // Register Vuforia life-cycle callbacks:
        Vuforia.VuforiaARController.Instance.RegisterVuforiaStartedCallback(OnVuforiaStarted);
        Vuforia.VuforiaARController.Instance.RegisterOnPauseCallback(OnPause);
        Vuforia.VuforiaARController.Instance.RegisterTrackablesUpdatedCallback(OnTrackablesUpdated);
        }
    /// <summary>
    /// Called when Vuforia is started
    /// </summary>
    private void OnVuforiaStarted()
    {
        // Try register camera image format
        if (CameraDevice.Instance.SetFrameFormat(mPixelFormat, true))
        {
            Debug.Log("Successfully registered pixel format " + mPixelFormat.ToString());
            mFormatRegistered = true;
        }
        else
        {
            Debug.LogError("Failed to register pixel format " + mPixelFormat.ToString() +
                "\n the format may be unsupported by your device;" +
                "\n consider using a different pixel format.");
            mFormatRegistered = false;
        }
    }
    /// <summary>
    /// Called when app is paused / resumed
    /// </summary>
    private void OnPause(bool paused)
    {
        if (paused)
        {
            Debug.Log("App was paused");
            UnregisterFormat();
        }
        else
        {
            Debug.Log("App was resumed");
            RegisterFormat();
        }
    }
    /// <summary>
    /// Called each time the Vuforia state is updated
    /// </summary>
    private void OnTrackablesUpdated()
    {
        
        if (mFormatRegistered)
        {
            if (mAccessCameraImage)
            {
                
                if (!isTracked && Plane.GetComponent<MeshRenderer>().enabled)
                {
                    StartCoroutine(CorrectPlane());
                }
               
                isTracked = true;
            }
        }

    }

    private IEnumerator CorrectPlane()
    {
        yield return new WaitForSeconds(2f);
        Debug.Log("Je fais ce que je veux");
        Vuforia.Image image = CameraDevice.Instance.GetCameraImage(mPixelFormat);
        if (image != null && image.IsValid())
        {
            string imageInfo = mPixelFormat + " image: \n";
            imageInfo += " size: " + image.Width + " x " + image.Height + "\n";
            imageInfo += " bufferSize: " + image.BufferWidth + " x " + image.BufferHeight + "\n";
            imageInfo += " stride: " + image.Stride;
            Debug.Log(imageInfo);
            byte[] pixels = image.Pixels;

            if (pixels != null && pixels.Length > 0)
            {
                Texture2D texture = new Texture2D (640, 360);
                image.CopyToTexture (texture);
                texture.Apply();
                Color [] pix = texture.GetPixels(200 , 640 / 2, 640 / 5, 360 / 15);
                Plane.GetComponent<Renderer> ().material.mainTexture = texture;
                float r = 0;
                float g = 0;
                float b = 0;
                float a = 0;
                foreach (Color col in pix){
                    r += col.r;
                    g += col.g;
                    b += col.b;
                    a += col.a;
                } 

                r /= pix.Length;
                g /= pix.Length;
                b /= pix.Length;
                a /= pix.Length;   
                
                Plane.GetComponent<Renderer>().material.SetColor("_EmissionColor",  new Color (r, g, b, a));
                //Plane.GetComponent<Renderer>().material.color = new Color (r, g, b, a); 
            }
        }
        isTracked = false; 
    }

    /// <summary>
    /// Unregister the camera pixel format (e.g. call this when app is paused)
    /// </summary>
    private void UnregisterFormat()
    {
        Debug.Log("Unregistering camera pixel format " + mPixelFormat.ToString());
        CameraDevice.Instance.SetFrameFormat(mPixelFormat, false);
        mFormatRegistered = false;
    }
    /// <summary>
    /// Register the camera pixel format
    /// </summary>
    private void RegisterFormat()
    {
        if (CameraDevice.Instance.SetFrameFormat(mPixelFormat, true))
        {
            Debug.Log("Successfully registered camera pixel format " + mPixelFormat.ToString());
            mFormatRegistered = true;
        }
        else
        {
            Debug.LogError("Failed to register camera pixel format " + mPixelFormat.ToString());
            mFormatRegistered = false;
        }
    }
}