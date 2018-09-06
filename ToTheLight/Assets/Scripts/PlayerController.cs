using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]

public class PlayerController : MonoBehaviour
{
    
    /// <summary>
    /// Link to the plsyer's RigidBody component
    /// </summary>
    private Rigidbody2D _playerRb;
    /// <summary>
    /// This force added when player hit space to fly
    /// </summary>
    public float flyForce = 55;
    /// <summary>
    /// This parameter used to multiply movement by vertical and horizontal axes
    /// </summary>
    public float moveSpeed = 2;
    /// <summary>
    /// Used to change players control params
    /// </summary>
    public PlayerCondition playerCondition;

    
    /// <summary>
    /// 
    /// </summary>
    public float transformationTime = 7;
    public float transformationExitTime = 2;

    public bool setRBValuesByEditor = false;
    private const float rbMass = 0.5f;
    private const float rbGravityScale = 0.5f;
    private int _leafCount = 0;
    private bool _isTransformating = false;
    private bool _canClimb = false;
    private SoundManager _soundManager;

    private PlayerAnimationController _animation;
    private bool _isMoving;

    private Vector3 _localScaleFacingRight = new Vector3(1, 1, 1);
    private Vector3 _localScaleFacingLeft = new Vector3(-1, 1, 1);

    public GameObject _youCanFlyNotificationTrigger;
    public GameStateManager _gameStateManager;

    private void Start()
    {
        _soundManager = SoundManager.instance;
        _playerRb = GetComponent<Rigidbody2D>();
        _animation = GetComponent<PlayerAnimationController>();
        _gameStateManager = FindObjectOfType<GameStateManager>();
        transform.tag = "Player";
        if (!setRBValuesByEditor)
        {
            SetRbValues();
        }
    }

    // Update is called once per frame
    void Update ()
    {
        _isMoving = false;

        if (_leafCount == 3)
        {
            StartCoroutine(Transformation());
        }
        switch (playerCondition)
        {  
            case PlayerCondition.caterpillar:
                CaterpillarMove();
                break;
            case PlayerCondition.butterfly:
                ButterflyMove();
                break;
            case PlayerCondition.uncontrollable:
                break;
            default:
                break;
        }

        if (_animation != null)
        {
            _animation.PlayAnimation(_isMoving);
        }
        else
        {
            throw new Exception("Отсутствует аниматор на обьекте " + name);
        }


    }
    void ButterflyMove()
    {     
        var horizontal = Input.GetAxisRaw("Horizontal");
        var jump = Input.GetButtonDown("Jump");
        if (horizontal != 0)
        {
            _isMoving = true;
            transform.Translate(Vector2.right * horizontal * Time.deltaTime * moveSpeed);
        }
        if (jump)
        {
            _playerRb.velocity = Vector2.zero;
            _playerRb.AddForce(Vector2.up * flyForce);
            _isMoving = true;
        }
        

    }
    void CaterpillarMove()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");
        
        if (horizontal!=0 )
        {
            _isMoving = true;
            transform.localScale = new Vector3(horizontal, 1, 1);
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            transform.Translate(Vector2.right * horizontal * Time.deltaTime * moveSpeed);

        }
        if (vertical != 0 && _canClimb)
        {
            _isMoving = true;
            transform.localRotation = Quaternion.Euler(0, 0, 90 * vertical);
            transform.Translate(Vector2.right  * Time.deltaTime * moveSpeed);
        }
    }

    void SetRbValues()
    {
        _playerRb.mass = rbMass;
        _playerRb.gravityScale = rbGravityScale;
    }
    IEnumerator Transformation()
    {
        
        if(!_isTransformating)
        {
            _soundManager.Transformation();
                        
            _isTransformating = true;
            _leafCount = 0;
            playerCondition = PlayerCondition.uncontrollable;

            // Animation
            _animation.SwitchEvolutionStage();

            yield return new WaitForSeconds(transformationTime);

            // Animation
            _animation.SwitchEvolutionStage();
            _soundManager.PlaySound("TransformationEnd");
            yield return new WaitForSeconds(transformationExitTime);
            playerCondition = PlayerCondition.butterfly;

            _youCanFlyNotificationTrigger.SetActive(true);

            // Animation
            _animation.SwitchEvolutionStage();
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag == "Leaf")
        {
            _soundManager.SoundPitch("LeafEat", UnityEngine.Random.Range(0.8f, 1.2f));
            _soundManager.PlaySound("LeafEat");
            _leafCount++;
            Destroy(collision.gameObject);
        }
        if (collision.tag == "Climb" && playerCondition == PlayerCondition.caterpillar)
        {
            _playerRb.isKinematic = true;
            _canClimb = true;
        }
        if (collision.tag == "Bushes")
        {
            
            _gameStateManager.GameOverLoss();
        }
    }
    private void OnTriggerExit2D (Collider2D collision)
    {

        if (collision.tag == "Climb"&&playerCondition == PlayerCondition.caterpillar)
        {
            _playerRb.isKinematic = false;
            _canClimb = false;
        }
    }

}

