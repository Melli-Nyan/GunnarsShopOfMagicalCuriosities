using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;

    [SerializeField] private AudioSource _backgroundMusic;
    [SerializeField] private AudioSource _fx00;
    [SerializeField] private AudioSource _fx01;
    [SerializeField] private AudioSource _fx02;
    private List<AudioSource> _fxSources = new List<AudioSource>();

    [SerializeField] private AudioClip _music;
    [SerializeField] private AudioClip _shuffle;
    [SerializeField] private AudioClip _cardSwoosh;
    [SerializeField] private AudioClip _bell;
    [SerializeField] private AudioClip _footsteps;
    [SerializeField] private AudioClip _refuse;
    [SerializeField] private AudioClip _owl;
    [SerializeField] private AudioClip _cardPlace;

    [SerializeField] private float _musicStartDelay;
    
    public static void PlayCardSwoosh()
    {
        _instance.PlayFX(_instance._cardSwoosh);
    }

    public static void PlayShuffle()
    {
        _instance.PlayFX(_instance._shuffle);
    }

    public static void PlayBell()
    {
        _instance.PlayFX(_instance._bell);
    }

    public static void PlayRefuse()
    {
        _instance.PlayFX(_instance._refuse);
    }

    public static void PlayOwl()
    {
        _instance.PlayFX(_instance._owl);
    }

    public static void PlayFootsteps()
    {
        _instance.PlayFX(_instance._footsteps);
    }

    public static void PlayCardPlace()
    {
        _instance.PlayFX(_instance._cardPlace);
    }

    public static void StopFootsteps()
    {
        _instance.StopSpecificFX(_instance._footsteps);
    }

    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else 
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        _fxSources.Add(_fx00);
        _fxSources.Add(_fx01);
        _fxSources.Add(_fx02);
        StartCoroutine(StartMusic());
    }

    public static void RestartMusic()
    {
        _instance.StartCoroutine(_instance.StartMusic());
    }

    private IEnumerator StartMusic()
    {
        yield return new WaitForSeconds(_musicStartDelay);
        _backgroundMusic.clip = _music;
        _backgroundMusic.Play();
    }

    public static void StopFX()
    {
        for (int i = 0; i < _instance._fxSources.Count; i++)
        {
            _instance._fxSources[i].Stop();
        }
    }

    private void StopSpecificFX(AudioClip clip)
    {
        for (int i = 0; i < _fxSources.Count; i++)
        {
            if (_fxSources[i].clip == null)
            {
                return;
            }
            else if (_fxSources[i].clip.ToString().Equals(clip.ToString()))
            {
                _fxSources[i].Stop();
            }
        }
    }

    private void PlayFX(AudioClip clip)
    {
        for (int i = 0; i < _fxSources.Count; i++)
        {
            if (!_fxSources[i].isPlaying)
            {
                _fxSources[i].clip = clip;
                _fxSources[i].Play();
                return;
            }
        }
        return;
    }

}
