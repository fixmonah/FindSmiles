using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class Card : MonoBehaviour
{
    [SerializeField] private Image _Image;
    [SerializeField] private Sprite _backImage;
    [SerializeField] private GameManager _gameManager;
    private Sprite _frontImage;
    private string _name;
    private bool _isFront;
    private Animator _animator;
    public bool IsActive { get; private set; }

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetFrontImage(Sprite sprite)
    {
        if (sprite != null)
        {
            _frontImage = sprite;
        }
        else
        {
            Debug.LogError("Sprite is null");
        }
    }
    public void SetName(string name)
    {
        _name = name;
    }
    public string GetName()
    {
        return _name;
    }

    internal void Clear()
    {
        _name = "";
        _frontImage = _backImage;
    }

    public bool IsFront() 
    { 
        return _isFront;
    }

    public void Show() 
    {
        _animator.SetBool("Show", true);
        _animator.SetBool("Front", false);
        IsActive = true;
    }
    public void Hide() 
    {
        _animator.SetBool("Show", false);
        IsActive = false;
    }
    public void TurnOverFront()
    {
        _animator.SetBool("Front", true);
    }
    public void TurnOverBack()
    {
        _animator.SetBool("Front", false);
    }


    public void OnMouseClickUI()
    {
        if (IsActive)
        {
            //_gameManager.OnClickCard(_name, _isFront);
            _gameManager.OnClickCard(this);
        }
    }

    /// <summary>
    /// Animation Events
    /// </summary>
    private void ChangeImageOnBack() 
    {
        _Image.sprite = _backImage;
        _isFront = false;
    }
    /// <summary>
    /// Animation Events
    /// </summary>
    private void ChangeImageOnFront()
    {
        _Image.sprite = _frontImage;
        _isFront = true;
    }
}
