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
        _player = GameManager.I.PlayerManager.Player;
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

        // BGM
        _bgm.Add("Loby", Resources.Load<AudioClip>("Sound/BGM/Loby"));
        _bgm.Add("BattleMap0", Resources.Load<AudioClip>("Sound/BGM/BattleMap0"));

        // SFX
        _sfx.Add("Gauge", Resources.Load<AudioClip>("Sound/SFX/UI/Gauge"));
        _sfx.Add("CastleHit", Resources.Load<AudioClip>("Sound/SFX/Castle/CastleHit"));
        _sfx.Add("Sword", Resources.Load<AudioClip>("Sound/SFX/Skills/Sword"));
        _sfx.Add("Strike", Resources.Load<AudioClip>("Sound/SFX/Skills/Strike"));
        _sfx.Add("Fireball", Resources.Load<AudioClip>("Sound/SFX/Skills/Fireball"));
        _sfx.Add("FireballExplosion", Resources.Load<AudioClip>("Sound/SFX/Skills/FireballExplosion"));
        _sfx.Add("BoltShower", Resources.Load<AudioClip>("Sound/SFX/Skills/BoltStrike"));
        _sfx.Add("BoltShowerExplosion", Resources.Load<AudioClip>("Sound/SFX/Skills/BoltStrikeExplosion"));
        _sfx.Add("Danger", Resources.Load<AudioClip>("Sound/SFX/UI/Danger"));
        _sfx.Add("ArrowShoot", Resources.Load<AudioClip>("Sound/SFX/Arrow/ArrowShoot"));
        _sfx.Add("ArrowHit", Resources.Load<AudioClip>("Sound/SFX/Arrow/ArrowHit"));
        _sfx.Add("Coin", Resources.Load<AudioClip>("Sound/SFX/Item/Coin"));
        _sfx.Add("ButtonClick", Resources.Load<AudioClip>("Sound/SFX/UI/ButtonClick"));
        _sfx.Add("ButtonExp", Resources.Load<AudioClip>("Sound/SFX/UI/ButtonExp"));
        _sfx.Add("LevelUp", Resources.Load<AudioClip>("Sound/SFX/UI/LevelUp"));
        _sfx.Add("ButtonClickMiss", Resources.Load<AudioClip>("Sound/SFX/UI/ButtonClickMiss"));
        _sfx.Add("FireSword", Resources.Load<AudioClip>("Sound/SFX/Skills/FireballExplosion"));
        _sfx.Add("BoltSword", Resources.Load<AudioClip>("Sound/SFX/Skills/BoltStrikeExplosion"));
        _sfx.Add("WindSword", Resources.Load<AudioClip>("Sound/SFX/Skills/WindSword"));
        _sfx.Add("BloodStrike", Resources.Load<AudioClip>("Sound/SFX/Skills/BloodStrike"));
        _sfx.Add("HolyStrike", Resources.Load<AudioClip>("Sound/SFX/Skills/HolyStrike"));
        _sfx.Add("MagmaStrike", Resources.Load<AudioClip>("Sound/SFX/Skills/MagmaStrike"));
        _sfx.Add("FireShower", Resources.Load<AudioClip>("Sound/SFX/Skills/Fireball"));
        _sfx.Add("FireShowerExplosion", Resources.Load<AudioClip>("Sound/SFX/Skills/FireballExplosion"));
        _sfx.Add("Boltball", Resources.Load<AudioClip>("Sound/SFX/Skills/BoltStrike"));
        _sfx.Add("BoltballExplosion", Resources.Load<AudioClip>("Sound/SFX/Skills/BoltStrikeExplosion"));
        _sfx.Add("Darkball", Resources.Load<AudioClip>("Sound/SFX/Skills/Darkball"));
        _sfx.Add("DarkballExplosion", Resources.Load<AudioClip>("Sound/SFX/Skills/DarkballExplosion"));
        _sfx.Add("DarkRain", Resources.Load<AudioClip>("Sound/SFX/Skills/Darkball"));
        _sfx.Add("DarkRainExplosion", Resources.Load<AudioClip>("Sound/SFX/Skills/DarkballExplosion"));
        _sfx.Add("BlueFireball", Resources.Load<AudioClip>("Sound/SFX/Skills/Fireball"));
        _sfx.Add("BlueFireballExplosion", Resources.Load<AudioClip>("Sound/SFX/Skills/MagmaStrike"));
        _sfx.Add("BlueFireRain", Resources.Load<AudioClip>("Sound/SFX/Skills/Fireball"));
        _sfx.Add("BlueFireRainExplosion", Resources.Load<AudioClip>("Sound/SFX/Skills/MagmaStrike"));
        _sfx.Add("Tornadoball", Resources.Load<AudioClip>("Sound/SFX/Skills/Tornadoball"));
        _sfx.Add("TornadoballExplosion", Resources.Load<AudioClip>("Sound/SFX/Skills/TornadoballExplosion"));
        _sfx.Add("TornadoShower", Resources.Load<AudioClip>("Sound/SFX/Skills/Tornadoball"));
        _sfx.Add("TornadoShowerExplosion", Resources.Load<AudioClip>("Sound/SFX/Skills/TornadoballExplosion"));
        _sfx.Add("Laser", Resources.Load<AudioClip>("Sound/SFX/Skills/Laser"));
        _sfx.Add("LaserExplosion", Resources.Load<AudioClip>("Sound/SFX/Skills/LaserExplosion"));
        _sfx.Add("LaserBomb", Resources.Load<AudioClip>("Sound/SFX/Skills/Laser"));
        _sfx.Add("LaserBombExplosion", Resources.Load<AudioClip>("Sound/SFX/Skills/LaserExplosion"));
        _sfx.Add("UIClick", Resources.Load<AudioClip>("Sound/SFX/UI/UIClick"));
        _sfx.Add("Equip", Resources.Load<AudioClip>("Sound/SFX/UI/Equip"));
        _sfx.Add("Buy", Resources.Load<AudioClip>("Sound/SFX/UI/Buy"));
        _sfx.Add("GetSkill", Resources.Load<AudioClip>("Sound/SFX/UI/GetSkill"));
        _sfx.Add("GameOver", Resources.Load<AudioClip>("Sound/SFX/UI/GameOver"));
        _sfx.Add("Potion", Resources.Load<AudioClip>("Sound/SFX/Item/Potion"));
        _sfx.Add("SkillAttribute", Resources.Load<AudioClip>("Sound/SFX/UI/SkillAttribute"));
        _sfx.Add("Nuckback", Resources.Load<AudioClip>("Sound/SFX/Enemy/Nuckback"));
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