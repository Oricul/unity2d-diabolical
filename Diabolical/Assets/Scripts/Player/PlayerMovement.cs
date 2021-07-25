using System.Collections;
using System;
using UnityEngine;

[SelectionBase]

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _body;
    private Animator _animator;
    private SpriteRenderer _sprite;
    private float _horizontalInput, _verticalInput;
    private float _timeToMove = 0.2f;
    private bool _isMoving;
    private Vector2 _origPos, _targetPos;
    public bool _movementEnabled = true;
    [SerializeField] AudioClip _walkSound;
    [SerializeField] float _walkVolume;
    [SerializeField] AudioClip _boundsSound;
    [SerializeField] float _boundsVolume;
    private AudioSource _audioSource;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (_movementEnabled)
        {
            _horizontalInput = Input.GetAxis("Horizontal");
            _verticalInput = Input.GetAxis("Vertical");
            if (!_isMoving)
            {
                if (_horizontalInput > 0.01f)
                {
                    _sprite.flipX = false;
                    StartCoroutine(MovePlayer(Vector2.right));
                } else if (_horizontalInput < -0.01f) {
                    _sprite.flipX = true;
                    StartCoroutine(MovePlayer(Vector2.left));
                } else if (_verticalInput > 0.01f) {
                    StartCoroutine(MovePlayer(Vector2.up));
                } else if (_verticalInput < -0.01f) {
                    StartCoroutine(MovePlayer(Vector2.down));
                }
            }
            // _animator.SetBool("walk", _horizontalInput != 0);
            // _audioSource.PlayOneShot(_walkSound, _walkVolume);
        }
    }

    private IEnumerator MovePlayer(Vector2 direction)
    {
        _isMoving = true;
        float elapsedTime = 0;
        _origPos = transform.position;
        _targetPos = _origPos + direction;
        while(elapsedTime < _timeToMove)
        {
            transform.position = Vector2.Lerp(_origPos, _targetPos, (elapsedTime / _timeToMove));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = _targetPos;
        _isMoving = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Boundary")
        {
            //_audioSource.PlayOneShot(_boundsSound, _boundsVolume);
        }
    }
}