using Photon.Pun;
using System;
using System.Threading.Tasks;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Vivox;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class VivoxSetup : MonoBehaviour
{
    [Header("Vivox Settings")]
    public string voiceChannelName = "StoryTellerRoom";
    public string playerNamePrefix = "Player";

    [Header("Mute Button UI")]
    public Button muteButton;
    public TMP_Text muteButtonText;

    private bool isMicMuted = false;
    private bool isVivoxReady = false;
    private bool isJoiningVivox = false;


    [Header("Talking UI")]
    public TMP_Text talkingText;

    private string testPlayerName;


    // Static protection so Vivox does not join the same channel twice
    private static bool globalVivoxJoined = false;
    private static bool globalVivoxJoining = false;
    private static VivoxSetup instance;

    private Dictionary<string, string> vivoxToPhotonNames = new Dictionary<string, string>();

    // Static protection so Vivox does not join the same channel twice
    private async void Awake()
    {
        // Prevent duplicate Vivox managers across scene loads
        if (instance != null && instance != this)
        {
            Debug.LogWarning("Duplicate VivoxSetup found. Destroying this copy.");
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    private async void Start()
    {
        SetupMuteButton();
        UpdateMuteButtonText();

        await InitializeVivox();
    }

    private void SetupMuteButton()
    {
        if (muteButton != null)
        {
            muteButton.onClick.RemoveListener(ToggleMute);
            muteButton.onClick.AddListener(ToggleMute);
        }
        else
        {
            Debug.LogWarning("Mute Button is not assigned in the Inspector.");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleMute();
        }

        CheckWhoIsTalking();
    }
    private async Task InitializeVivox()
    {
        if (isJoiningVivox || isVivoxReady || globalVivoxJoining || globalVivoxJoined)
        {
            Debug.LogWarning("Vivox is already joining or already connected. Skipping duplicate join.");

            isVivoxReady = true;
            return;
        }

        isJoiningVivox = true;
        globalVivoxJoining = true;

        try
        {
            testPlayerName = PhotonNetwork.NickName;

            if (string.IsNullOrEmpty(testPlayerName))
            {
                testPlayerName = "Player_" + UnityEngine.Random.Range(1000, 9999);
            }
            Debug.Log("Starting Vivox test as: " + testPlayerName);

            if (UnityServices.State != ServicesInitializationState.Initialized)
            {
                await UnityServices.InitializeAsync();
                Debug.Log("Unity Services initialized successfully.");
            }

            if (!AuthenticationService.Instance.IsSignedIn)
            {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
                Debug.Log("Authentication signed in successfully.");
            }

            Debug.Log("Vivox setup reached authentication stage.");

            await VivoxService.Instance.InitializeAsync();

            await VivoxService.Instance.LoginAsync(new LoginOptions()
            {
                DisplayName = testPlayerName
            });

            await VivoxService.Instance.JoinGroupChannelAsync(
                voiceChannelName,
                ChatCapability.AudioOnly
            );
            Debug.Log("Vivox initialized successfully.");

      

            isVivoxReady = true;
            globalVivoxJoined = true;

            Debug.Log(testPlayerName + " joined Vivox voice channel: " + voiceChannelName);
        }
        catch (InvalidOperationException e)
        {
            Debug.LogWarning("Vivox was already in this channel. Treating as connected: " + e.Message);

            isVivoxReady = true;
            globalVivoxJoined = true;
        }
        catch (Exception e)
        {
            Debug.LogError("Vivox setup failed: " + e.Message);

            isVivoxReady = false;
            globalVivoxJoined = false;
        }
        finally
        {
            isJoiningVivox = false;
            globalVivoxJoining = false;
        }
    }

    public void ToggleMute()
    {
        if (!isVivoxReady)
        {
            Debug.LogWarning("Vivox is not ready yet. Wait until the voice channel is joined.");
            return;
        }

        try
        {
            if (!isMicMuted)
            {
                VivoxService.Instance.MuteInputDevice();
                isMicMuted = true;
                Debug.Log(testPlayerName + " microphone muted.");
            }
            else
            {
                VivoxService.Instance.UnmuteInputDevice();
                isMicMuted = false;
                Debug.Log(testPlayerName + " microphone unmuted.");
            }

            UpdateMuteButtonText();
        }
        catch (Exception e)
        {
            Debug.LogWarning("Mute toggle failed: " + e.Message);
        }
    }


    private void CheckWhoIsTalking()
    {
        if (!isVivoxReady)
            return;

        try
        {
            foreach (var channel in VivoxService.Instance.ActiveChannels)
            {
                foreach (var participant in channel.Value)
                {
                    if (participant.SpeechDetected)
                    {
                        string talkingPlayer = participant.DisplayName;

                        if (string.IsNullOrEmpty(talkingPlayer))
                        {
                            talkingPlayer = participant.PlayerId;
                        }

                        if (talkingText != null)
                        {
                            talkingText.text = talkingPlayer + " is talking...";
                        }

                        CancelInvoke(nameof(ClearTalkingText));
                        Invoke(nameof(ClearTalkingText), 1.5f);

                        return;
                    }
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning("Speech detect error: " + e.Message);
        }
    }
    private void ClearTalkingText()
    {
        if (talkingText != null)
        {
            talkingText.text = "";
        }
    }
    private void UpdateMuteButtonText()
    {
        if (muteButtonText == null)
        {
            Debug.LogWarning("Mute Button Text is not assigned in the Inspector.");
            return;
        }

        muteButtonText.text = isMicMuted ? "Unmute" : "Mute";
    }

    public async Task LeaveVoiceChannel()
    {
        if (!globalVivoxJoined)
        {
            return;
        }

        try
        {
            await VivoxService.Instance.LeaveAllChannelsAsync();

            isVivoxReady = false;
            isMicMuted = false;
            globalVivoxJoined = false;

            Debug.Log(testPlayerName + " left Vivox voice channel successfully.");
        }
        catch (Exception e)
        {
            Debug.LogWarning("Failed to leave voice channel: " + e.Message);
        }
    }

    private async void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }

        // Only leave when this real manager is destroyed
        if (globalVivoxJoined)
        {
            await LeaveVoiceChannel();
        }
    }
}