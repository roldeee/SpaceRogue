using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AudioEventManager : MonoBehaviour
{
    public EventSound3D eventSound3DPrefab;
    public AudioClip playerShootsAudio;
    public AudioClip doorOpensAudio;
    public AudioClip playerDamagedAudio;

    private UnityAction<Vector3> playerShootsListener;
    private UnityAction<Vector3> doorOpensListener;
    private UnityAction<Vector3> playerDamagedListener;
    private Image damageFlash;

    private void Awake() {
        DontDestroyOnLoad(gameObject);
        playerShootsListener = new UnityAction<Vector3>(playerShootsHandler);
        doorOpensListener = new UnityAction<Vector3>(doorOpensHandler);
        playerDamagedListener = new UnityAction<Vector3>(playerDamageHandler);
        damageFlash = GetComponentInChildren<Image>();
    }

    private void Update() {
        // damage flash
        if (damageFlash.color.a > 0) {
            Color color = damageFlash.color;
            color.a -= 0.05f;
            damageFlash.color = color;
        }
    }

    private void OnEnable() {
        EventManager.StartListening<PlayerShootsEvent, Vector3>(playerShootsListener);
        EventManager.StartListening<DoorOpensEvent, Vector3>(doorOpensListener);
        EventManager.StartListening<PlayerDamageEvent, Vector3>(playerDamagedListener);
    }

    private void OnDisable() {
        EventManager.StopListening<PlayerShootsEvent, Vector3>(playerShootsListener);
        EventManager.StopListening<DoorOpensEvent, Vector3>(doorOpensListener);
        EventManager.StopListening<PlayerDamageEvent, Vector3>(playerDamagedListener);
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

    void playerDamageHandler(Vector3 pos) {
        if (eventSound3DPrefab) {
            EventSound3D sound = Instantiate(eventSound3DPrefab, pos, Quaternion.identity, null);
            sound.audioSrc.clip = playerDamagedAudio;
            sound.audioSrc.minDistance = 5f;
            sound.audioSrc.maxDistance = 100f;
            sound.audioSrc.Play();
        }
        Color color = damageFlash.color;
        color.a = 0.8f;
        damageFlash.color = color;
    }
}
