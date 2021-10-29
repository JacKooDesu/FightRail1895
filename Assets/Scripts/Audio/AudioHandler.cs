using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JacDev.Audio
{
    public class AudioHandler : MonoBehaviour   // 寫得極爛，管所有音源
    {
        static AudioHandler singleton = null;
        public static AudioHandler Singleton
        {
            get
            {
                if (singleton != null)
                    return singleton;
                else
                    singleton = FindObjectOfType(typeof(AudioHandler)) as AudioHandler;

                if (singleton == null)
                {
                    GameObject g = new GameObject("AudioHandler");
                    singleton = g.AddComponent<AudioHandler>();
                }

                return singleton;
            }
        }

        public GameObject audioSourcePrefab;

        public GameObject bgmPlayer;

        [Header("Sound List")]
        public SoundList soundList;

        // public Dictionary<string, AudioSource> currentPlayingSounds = new Dictionary<string, AudioSource>();

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            if (singleton == null)
            {
                singleton = this;
            }
            else if (singleton != this)
            {
                Destroy(gameObject);
            }
        }

        public void PlaySound(string name)  // 播2D音效
        {
            SoundSetting sound = soundList.GetSound(name);
            GameObject temp = Instantiate(audioSourcePrefab, transform);
            // DontDestroyOnLoad(temp);    // avoid change scene stop the sound
            AudioSource audioSource = temp.GetComponent<AudioSource>();
            audioSource.clip = sound.clip;
            audioSource.Play();
            StartCoroutine(DestroyAudioSource(sound.clip.length, temp));
            // Destroy(temp, sound.clip.length);

            // currentPlayingSounds.Add(sound.name, audioSource);
        }

        public GameObject PlaySound(string name, float t = -1f)  // 播2D音效
        {
            SoundSetting sound = soundList.GetSound(name);
            GameObject temp = Instantiate(audioSourcePrefab, transform);
            // DontDestroyOnLoad(temp);    // avoid change scene stop the sound
            AudioSource audioSource = temp.GetComponent<AudioSource>();
            audioSource.clip = sound.clip;
            audioSource.Play();
            StartCoroutine(DestroyAudioSource(sound.clip.length, temp));
            // Destroy(temp, sound.clip.length);
            return temp;
            // currentPlayingSounds.Add(sound.name, audioSource);
        }

        public void PlayBgm(int index)
        {
            AudioSource au = bgmPlayer.GetComponent<AudioSource>();
            au.clip = SettingManager.Singleton.BgmSetting.soundSettings[index].clip;
            au.Play();
        }

        public void PlayBgm(string name)
        {
            AudioSource au = bgmPlayer.GetComponent<AudioSource>();
            au.clip = SettingManager.Singleton.BgmSetting.soundSettings[0].clip;
            foreach (SoundSetting s in SettingManager.Singleton.BgmSetting.soundSettings)
            {
                if (s.name == name)
                {
                    au.clip = s.clip;
                    break;
                }
            }
            au.Play();
        }

        public void RandPlayBgm(params int[] avoid)
        {
            int i = 0;
            List<int> avoidList = new List<int>(avoid);
            do
            {
                i = Random.Range(0, SettingManager.Singleton.BgmSetting.soundSettings.Length);
            } while (avoidList.IndexOf(i) != -1);

            PlayBgm(i);
        }

        public void PauseBgm()
        {
            AudioSource au = bgmPlayer.GetComponent<AudioSource>();
            au.Pause();
        }

        // public AudioSource PlayAudio(AudioClip audio, bool loop)
        // {
        //     GameObject temp = Instantiate(speakerPrefab, speaker.transform);
        //     AudioSource audioSource = temp.GetComponent<AudioSource>();
        //     audioSource.clip = audio;

        //     audioSource.Play();

        //     if (loop)
        //         audioSource.loop = loop;
        //     else
        //         StartCoroutine(DestroyAudioSource(audio.length, temp));

        //     return audioSource;
        // }

        // public AudioSource PlayAudio(AudioClip audio, bool loop, Transform target)
        // {
        //     if (target.GetComponentInChildren<AudioSource>())
        //         if (target.GetComponentInChildren<AudioSource>().clip == audio)
        //             return target.GetComponentInChildren<AudioSource>();

        //     GameObject temp = Instantiate(speakerPrefab, target);
        //     temp.transform.localPosition = Vector3.zero;

        //     AudioSource audioSource = temp.GetComponent<AudioSource>();
        //     audioSource.clip = audio;

        //     audioSource.Play();

        //     if (loop)
        //         audioSource.loop = loop;
        //     else
        //         StartCoroutine(DestroyAudioSource(audio.length, temp));

        //     return audioSource;
        // }

        // 摧毀音源
        IEnumerator DestroyAudioSource(float t, GameObject target)
        {
            yield return new WaitForSeconds(t);
            if (target != null)
                Destroy(target);
        }

        // 清空喇叭
        // public void ClearSpeaker()
        // {
        //     for (int i = speaker.transform.childCount; i > 0; --i)
        //     {
        //         Destroy(speaker.transform.GetChild(i - 1).gameObject);
        //     }
        // }

        public void StopAll()
        {
            foreach (AudioSource a in FindObjectsOfType<AudioSource>())
            {
                a.Stop();
            }
        }

        // public void StopCurrent(string name)
        // {
        //     currentPlayingSounds[name].Stop();
        // }

        // public void Windup(float f, string name)
        // {
        //     currentPlayingSounds[name].pitch = f;
        // }

        public AudioSource GetSpeakerAudioSource(AudioClip ac)
        {
            // for (int i = speaker.transform.childCount; i > 0; --i)
            // {
            //     if (speaker.transform.GetChild(i - 1).GetComponent<AudioSource>().clip == ac)
            //         return speaker.transform.GetChild(i - 1).GetComponent<AudioSource>();
            // }

            foreach (AudioSource a in FindObjectsOfType<AudioSource>())
            {
                if (a.clip == ac)
                    return a;
            }

            return null;
        }

        // public AudioSource GetSoundAudioSource(AudioClip ac)
        // {
        //     for (int i = GameHandler.Singleton.player.transform.childCount; i > 0; --i)
        //     {
        //         if (GameHandler.Singleton.player.transform.GetChild(i - 1).GetComponent<AudioSource>().clip == ac)
        //             return GameHandler.Singleton.player.transform.GetChild(i - 1).GetComponent<AudioSource>();
        //     }

        //     return null;
        // }
    }
}
