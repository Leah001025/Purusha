using System.Collections;
using System.Collections.Generic;
using UnityEditor.AddressableAssets.HostingServices;
using UnityEngine;

public class SoundManager : SingleTon<SoundManager>
{
    [Header("AudioClip")]
    [SerializeField] private AudioSource bgmAudioSource;
    [SerializeField] private AudioSource attackAudioSource;
    [SerializeField] private AudioSource effectAudioSource;
    [SerializeField] private AudioSource environmentAudioSource;
    [SerializeField] private AudioSource buttonAudioSource;

    private Dictionary<string, AudioClip> _bgmClips = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioClip> _attackClips = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioClip> _effectClips = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioClip> _environmentClips = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioClip> _buttonClips = new Dictionary<string, AudioClip>();

    public GameObject Player;
    private bool isAudioIn;

    protected override void Awake()
    {
        base.Awake();
        var _rsc = Resources.Load("Sound/MainAudioSource") as GameObject;
        var _obj = Instantiate(_rsc, gameObject.transform);
        bgmAudioSource = _obj.transform.GetChild(0).GetComponent<AudioSource>();
        attackAudioSource = _obj.transform.GetChild(1).GetComponent<AudioSource>();
        effectAudioSource = _obj.transform.GetChild(2).GetComponent<AudioSource>();
        environmentAudioSource = _obj.transform.GetChild(3).GetComponent<AudioSource>();
        buttonAudioSource = _obj.transform.GetChild(4).GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (bgmAudioSource.volume < 1 && isAudioIn)
        {
            bgmAudioSource.volume += Time.deltaTime * 0.1f;
        }
        if (bgmAudioSource.volume > 0 && !isAudioIn)
        {
            bgmAudioSource.volume -= Time.deltaTime * 1f;
        }
        if (Player != null)
        {
            gameObject.transform.position = Player.transform.position;
        }
        else
        {
            if (gameObject.transform.position.x != 0)
            {
                gameObject.transform.position = new Vector3(0, 0, 0);
            }
        }
    }
    public void SceneAudioStart(string sceneName)
    {
        AudioClear();
        BgmAudio(sceneName);
    }
    public void SceneAudioExit()
    {
        isAudioIn = false;
    }
    private void AudioClear()
    {
        if (_bgmClips.Count != 0)
        {
            if (bgmAudioSource.isPlaying) bgmAudioSource.Stop();
            _bgmClips.Clear();
        }
        if (_attackClips.Count != 0)
        {
            if (attackAudioSource.isPlaying) attackAudioSource.Stop();
            _attackClips.Clear();
        }
        if (_effectClips.Count != 0)
        {
            if (effectAudioSource.isPlaying) effectAudioSource.Stop();
            _effectClips.Clear();
        }
        if (_environmentClips.Count != 0)
        {
            if (environmentAudioSource.isPlaying) environmentAudioSource.Stop();
            _environmentClips.Clear();
        }
        if (_buttonClips.Count != 0)
        {
            if (buttonAudioSource.isPlaying) buttonAudioSource.Stop();
            _buttonClips.Clear();
        }
    }
    public void BgmAudio(string clip)
    {
        isAudioIn = true;
        if (bgmAudioSource.isPlaying)
        {
            bgmAudioSource.Stop();
        }
        if (_bgmClips.ContainsKey(clip) == false)
        {
            var _clip = Resources.Load<AudioClip>($"Sound/Bgm/{clip}");
            _bgmClips.Add(clip, _clip);
        }
        bgmAudioSource.clip = _bgmClips[clip];
        bgmAudioSource.volume = 0f;
        bgmAudioSource.Play();
    }
    public void AttackAudio(int id, string num)
    {
        if (attackAudioSource.isPlaying)
        {
            attackAudioSource.Stop();
        }
        if (_attackClips.ContainsKey(id + num) == false)
        {
            var _clip = Resources.Load<AudioClip>($"Sound/Attack/{id}/{num}");
            _attackClips.Add(id + num, _clip);
        }
        attackAudioSource.clip = _attackClips[id + num];
        attackAudioSource.Play();
    }
    public void ButtonAudio(string clip)
    {
        if (buttonAudioSource.isPlaying)
        {
            buttonAudioSource.Stop();
        }
        if (_buttonClips.ContainsKey(clip) == false)
        {
            var _clip = Resources.Load<AudioClip>($"Sound/Button/{clip}");
            _buttonClips.Add(clip, _clip);
        }
        buttonAudioSource.clip = _buttonClips[clip];
        buttonAudioSource.Play();
    }
}
