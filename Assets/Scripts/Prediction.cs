using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Prediction : MonoBehaviour
{
    [SerializeField] GameObject _ball;
    [SerializeField] GameObject _moveToPredictionScene;
    [SerializeField] GameObject _dotPrefab;

    public List<GameObject> DotsList = new List<GameObject>();

    private GameObject _ballInPhysicsScene;
    private PhysicsScene2D _physicsScene;

    void Start()
    {
        for (int i = 0; i < 50; i++)
        {
            GameObject newDot = Instantiate(_dotPrefab, transform);
            DotsList.Add(newDot);
        }

        Scene predictionScene = SceneManager.CreateScene("Prediction", new CreateSceneParameters(LocalPhysicsMode.Physics2D));
        _physicsScene = predictionScene.GetPhysicsScene2D();

        _ballInPhysicsScene = Instantiate(_ball);
        _ballInPhysicsScene.GetComponent<Rigidbody2D>().isKinematic = false;
        _ballInPhysicsScene.GetComponent<Renderer>().enabled = false;
        _ballInPhysicsScene.GetComponent<Ball>().TrailRenderer.enabled = false;

        SceneManager.MoveGameObjectToScene(_ballInPhysicsScene, predictionScene);
        SceneManager.MoveGameObjectToScene(_moveToPredictionScene, predictionScene);
        
    }

    public void DrawPath(Vector3 velocity)
    {
        _ballInPhysicsScene.transform.position = _ball.transform.position;
        _ballInPhysicsScene.transform.rotation = _ball.transform.rotation;
        _ballInPhysicsScene.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        _ballInPhysicsScene.GetComponent<Rigidbody2D>().angularVelocity = 0f;

        _ballInPhysicsScene.GetComponent<Rigidbody2D>().velocity = velocity;

        for (int i = 0; i < DotsList.Count; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                _physicsScene.Simulate(Time.fixedDeltaTime);
            }        
            DotsList[i].transform.position = _ballInPhysicsScene.transform.position;
        }
    }

    public void HideDots()
    {
        gameObject.SetActive(false);
    }

    public void ShowDots()
    {
        gameObject.SetActive(true);
    }

}


