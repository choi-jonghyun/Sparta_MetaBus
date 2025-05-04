using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopSoundManager : MonoBehaviour
{
    public static TopSoundManager instance;

    [SerializeField][Range(0f, 1f)] private float soundEffectVolume; // ȿ���� ����
    [SerializeField][Range(0f, 1f)] private float soundEffectPitchVariance; // ȿ���� ��ġ ������
    [SerializeField][Range(0f, 1f)] private float musicVolume; // ��� ���� ����

    private AudioSource musicAudioSource; // ��� ���ǿ� AudioSource
    public AudioClip musicClip; // �⺻ ��� ���� Ŭ��

    public TopSoundSource soundSourcePrefab; // ȿ������ ����� ������ (TopSoundSource ���)
    private void Awake()
    {
        instance = this;

        // ����� ����� AudioSource ����
        musicAudioSource = GetComponent<AudioSource>();
        musicAudioSource.volume = musicVolume;
        musicAudioSource.loop = true;
    }

    private void Start()
    {
        // �⺻ ��� ���� ����
        ChangeBackGroundMusic(musicClip);
    }

    // ��� ������ �ٸ� Ŭ������ ��ü�ϴ� �Լ�
    public void ChangeBackGroundMusic(AudioClip clip)
    {
        musicAudioSource.Stop();
        musicAudioSource.clip = clip;
        musicAudioSource.Play();
    }

    // ȿ������ ����ϴ� ���� �޼��� (�ܺ� ��𼭵� ȣ�� ����)
    public static void PlayClip(AudioClip clip)
    {
        // TopSoundSource ������ �ν��Ͻ� ���� �� ���
        TopSoundSource obj = Instantiate(instance.soundSourcePrefab);
        TopSoundSource soundSource = obj.GetComponent<TopSoundSource>();
        soundSource.Play(clip, instance.soundEffectVolume, instance.soundEffectPitchVariance);
    }
}