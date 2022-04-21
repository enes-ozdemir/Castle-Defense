using UnityEngine;

namespace Script.Utils
{
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

            _oneShotAudioSource.PlayOneShot(GetAudioClip(sound));
        }

        public static void PlaySound(Sound sound)
        {
            GameObject soundGameObject = new GameObject("Sound");
            AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
            audioSource.PlayOneShot(GetAudioClip(sound));
        }

        private static AudioClip GetAudioClip(Sound sound)
        {
            foreach (GameAssets.SoundAudioClip soundAudioClip in GameAssets.Instance.soundAudioClipArray)
            {
                if (soundAudioClip.sound == sound)
                {
                    return soundAudioClip.audioClip;
                }
            }

            return null;
        }
    }
}