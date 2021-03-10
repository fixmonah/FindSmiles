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
    /// <summary>
    /// Set the front picture
    /// </summary>
    /// <param name="sprite"></param>
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
    /// <summary>
    /// Clear card data
    /// </summary>
    public void Clear()
    {
        _name = "";
        _frontImage = _backImage;
    }
    /// <summary>
    /// Check if the card is front
    /// </summary>
    /// <returns></returns>
    public bool IsFront() 
    { 
        return _isFront;
    }
    /// <summary>
    /// Show card
    /// </summary>
    public void Show() 
    {
        _animator.SetBool("Show", true);
        _animator.SetBool("Front", false);
        IsActive = true;
    }
    /// <summary>
    /// Hide card
    /// </summary>
    public void Hide() 
    {
        _animator.SetBool("Show", false);
        IsActive = false;
    }
    /// <summary>
    /// Turn the card front is up
    /// </summary>
    public void TurnOverFront()
    {
        _animator.SetBool("Front", true);
    }
    /// <summary>
    /// Turn the card back is up
    /// </summary>
    public void TurnOverBack()
    {
        _animator.SetBool("Front", false);
    }

    /// <summary>
    /// Mouse click method for UI button
    /// </summary>
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
