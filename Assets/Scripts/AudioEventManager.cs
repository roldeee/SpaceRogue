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
    public AudioClip enemyDamagedAudio;
    public AudioClip enemyAttacksAudio;

    private UnityAction<Vector3> playerShootsListener;
    private UnityAction<Vector3> doorOpensListener;
    private UnityAction<Vector3> playerDamagedListener;
    private UnityAction<Vector3> enemyDamagedListener;
    private UnityAction<Vector3> enemyAttacksListener;
    private Image damageFlash;

    private void Awake() {
        DontDestroyOnLoad(gameObject);
        playerShootsListener = new UnityAction<Vector3>(playerShootsHandler);
        doorOpensListener = new UnityAction<Vector3>(doorOpensHandler);
        playerDamagedListener = new UnityAction<Vector3>(playerDamageHandler);
        enemyDamagedListener = new UnityAction<Vector3>(enemyDamagedHandler);
        enemyAttacksListener = new UnityAction<Vector3>(enemyAttacksHandler);
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
        EventManager.StartListening<EnemyDamagedEvent, Vector3>(enemyDamagedListener);
        EventManager.StartListening<EnemyAttacksEvent, Vector3>(enemyAttacksListener);
    }

    private void OnDisable() {
        EventManager.StopListening<PlayerShootsEvent, Vector3>(playerShootsListener);
        EventManager.StopListening<DoorOpensEvent, Vector3>(doorOpensListener);
        EventManager.StopListening<PlayerDamageEvent, Vector3>(playerDamagedListener);
        EventManager.StopListening<EnemyDamagedEvent, Vector3>(enemyDamagedListener);
        EventManager.StopListening<EnemyAttacksEvent, Vector3>(enemyAttacksListener);
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

    void enemyDamagedHandler(Vector3 pos) {
        if (eventSound3DPrefab) {
            EventSound3D sound = Instantiate(eventSound3DPrefab, pos, Quaternion.identity, null);
            sound.audioSrc.clip = enemyDamagedAudio;
            sound.audioSrc.minDistance = 5f;
            sound.audioSrc.maxDistance = 100f;
            sound.audioSrc.Play();
        }
    }

    void enemyAttacksHandler(Vector3 pos) {
        if (eventSound3DPrefab) {
            EventSound3D sound = Instantiate(eventSound3DPrefab, pos, Quaternion.identity, null);
            sound.audioSrc.clip = enemyAttacksAudio;
            sound.audioSrc.minDistance = 5f;
            sound.audioSrc.maxDistance = 100f;
            sound.audioSrc.Play();
        }
    }
}
