using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class QRCodeReaderController : MonoBehaviour
{
    [SerializeField] InputActionReference _qrCodeReaderAction;
    [SerializeField] List<Card> _cards = new();
    [SerializeField] private CameraCapture _cameraCapture;

    private void Start()
    {
        _qrCodeReaderAction.action.performed += OnQRCodeReader;
    }
    private void OnDestory()
    {
        _qrCodeReaderAction.action.performed -= OnQRCodeReader;
    }
    private void OnQRCodeReader(InputAction.CallbackContext context)
    {
        int i = QRCodeAnalyzer.AnalyzeQRCode(_cameraCapture.ConvertToBitmap(_cameraCapture.CapturePicture()));
        switch (i)
        {
            case 1:
                DisableAllOthersAndEnableIndex(0);
                break;
            case 2:
                DisableAllOthersAndEnableIndex(1);
                break;
            case 3:
                DisableAllOthersAndEnableIndex(2);
                break;
            case 4:
                DisableAllOthersAndEnableIndex(3);
                break;
            default:
                Debug.Log("No matches");
                break;
        }
        Debug.Log("QRCodeReader");
    }
    private void DisableAllOthersAndEnableIndex(int index)
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
