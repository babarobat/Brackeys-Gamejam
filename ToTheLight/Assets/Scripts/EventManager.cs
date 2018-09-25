using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {

    private SoundManager _soundManager;
    private CameraController _cameraController;
    private DangerEffect _dangerEffect;

    private void Start()
    {
        _cameraController = FindObjectOfType<CameraController>();
        _dangerEffect = FindObjectOfType<DangerEffect>();
        _soundManager = SoundManager.instance;
        if (_soundManager ==null)
        {
            throw new System.Exception("EventManager не может найти ссылку на SoundManager");
        }
    }
    public void StartEvent(string eventMeta)
    {
        switch (eventMeta)
        {
            case "PlaySecondPartOfmainTheme":
                StartSecondMusicTheme();
                break;
            case "ShakeCam":
                _cameraController.SwitchCamAnimTrigger("ShakeCam");
                break;
            case "DangerComes":
                _dangerEffect.AnimationTriggerSwitch("DangerComes");
                _cameraController.SwitchCamAnimTrigger("DangerCamShake");
                break;
            case "NoDanger":
                _dangerEffect.AnimationTriggerSwitch("DangerStop");
                _cameraController.SwitchCamAnimTrigger("NormalCam");
                break;

            default:
                break;
        }
    }

    public void StartSecondMusicTheme()
    {
        _soundManager.PlaySecondPartOfmainTheme();
    }
    

    
}
