using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SKT_AI : MonoBehaviour {


    public enum State
    {
        Idle,
        Walk,
        Run,
        Death,
        Eat,
    }

    public Transform[] _MovePoints = new Transform[2];
   // public Transform[] _TurnPoints = new Transform[2];
    public float _WaitTimer;
    public float _WalkSpeed;
    public float _RunSpeed;
    [HideInInspector]
    public bool _Dead;
    public GameObject _TargetPosition;
   
   // public Transform _Enemy;
   

    //private Variable
    private State _state;
    private State _Laststate = State.Idle;
    private GameObject _Player;
   
    private int _PointIndex;
    private Animator _animator;
    private float _Speed;
    private bool _playerDead = false;
    private float _WaitTime;
    private float move = 0;
   // private float horizontal = 0;


    void Awake()
    {
        _Player = GameObject.FindGameObjectWithTag("Player");
        _animator = this.GetComponent<Animator>();
    }
    void Start()
    {
        _TargetPosition.transform.position = _MovePoints[0].position;
       // _Enemy.transform.position = this.transform.position;
  
    }

    void OnCollisionEnter2D(Collision2D collision)
     // void OnTriggerEnter2D(Collider2D col)
   {
        if (collision.collider.tag == "turn")
         { 
            Debug.Log("test1");
          transform.Rotate(new Vector3(0, 180, 0));   
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            Debug.Log("test1");
            transform.Rotate(new Vector3(0, 180, 0));
        }

    }

    void FixedUpdate()
    {
        if (_state != State.Death)
        {
            if (_Dead)
            {
                _state = State.Death;
            }
            else
            {
                if ((_Player.transform.position.x > _MovePoints[0].position.x) && (_Player.transform.position.x < _MovePoints[1].position.x))
                {
                    Chase();
                }
                else
                {
                    MoveAround();
                 
                    
                   
                }
            }
        }
    }



    void MoveAround()
    {
        float _Distance = Vector3.Distance(_TargetPosition.transform.position, this.transform.position);
        if (_Distance > 1.0f)
        {
            
            _state = State.Walk;
            //this.transform.LookAt(_TargetPosition.transform);
            //this.transform.Translate(Vector3.forward * _Speed * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, _TargetPosition.transform.position, _Speed * Time.deltaTime);

        }
        else
        {
            if (_Laststate == State.Run)
            {
                _TargetPosition.transform.position = new Vector3(_MovePoints[0].position.x, this.transform.position.y, this.transform.position.z);  //
                _Laststate = State.Idle;
            }
            _state = State.Idle;
            _WaitTime -= Time.deltaTime;
            if (_WaitTime <= 0.0f)
            {
                if (_TargetPosition.transform.position.x == _MovePoints[0].position.x)
                {
                    _TargetPosition.transform.position = _MovePoints[1].position;
                }
                else if (_TargetPosition.transform.position.x == _MovePoints[1].position.x)
                {
                    _TargetPosition.transform.position = _MovePoints[0].position;
                }
                _WaitTime = _WaitTimer;
            }
        }
        JudgeState();
    }
    void Chase()
    {
        if (!_playerDead)
        {
            _state = State.Run;
            _TargetPosition.transform.position = new Vector3(_Player.transform.position.x, this.transform.position.y, this.transform.position.z);

            // this.transform.LookAt(_TargetPosition.transform);
            //transform.Translate(new Vector2(_Speed, 0));
           transform.position = Vector3.MoveTowards(transform.position, _TargetPosition.transform.position, _Speed * Time.deltaTime);
        
}
        else
        {
            _state = State.Eat;
        }
        _Laststate = State.Run;
        JudgeState();
    }

   /* void Turn()
    {
        Debug.Log(_Enemy.transform.position.x);
        Debug.Log(_TurnPoints[0].transform.position.x);
        Debug.Log(_TurnPoints[1].transform.position.x);
        if (_Enemy.transform.position.x==_TurnPoints[0].transform.position.x)
        {
            Debug.Log("test2");
            this.transform.Rotate(new Vector3(0, -180, 0));
        }

        else if (_Enemy.transform.position.x== _TurnPoints[1].position.x)
            {
                Debug.Log("test3");
                this.transform.Rotate(new Vector3(0, -180, 0));
            }
    }*/

    void JudgeState()
    {
        if (_state == State.Idle)
        {
            _animator.SetBool("Run", false);
            _animator.SetBool("Walk", false);
            _animator.SetBool("Eat", false);
            _Speed = 0.0f;
        }
        else if (_state == State.Walk)
        {
            _animator.SetBool("Run", false);
            _animator.SetBool("Eat", false);
            _animator.SetBool("Walk", true);
            _Speed = _WalkSpeed;
        }
        else if (_state == State.Run)
        {
            _animator.SetBool("Run", true);
            _animator.SetBool("Eat", false);
           _animator.SetBool("Walk", false);
            _Speed = _RunSpeed;
        }
        else if (_state == State.Eat)
        {
            _animator.SetBool("Eat", true);
            _animator.SetBool("Run", false);
            _animator.SetBool("Walk", false);
            _Speed = 0.0f;
        }
        else if (_state == State.Death)
        {
            _animator.SetBool("Dead", true);
            _animator.SetBool("Run", false);
            _animator.SetBool("Walk", false);
            _animator.SetBool("Eat", false);
            _Speed = 0.0f;
            Destroy(this.gameObject, 3);
        }
    }

    /*void Update()
    {

        move = horizontal * _Speed;

        if (move > 0)
        {
            this.transform.localScale = new Vector3(Mathf.Abs(this.transform.localScale.x),
                                                  this.transform.localScale.y,
                                                  this.transform.localScale.z);
        }


        if (move < 0)
        {
            this.transform.localScale = new Vector3(-Mathf.Abs(this.transform.localScale.x),
                                                  this.transform.localScale.y,
                                                  this.transform.localScale.z);
        }
        _animator.SetFloat("Walk", Mathf.Abs(move));
    }*/

    
}




