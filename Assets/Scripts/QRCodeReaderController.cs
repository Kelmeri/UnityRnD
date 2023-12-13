// 2023-12-10 AI-Tag 
// This was created with assistance from Muse, a Unity Artificial Intelligence product

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;

public class QRCodeReaderController : NetworkBehaviour
{
    [SerializeField] InputActionReference _qrCodeReaderAction;
    [SerializeField] List<Card> _cards = new();
    [SerializeField] private CameraCapture _cameraCapture;

    public void Start()
    {
        Debug.Log("lolxd");
        
            _qrCodeReaderAction.action.performed += OnQRCodeReader;
        
    }
    public override void OnDestroy()
    {
        base.OnDestroy();
        
            _qrCodeReaderAction.action.performed -= OnQRCodeReader;
    }
    
    private void OnQRCodeReader(InputAction.CallbackContext context)
    {
        Debug.Log("lolxdx2");
        int i = QRCodeAnalyzer.AnalyzeQRCode(_cameraCapture.ConvertToBitmap(_cameraCapture.CapturePicture()));
        OnQrCodeRead(i);
    }

    
    private void OnQrCodeRead(int i)
    {
        switch (i)
        {
            case 1:
                DisableAllOthersAndEnableIndexClientRp
                (0);
                break;
            case 2:
                DisableAllOthersAndEnableIndexClientRp
                (1);
                break;
            case 3:
                DisableAllOthersAndEnableIndexClientRp
                (2);
                break;
            case 4:
                DisableAllOthersAndEnableIndexClientRp
                (3);
                break;
            default:
                Debug.Log("No matches");
                break;
        }
        Debug.Log("QRCodeReader");
    }

    
    private void DisableAllOthersAndEnableIndexClientRp(int index)
    {
        if (_cards[index].gameObject == null)
        {
            Debug.LogError("Card is null");
            return;
        }
        for (int i = 0; i < _cards.Count; i++)
        {
            if (i != index)
            {
                _cards[i].gameObject.SetActive(false);
            }
            else
            {
                _cards[i].gameObject.SetActive(true);
            }
        }
    }
}
