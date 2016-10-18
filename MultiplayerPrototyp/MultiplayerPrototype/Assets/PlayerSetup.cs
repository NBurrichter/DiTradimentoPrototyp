using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

/// <summary>
/// Handels the setup, when the player connects
/// </summary>
public class PlayerSetup : NetworkBehaviour {

    [SerializeField]
    private Behaviour[] componentsToDisableOnRemotePlayer;
    [SerializeField]
    private Behaviour[] componentsToDisableOnClientPlayer;

    [SerializeField]
    private GameObject localPlayerModel;

    private Camera sceneCamera;

    void Start()
    {
        //---Disable all components on remotes, that only the player should be able to use---
        if(!isLocalPlayer)
        {
            DisableRemoteComponents();
        }
        else
        {
            DisableLocalComponents();
            localPlayerModel.SetActive(false);

            //Disable the overview camera
            sceneCamera = Camera.main;
            if(sceneCamera != null)
            {
                sceneCamera.gameObject.SetActive(false);
            }
        }

        
    }

    /// <summary>
    /// Disables all components that should only the local player be able to use (Player controller, player camera, etc.)
    /// </summary>
    void DisableRemoteComponents()
    {
        for (int i = 0; i < componentsToDisableOnRemotePlayer.Length; i++)
        {
            componentsToDisableOnRemotePlayer[i].enabled = false;
        }
    }

    /// <summary>
    /// Dissables all components that only other players should be albe to see (Player name text etc.)
    /// </summary>
    void DisableLocalComponents()
    {
        for (int i = 0; i < componentsToDisableOnClientPlayer.Length; i++)
        {
            componentsToDisableOnClientPlayer[i].enabled = false;
        }
    }

}
