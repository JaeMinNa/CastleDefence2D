using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource _mainBGMAudioSource;
    private AudioSource _mainSFXAuidoSource;
    private AudioSource[] _etcSFXAudioSources;
    private Dictionary<string, AudioClip> _bgm;
    private Dictionary<string, AudioClip> _sfx;
    private int _index;
    private GameObject _audioSources;
    private GameObject _player;

    [Header("Settings")]
    [SerializeField] private float _maxDistance = 50f;
    [Range(0f, 1f)] public float StartVolume = 0.1f;

    public void Init()
    {
        // 초기 셋팅
        _bgm = new Dictionary<string, AudioClip>();
        _sfx = new Dictionary<string, AudioClip>();
        _audioSources = Camera.main.gameObject.transform.GetChild(0).gameObject;
        _mainBGMAudioSource = _audioSources.transform.GetChild(0).GetComponent<AudioSource>();
        _mainSFXAuidoSource = _audioSources.transform.GetChild(1).GetComponent<AudioSource>();
        _etcSFXAudioSources = new AudioSource[4];
        for (int i = 0; i < 4; i++)
        {
            _etcSFXAudioSources[i] = _audioSources.transform.GetChild(i + 2).GetComponent<AudioSource>();
        }

        _mainBGMAudioSource.loop = true;
        _mainBGMAudioSource.volume = StartVolume;
        _mainSFXAuidoSource.playOnAwake = false;
        _mainSFXAuidoSource.volume = StartVolume;
        for (int i = 0; i < _etcSFXAudioSources.Length; i++)
        {
            _etcSFXAudioSources[i].playOnAwake = false;
            _etcSFXAudioSources[i].volume = StartVolume;
        }

        _player = GameManager.I.PlayerManager.Player;

        // BGM

        // SFX
        _sfx.Add("Gauge", Resources.Load<AudioClip>("Sound/SFX/UI/Gauge"));
        _sfx.Add("CastleHit", Resources.Load<AudioClip>("Sound/SFX/Castle/CastleHit"));
        _sfx.Add("Sword", Resources.Load<AudioClip>("Sound/SFX/Skills/Sword"));
        _sfx.Add("Strike", Resources.Load<AudioClip>("Sound/SFX/Skills/Strike"));
        _sfx.Add("Fireball", Resources.Load<AudioClip>("Sound/SFX/Skills/Fireball"));
        _sfx.Add("FireballExplosion", Resources.Load<AudioClip>("Sound/SFX/Skills/FireballExplosion"));
        _sfx.Add("BoltStrike", Resources.Load<AudioClip>("Sound/SFX/Skills/BoltStrike"));
        _sfx.Add("BoltStrikeExplosion", Resources.Load<AudioClip>("Sound/SFX/Skills/BoltStrikeExplosion"));
        _sfx.Add("Danger", Resources.Load<AudioClip>("Sound/SFX/UI/Danger"));
    }

    // 메모리 해제
    public void Release()
    {

    }

    // 다른 오브젝트에서 출력되는 사운드
    public void StartSFX(string name, Vector3 position)
    {
        _index = _index % _etcSFXAudioSources.Length;

        float distance = Vector2.Distance(position, _player.transform.position);
        float volume = 1f - (distance / _maxDistance);
        _etcSFXAudioSources[_index].volume = Mathf.Clamp01(volume) * StartVolume;
        _etcSFXAudioSources[_index].PlayOneShot(_sfx[name]);

        _index++;
    }

    // Player에서 출력되는 사운드
    public void StartSFX(string name)
    {
        _mainSFXAuidoSource.PlayOneShot(_sfx[name]);
    }

    public void StartBGM(string name)
    {
        _mainBGMAudioSource.Stop();
        _mainBGMAudioSource.clip = _bgm[name];
        _mainBGMAudioSource.Play();
    }

    public void StopBGM(string name)
    {
        if (_mainBGMAudioSource != null) _mainBGMAudioSource.Stop();
    }
}