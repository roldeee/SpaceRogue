using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioEventManager : MonoBehaviour
{
    public EventSound3D eventSound3DPrefab;
    public AudioClip playerShootsAudio;
    public AudioClip doorOpensAudio;

    private UnityAction<Vector3> playerShootsListener;
    private UnityAction<Vector3> doorOpensListener;

    private void Awake() {
        DontDestroyOnLoad(gameObject);
        playerShootsListener = new UnityAction<Vector3>(playerShootsHandler);
        doorOpensListener = new UnityAction<Vector3>(doorOpensHandler);
    }

    private void OnEnable() {
        EventManager.StartListening<PlayerShootsEvent, Vector3>(playerShootsListener);
        EventManager.StartListening<DoorOpensEvent, Vector3>(doorOpensListener);
    }

    private void OnDisable() {
        EventManager.StopListening<PlayerShootsEvent, Vector3>(playerShootsListener);
        EventManager.StopListening<DoorOpensEvent, Vector3>(doorOpensListener);
    }

    void playerShootsHandler(Vector3 pos) {
        if (eventSound3DPrefab) {
            EventSound3D sound = Instantiate(eventSound3DPrefab, pos, Quaternion.identity, null);
            sound.audioSrc.clip = playerShootsAudio;
            sound.audioSrc.minDistance = 5f;
            sound.audioSrc.maxDistance = 100f;
            sound.audioSrc.Play();
        }
    }

    void doorOpensHandler(Vector3 pos) {
        if (eventSound3DPrefab) {
            EventSound3D sound = Instantiate(eventSound3DPrefab, pos, Quaternion.identity, null);
            sound.audioSrc.clip = doorOpensAudio;
            sound.audioSrc.minDistance = 5f;
            sound.audioSrc.maxDistance = 100f;
            sound.audioSrc.Play();
        }
    }
}
