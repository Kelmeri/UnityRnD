using UnityEngine;
using System.Collections;
using System.IO;

public class CameraCapture : MonoBehaviour
{
    WebCamTexture webcamTexture;

    void Start()
    {
        // Check for available cameras.
        if(WebCamTexture.devices.Length > 1)
        {
            // Use the second camera.
            webcamTexture = new WebCamTexture(WebCamTexture.devices[1].name);
        }
        else
        {
            // If second camera is not available, use the first camera.
            webcamTexture = new WebCamTexture(WebCamTexture.devices[0].name);
        }

        webcamTexture.Play();
    }

    public Texture2D CapturePicture()
    {
        // Capture a picture from the webcam.
        Texture2D capturedPicture = new Texture2D(webcamTexture.width, webcamTexture.height);
        capturedPicture.SetPixels(webcamTexture.GetPixels());
        capturedPicture.Apply();

        return capturedPicture;
    }

    public byte[] ConvertToBitmap(Texture2D texture)
    {
        // Convert the captured picture into bitmap format.
        byte[] bitmap = texture.EncodeToPNG();

        return bitmap;
    }
}
