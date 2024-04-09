using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IAscript : MonoBehaviour
{

    enum State 
    {
        Patrolling,
        Chasing,
        Attack 
    }

    State _currentState;

    NavMeshAgent _enemyAgent;

    [SerializeField] Transform _playerTransform;

    [SerializeField] Transform patrolAreaCenter;

    [SerializeField] Vector2 areaSize;

    [SerializeField] float visionRange = 15;

    void Awake ()
    {
        _enemyAgent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        _currentState = State.Patrolling;
    }

    void Update()
    {
        switch (_currentState)
        {
            case State.Patrolling:
                Patrol();
            break;

            case State.Chasing:
                Chase();
            break;

            case State.Attack:
                Ataca();
            break;
        }
    }

    void Patrol()
    {
        if(OnRange() == true)
        {
            _currentState = State.Chasing;
        }
        if (_enemyAgent.remainingDistance < 0.5f)
        {
            SetRandomPoint();
        }
    }

    void Chase()
    {
        _enemyAgent.destination = _playerTransform.position;
        if (_enemyAgent.remainingDistance < 0.5f)
        {
            _currentState = State.Attack;
        }


        if(OnRange() == false)
        {
            _currentState = State.Patrolling;
        }
    }

    void Ataca()
    {
    Debug.Log ("Ataque!");
    _currentState = State.Chasing;
    }


    void SetRandomPoint()
    {
        float _randomX = Random.Range(-areaSize.x /2, areaSize.x/2);
        float _randomZ = Random.Range(-areaSize.y /2, areaSize.y/2);

        Vector3 randomPoint = new Vector3(_randomX, 0f, _randomZ) + patrolAreaCenter.position;
        _enemyAgent.destination = randomPoint;
    }

    bool OnRange()
    {
        
        if (Vector3.Distance(transform.position, _playerTransform.position) <= visionRange)
        {
            return true;
        }

        return false;
    }

     void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(patrolAreaCenter.position, new Vector3(areaSize.x,0, areaSize.y));

    }
}
    
