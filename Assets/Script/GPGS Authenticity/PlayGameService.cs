using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GooglePlayGames;
using GooglePlayGames.BasicApi;


public class PlayGameService : MonoBehaviour
{
    public static PlayGameService Instance { get; private set; }
    public bool isConnectedToGoogleService;

    public void Start()
    {

       PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
       PlayGamesPlatform.Activate();
    }

    internal void ProcessAuthentication(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            // Continue with Play Games Services
            isConnectedToGoogleService = true;
        }
        else
        {
            isConnectedToGoogleService = false;
            // Disable your integration with Play Games Services or show a login button
            // to ask users to sign-in. Clicking it should call
            // PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication).
        }
    }
}
