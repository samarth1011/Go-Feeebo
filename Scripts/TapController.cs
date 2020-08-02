﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;



[RequireComponent(typeof(Rigidbody2D))]
public class TapController : MonoBehaviour

{
    public delegate void PlayerDelegate();
    public static event PlayerDelegate OnPlayerDied;
    public static event PlayerDelegate OnPlayerScored;
    public float tapForce = 10;
    public float tiltSmooth = 5;
    public Vector3 startPos;
    public AudioSource tapAudio;
    public AudioSource scoreAudio;
    public AudioSource dieAudio;
    public AudioSource backAudio;



    private new Rigidbody2D rigidbody;
    Quaternion downRotation;
    Quaternion forwardRotation;
    GameManager game;
    void Start()
    {
        
        rigidbody = GetComponent<Rigidbody2D>();
        downRotation = Quaternion.Euler(0, 0,-90);
        forwardRotation = Quaternion.Euler(0, 0,35);
        game = GameManager.Instance;
        rigidbody.simulated = false;
    }
    void OnEnable()
    {
        GameManager.OnGameStarted += OnGameStarted;
        GameManager.OnGameOverConfirmed += OnGameOverConfirmed;
    }
    void OnDisable()
    {
        GameManager.OnGameStarted -= OnGameStarted;
        GameManager.OnGameOverConfirmed -= OnGameOverConfirmed;
    }
    void OnGameStarted()
    {
        backAudio.Play();
        rigidbody.velocity = Vector3.zero;
        rigidbody.simulated = true;
    }
    void OnGameOverConfirmed()
    {
        transform.localPosition = startPos;
        transform.rotation = Quaternion.identity;
        backAudio.Stop();
    }
    void Update()
    {
        
        if (game.GameOver) return;
        if (game.GameOver)
        {
            backAudio.Stop();
        }
        if (Input.GetMouseButtonDown(0))
        {
            tapAudio.Play();
            
            transform.rotation = forwardRotation;
            rigidbody.velocity = Vector3.zero;
            rigidbody.AddForce(Vector2.up * tapForce, ForceMode2D.Force);
        }
        transform.rotation = Quaternion.Lerp(transform.rotation, downRotation, tiltSmooth * Time.deltaTime);
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "ScoreZone")
        {
            OnPlayerScored();
            scoreAudio.Play();
        }
        if (col.gameObject.tag == "DeadZone")
        {
            rigidbody.simulated = false;
            OnPlayerDied();
            dieAudio.Play();
        }
    }
     

    // Start is called before the first frame update
}
