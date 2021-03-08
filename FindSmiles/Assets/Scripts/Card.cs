using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] private Image _Image;
    [SerializeField] private Sprite _backImage;
    private Sprite _frontImage;
    private string _name;
    private bool _isFront;
    private Animator _animator;

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
    public bool IsFront() 
    { 
        return _isFront;
    }

    public void Show() 
    {
        _animator.SetBool("Show", true);
        _animator.SetBool("Front", false);
    }
    public void Hide() 
    {
        _animator.SetBool("Show", false);
    }
    public void TurnOverFront()
    {
        _animator.SetBool("Front", true);
    }
    public void TurnOverBack()
    {
        _animator.SetBool("Front", false);
    }

    private void ChangeImageOnBack() 
    {
        _Image.sprite = _backImage;
        _isFront = false;
    }
    private void ChangeImageOnFront()
    {
        _Image.sprite = _frontImage;
        _isFront = true;
    }
}
