using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.IO;
using UnityEngine;
using AForge.Video;
using AForge.Video.DirectShow;
using ZXing;

public struct QRCodeAnalyzer
{
    private static AutoResetEvent _autoResetEvent = new AutoResetEvent(false);
    private static Bitmap _bitmap;
    public static int AnalyzeQRCode()
    {
        var videoDevices = new AForge.Video.DirectShow.FilterInfoCollection(FilterCategory.VideoInputDevice);
        if (videoDevices.Count == 0)
        {
            Debug.LogError("No camera found");
            return -1;
        }
        var videoDevice = new VideoCaptureDevice(videoDevices[0].MonikerString ?? videoDevices[1].MonikerString);
        videoDevice.NewFrame += VideoDevice_NewFrame;
        videoDevice.Start();
        _autoResetEvent.WaitOne();
        videoDevice.SignalToStop();
        videoDevice.WaitForStop();

        //save to pictures folder
        string picturesFolderPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyPictures);
        _bitmap.Save(Path.Combine(picturesFolderPath, "QRCode.png"));

        string decodedQRCode = DecodeQRCode(_bitmap);
        if (string.IsNullOrEmpty(decodedQRCode))
        {
            return -1;
        }
        else
        {
            return int.Parse(decodedQRCode);
        }
    }
    private static void VideoDevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
    {
        _bitmap = (Bitmap)eventArgs.Frame.Clone();
        _autoResetEvent.Set();
    }
    private static string DecodeQRCode(Bitmap bitmap)
    {
        var barcodeReader = new BarcodeReader();
        var result = barcodeReader.Decode(bitmap);
        if (result != null)
        {
            Debug.Log("QR Code: " + result.Text);
            return result.Text;
        }
        else
        {
            Debug.Log("No QR Code found");
            return null;
        }
    }

}
