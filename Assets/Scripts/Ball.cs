using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rigidbody2D;
    public PlayerInput PlayerInput;
    public TrailRenderer TrailRenderer;
    public AudioSource HitSound;
    [SerializeField] BallState _currentBallState; 
    private void Update()
    {
        if(transform.position.y < PlayerInput.ExitTransform.position.y)
        {
            PlayerInput.HideBall(this);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(PlayerInput._currentBallState == BallState.Active)
        {
            HitSound.Play();
        }
        
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Shot(Vector3 startPosition, Vector3 velocity)
    {
        transform.position = startPosition;
        gameObject.SetActive(true);
        _rigidbody2D.isKinematic = false;
        _rigidbody2D.velocity = velocity;
        

    }

}
