﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painel : MonoBehaviour {

    public Button buttonV;
    public Button buttonF;

    public Cenoura cenouraExe;
    public Pao paoExe;
    public Coins coinsExe;

    public GameObject buttonPlayer;
    public GameObject pickPlayer;
    
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    
    private Animator _animatorPick;
    private Rigidbody2D _rigidbody2DPick;
    
    private static readonly int MoveX = Animator.StringToHash("moveX");
    
    private Vector3 _cenouraPos;
    private Vector3 _paoPos;
    private Vector3 _coinPos;


    // Start is called before the first frame update
    private void Start() {
        _animator = buttonPlayer.GetComponent<Animator>();
        _rigidbody2D = buttonPlayer.GetComponent<Rigidbody2D>();   
        
        _rigidbody2D.velocity = new Vector2(0.1f,0f).normalized * (75 * Time.deltaTime);
        _animator.SetFloat(MoveX, _rigidbody2D.velocity.x);
        
        _animatorPick = pickPlayer.GetComponent<Animator>();
        _rigidbody2DPick = pickPlayer.GetComponent<Rigidbody2D>();

        _rigidbody2DPick.velocity = new Vector2(0.1f,0f).normalized * (75 * Time.deltaTime);
        _animatorPick.SetFloat(MoveX, _rigidbody2DPick.velocity.x);

        _cenouraPos = cenouraExe.transform.position;
        _paoPos = paoExe.transform.position;
        _coinPos = coinsExe.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other) {
      //TODO:FAZER ELE ANDAR E VOLTAR AO ENCONTRAR A BARREIRA   
      if (!other.CompareTag("Player")) return;
      //Player que pisa no botão
      if (_rigidbody2D.velocity.x > 0 && other.name == buttonPlayer.name) { //indo para direita, muda p/ esquerda
          _rigidbody2D.velocity = new Vector2(-0.1f,0f).normalized * (75 * Time.deltaTime);
          _animator.SetFloat(MoveX, _rigidbody2D.velocity.x);
      }
      else if(_rigidbody2D.velocity.x < 0 && other.name == buttonPlayer.name){ //indo para esquerda, muda p/ direita
          _rigidbody2D.velocity = new Vector2(0.1f,0f).normalized * (75 * Time.deltaTime);
          _animator.SetFloat(MoveX, _rigidbody2D.velocity.x);
      }
      //Player que pega os itens
      if (_rigidbody2DPick.velocity.x > 0 && other.name == pickPlayer.name) { //indo para direita, muda p/ esquerda
          _rigidbody2DPick.velocity = new Vector2(-0.1f,0f).normalized * (75 * Time.deltaTime);
          _animatorPick.SetFloat(MoveX, _rigidbody2DPick.velocity.x);
          
          Instantiate(cenouraExe, _cenouraPos, Quaternion.identity).transform.SetParent(gameObject.transform, false);;
          Instantiate(paoExe, _paoPos, Quaternion.identity).transform.SetParent(gameObject.transform, false);;
          Instantiate(coinsExe, _coinPos, Quaternion.identity).transform.SetParent(gameObject.transform, false);;
      }
      else if(_rigidbody2DPick.velocity.x < 0 && other.name == pickPlayer.name){ //indo para esquerda, muda p/ direita
          _rigidbody2DPick.velocity = new Vector2(0.1f,0f).normalized * (75 * Time.deltaTime);
          _animatorPick.SetFloat(MoveX, _rigidbody2DPick.velocity.x);
          
          Instantiate(cenouraExe, _cenouraPos, Quaternion.identity).transform.SetParent(gameObject.transform, false);;
          Instantiate(paoExe, _paoPos, Quaternion.identity).transform.SetParent(gameObject.transform, false);;
          Instantiate(coinsExe, _coinPos, Quaternion.identity).transform.SetParent(gameObject.transform, false);;
      }
    }
}
