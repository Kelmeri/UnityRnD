// 2023-12-10 AI-Tag 
// This was created with assistance from Muse, a Unity Artificial Intelligence product

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode; // Include the Netcode for GameObjects library

public class QRCodeReaderController : NetworkBehaviour
{
    [SerializeField] InputActionReference _qrCodeReaderAction;
    [SerializeField] List<Card> _cards = new();
    [SerializeField] private CameraCapture _cameraCapture;

    public void Start()
    {
        if (IsLocalPlayer)
        {
            _qrCodeReaderAction.action.performed += OnQRCodeReader;
        }
    }
    public override void OnDestroy()
    {
        base.OnDestroy();
        if (IsLocalPlayer)
        {
            _qrCodeReaderAction.action.performed -= OnQRCodeReader;
        }
    }
    
    private void OnQRCodeReader(InputAction.CallbackContext context)
    {
        int i = QRCodeAnalyzer.AnalyzeQRCode(_cameraCapture.ConvertToBitmap(_cameraCapture.CapturePicture()));
        CmdOnQRCodeReaderServerRpc(i);
    }

    [ServerRpc]
    private void CmdOnQRCodeReaderServerRpc(int i)
    {
        switch (i)
        {
            case 1:
                DisableAllOthersAndEnableIndexClientRpc(0);
                break;
            case 2:
                DisableAllOthersAndEnableIndexClientRpc(1);
                break;
            case 3:
                DisableAllOthersAndEnableIndexClientRpc(2);
                break;
            case 4:
                DisableAllOthersAndEnableIndexClientRpc(3);
                break;
            default:
                Debug.Log("No matches");
                break;
        }
        Debug.Log("QRCodeReader");
    }

    [ClientRpc]
    private void DisableAllOthersAndEnableIndexClientRpc(int index)
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
