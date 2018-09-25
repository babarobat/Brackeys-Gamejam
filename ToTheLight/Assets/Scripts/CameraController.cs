using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public Vector3 offset;
    public float smoothSpeed;
    public Transform lowerCamPos;

    private Transform _player;

    private float _cameraPositionZ;

    
    Camera _camera;
    
    Animator _anim;
    
    

    private void Start()
    {
         
        
        _anim = GetComponent<Animator>();
        
        _player = FindObjectOfType<PlayerController>().transform;
        _cameraPositionZ = transform.position.z;
        transform.position = new Vector3(_player.position.x, _player.position.y, _cameraPositionZ) + offset; ;
    }
    private void LateUpdate()
    {
        FollowPlayer();
        
    }
    private void FollowPlayer()
    {
        Vector3 desiredPos = new Vector3 (_player.position.x,
                                          Mathf.Clamp(_player.position.y,lowerCamPos.position.y, _player.position.y),
                                          _cameraPositionZ) + offset;
        Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPos;
    }


   
    
    public void SwitchCamAnimTrigger(string animName)
    {
        _anim.SetTrigger(animName);
    }
}
