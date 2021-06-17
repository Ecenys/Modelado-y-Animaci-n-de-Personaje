using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private InputController _input;
    private Vector2 _movement;

    private float _rotSpeed = 5f;
    private float _speed = 0.2f;

    private Animator _anim;

    void Awake()
    {
        _input = new InputController();        
    }

    // Start is called before the first frame update
    private void Start()
    {
        _anim = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        _movement = _input.Player.Movement.ReadValue<Vector2>();

        //Andar
        if (_movement.y != 0)
        {
            _anim.SetBool("isWalking", true);
        }
        else
        {
            _anim.SetBool("isWalking", false);
        }

        //Correr
        if(_input.Player.Run.ReadValue<float>() > 0)
        {
            _anim.SetBool("isRunning", true);
        }
        else {
            _anim.SetBool("isRunning", false);
        }

        //Saltar
        if (_input.Player.Jump.ReadValue<float>() > 0)
        {
            _anim.SetBool("isJumping", true);
        }
        else
        {
            _anim.SetBool("isJumping", false);
        }

        if (_input.Player.Attack.triggered && !_anim.GetCurrentAnimatorStateInfo(0).IsName("SwordAttack")
            && !_anim.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            _anim.SetTrigger("Attack");
        }
    }
    private void FixedUpdate()
    {
        if (_input.Player.Movement.ReadValue<Vector2>().y > 0 && !_anim.GetCurrentAnimatorStateInfo(0).IsName("SwordAttack"))
        {
            Vector3 rotation = new Vector3(0, _movement.x * _rotSpeed, 0);
            transform.Rotate(rotation);
        }
        Transform playerTransform = transform;
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        playerTransform.position += forward * (_speed * _movement.y);
    }
}
