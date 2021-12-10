using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BallState 
{
    Idle,
    Active
}

public class PlayerInput : MonoBehaviour
{
    public BallState _currentBallState;
    public BlockManager BlockManager;
    public Prediction Prediction;
    [SerializeField] Plane _groundPlane;
    [SerializeField] Camera _camera;
    [SerializeField] GameObject _ball;
    [SerializeField] GameObject _ballPrefab;

    [SerializeField] float _speed;

    public List<Ball> AllBalls = new List<Ball>();

    [SerializeField] Vector3 _fromBallToPointer;
    public Transform ExitTransform;

    int _ballIndex;
    float timeBetweenBallCreation = 0.1f;
    float _timer;

    private int _hideBallNumber;
    public int TotalBallCount = 50;

    void Start()
    {
        _groundPlane = new Plane();
        _groundPlane.normal = -Vector3.forward;
        _groundPlane.distance = 0f;

        for (int i = 0; i < TotalBallCount; i++)
        {
            GameObject newBallObject = Instantiate(_ballPrefab);
            Ball newBall = newBallObject.GetComponent<Ball>();
            newBall.enabled = true;
            newBall.PlayerInput = this;
            AllBalls.Add(newBall);
            newBall.Hide();
        }
    }

    void Update()
    {
        if (_currentBallState == BallState.Idle)
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 50f, Color.red);
            float distance;
            _groundPlane.Raycast(ray, out distance);
            Vector3 targetPoint = ray.GetPoint(distance);
            _fromBallToPointer = (targetPoint - _ball.transform.position).normalized;
            Prediction.DrawPath(_fromBallToPointer * _speed);

            if (Input.GetMouseButtonDown(0))
            {
                StartShooting();
            }
        }
    }



    void StartShooting()
    {
        _currentBallState = BallState.Active;
        _ball.SetActive(false);
        Prediction.HideDots();
    }

    void StartIdle()
    {
        _currentBallState = BallState.Idle;
        _ball.SetActive(true);
        Prediction.ShowDots();
        _ballIndex = 0;
        _hideBallNumber = 0;
    }


    private void FixedUpdate()
    {
        if (_currentBallState == BallState.Active)
        {
            if (_ballIndex < AllBalls.Count)
            {
                _timer += Time.deltaTime;
                if (_timer > timeBetweenBallCreation)
                {
                    _timer = 0f;
                    Ball ball = AllBalls[_ballIndex];
                    ball.Shot(_ball.transform.position, _fromBallToPointer * _speed);
                    _ballIndex += 1;
                }
            }
        }
    }


    public void HideBall(Ball ball)
    {
        ball.gameObject.SetActive(false);
        _hideBallNumber += 1;
        if(_hideBallNumber == AllBalls.Count)
        {
            BlockManager.MoveDown();
            StartIdle();
        }
    }
}
