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
    public static int AnalyzeQRCode(byte[] bitmap)
    {
        _bitmap = new Bitmap(new MemoryStream(bitmap));
        string decodedQRCode = DecodeQRCode(_bitmap);
        string picturesFolderPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyPictures);
        _bitmap.Save(Path.Combine(picturesFolderPath, "QRCode.png"));
        if (string.IsNullOrEmpty(decodedQRCode))
        {
            return -1;
        }
        else
        {
            if (int.TryParse(decodedQRCode, out int i))
            {
                return i;
            }
            else
            {
                return -1;
            }
        }
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