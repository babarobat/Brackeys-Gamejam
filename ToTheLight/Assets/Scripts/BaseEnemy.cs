using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour {
    
    public float patrolSpeed;
    
    public float agroRadius;

    protected Transform _player;

    protected SoundManager _soundManager;
    protected GameStateManager _gameStateManager;

    protected virtual void Start()
    {
        _player = FindObjectOfType<PlayerController>().transform;
        _soundManager = SoundManager.instance;
        _gameStateManager = FindObjectOfType<GameStateManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.tag == "Player" )
        {
            _gameStateManager.GameOverLoss();
        }
    }


}
