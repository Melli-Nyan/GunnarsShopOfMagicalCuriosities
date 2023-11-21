using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor.Animations;

public class script : MonoBehaviour
{
    [SerializeField] private Animator _characterAnimator;
    [SerializeField] private CharacterType _characterType;
    public CharacterType _currentType = CharacterType.Archer;

    [SerializeField] private bool _isLookingForward;
    [SerializeField] private bool _isWalking;


    // Start is called before the first frame update
    void Start()
    {
        UpdateAnimator();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAnimator();
    }

    public void SetCharacterType(CharacterType type)
    {
        _characterType = type;
    }

    public void SetFowardLooking(bool isLookingForward)
    {
        _isLookingForward = isLookingForward;
    }

    public void SetWalking(bool isWalking)
    {
        _isWalking = isWalking;
    }

    private void UpdateAnimator()
    {
        if (_characterType != _currentType)
        {
            SetAnimatorLayer(_currentType, 0f);
            SetAnimatorLayer(_characterType, 1f);
            _currentType = _characterType;
        }
        _characterAnimator.SetBool("isLookingForward", _isLookingForward);
        _characterAnimator.SetBool("isWalking", _isWalking);
    }

    private void SetAnimatorLayer(CharacterType type, float weight)
    {
        int layerIndex = 0;
        switch (type)
        {
            case CharacterType.Archer:
                layerIndex = _characterAnimator.GetLayerIndex("ArcherLayer");
                break;
            case CharacterType.Knight:
                layerIndex = _characterAnimator.GetLayerIndex("KnightLayer");
                break;
            case CharacterType.Monk:
                layerIndex = _characterAnimator.GetLayerIndex("MonkLayer");
                break;
            case CharacterType.Wizard:
                layerIndex = _characterAnimator.GetLayerIndex("WizardLayer");
                break;
        }
        _characterAnimator.SetLayerWeight(layerIndex, weight);
    }
}

public enum CharacterType
{
    Archer,
    Knight,
    Wizard,
    Monk
}