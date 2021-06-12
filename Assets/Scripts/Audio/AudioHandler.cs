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

        [Header("Sound List")]
        public SoundList soundList;

        // public Dictionary<string, AudioSource> currentPlayingSounds = new Dictionary<string, AudioSource>();

        public void PlaySound(string name)  // 播2D音效
        {
            SoundSetting sound = soundList.GetSound(name);
            GameObject temp = Instantiate(audioSourcePrefab);
            DontDestroyOnLoad(temp);    // avoid change scene stop the sound
            AudioSource audioSource = temp.GetComponent<AudioSource>();
            audioSource.clip = sound.clip;
            audioSource.Play();
            // StartCoroutine(DestroyAudioSource(sound.clip.length, temp));
            Destroy(temp, sound.clip.length);

            // currentPlayingSounds.Add(sound.name, audioSource);
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
