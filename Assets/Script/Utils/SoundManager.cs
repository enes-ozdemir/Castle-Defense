using UnityEngine;

public static class SoundManager
{
    public enum Sound
    {
        BackgroundMusic,
        BattleMusic,
        ArrowSound,
        EnemyHit,
        GoldDrop,
        EnemyWalk,
        EnemySound,
        BombExplosion,
        SkillExplosion,
        BossGrowl,
        Win,
        Loss,
        DiamondDrop,
        SpearHit,
        EnemyAttackToCastle,
    }

    private static GameObject _oneShotGameObject;
    private static AudioSource _oneShotAudioSource;

    public static void PlaySound(Sound sound, float volume, bool isLoop)
    {
        if (_oneShotGameObject == null)
        {
            _oneShotGameObject = new GameObject("Sound");
            _oneShotAudioSource = _oneShotGameObject.AddComponent<AudioSource>();
            _oneShotAudioSource.volume = volume;
            _oneShotAudioSource.loop = isLoop;
        }

        _oneShotAudioSource.PlayOneShot(GetAudioclip(sound));
    }

    public static void PlaySound(Sound sound)
    {
        GameObject soundGameobject = new GameObject("Sound");
        AudioSource audioSource = soundGameobject.AddComponent<AudioSource>();
        audioSource.PlayOneShot(GetAudioclip(sound));
    }

    private static AudioClip GetAudioclip(Sound sound)
    {
        foreach (GameAssets.SoundAudioClip soundAudioclip in GameAssets.Instance.soundAudioClipArray)
        {
            if (soundAudioclip.sound == sound)
            {
                return soundAudioclip.audioClip;
            }
        }

        return null;
    }
}