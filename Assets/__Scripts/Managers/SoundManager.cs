using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance {get; private set;}

    [SerializeField] AudioMixer audioMixer;
    [SerializeField] SOSoundList soundList;
    [SerializeField] PackageCannon cannon;
    [SerializeField] PlayerPickup playerPickup;
    [SerializeField] Transform audioPrefab;

    readonly string MASTER_VOLUME = "MasterVolume";

    public void SetParameter(float value)
    {
        if(value <= -20f)
        {
            value = -80f;
            audioMixer.SetFloat(MASTER_VOLUME, value);
        }
        else
        {
            audioMixer.SetFloat(MASTER_VOLUME, value);
        }
    }

    float volumeMultiplier = 2f;
    
    void Awake()
    {Instance = this;

    }

    void Start()
    {
        playerPickup.OnPickupPackage += PlayerPickup_OnPickupPackage;
        playerPickup.OnFailPickup += PlayerPickup_OnFailPickup;
        House.OnAnyDelivered += House_OnAnyDelivered;
        House.OnAnyFail += House_OnAnyFail;
        cannon.OnCannonShoot += Cannon_OnCannonShoot;
        cannon.OnRocketShoot += Cannon_OnRocketShoot;
    }

    void PlayerPickup_OnPickupPackage(object sender, EventArgs e)
    {
        PlaySound(soundList.pickupSound, playerPickup.transform.position);
    }

    void PlayerPickup_OnFailPickup(object sender, EventArgs e)
    {
        PlaySound(soundList.failPickupSound, playerPickup.transform.position);
    }

    void House_OnAnyDelivered(object sender, House.OnDeliveredEventArgs e)
    {
        if(e.e_successStatus == true)
        {
            PlaySound(soundList.successSound, e.e_housePosition);
        }
        else if(e.e_successStatus == false)
        {
            PlaySound(soundList.failSound, e.e_housePosition);
        }
    }

    void House_OnAnyFail(object sender, EventArgs e)
    {
        PlaySound(soundList.failSound, Camera.main.transform.position);
    }

    void Cannon_OnCannonShoot(object sender, PackageCannon.OnCannonShootEventArgs e)
    {
        PlaySound(soundList.cannonSound, e.e_cannonEndPointPosition);
    }

    void Cannon_OnRocketShoot(object sender, PackageCannon.OnCannonShootEventArgs e)
    {
        PlaySound(soundList.rocketSound, e.e_cannonEndPointPosition);
    }

    void PlaySound(AudioClip _clip, Vector3 position)
    {
        Transform audioTransform = Instantiate(audioPrefab, position, Quaternion.identity);
        audioTransform.TryGetComponent(out AudioSource spawnedAudio);
        spawnedAudio.clip = _clip;
        spawnedAudio.Play();
        audioTransform.TryGetComponent(out AudioPrefab prefabScript);
        prefabScript.StartCoroutine(prefabScript.DestroyPrefab());
    }

    // void PlaySound(AudioClip[] _clips, Vector3 position)
    // {
    //     AudioSource.PlayClipAtPoint(_clips[UnityEngine.Random.Range(0, _clips.Length)], position);
    // }

    public void PlayFootstepsSound(Vector3 position)
    {
        PlaySound(soundList.footstepSound, position);
    }

    // public void ChangeVolume()
    // {
    //     volume += .1f;
    //     if(volume > 1f)
    //     {
    //         volume = 0f;
    //     }

    //     PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, volume);
    //     PlayerPrefs.Save();
    // }

    public float GetVolumeMultiplier()
    {
        return volumeMultiplier;
    }
}
