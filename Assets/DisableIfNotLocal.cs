using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class DisableIfNotLocal : NetworkBehaviour
{
    [SerializeField] private List<GameObject> _gameObjects = new();

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if (!IsLocalPlayer)
        {
            foreach (var gameObject in _gameObjects)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
