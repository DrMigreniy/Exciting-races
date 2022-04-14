using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineSound : MonoBehaviour
{
    private float startUpSound = 2f;
    private float soundTime = 0;
    private bool isStart = false;
    private bool isKeyDown = false;
    public AudioSource audioSource;
    public float speed;

    public int low;
    public int med;
    public int high;
    public int limit;

    public AudioClip lowOn;
    public AudioClip lowOf;

    public AudioClip medOn;
    public AudioClip medOf;

    public AudioClip highOn;
    public AudioClip highOf;

    public AudioClip idle;
    public AudioClip maxRPM;

    // Start is called before the first frame update
    void Start()
    {
        isStart = true;
    }

    // Update is called once per frame
    void Update()
    {
        speed = GetComponent<Controller>().getSpeed();
    }
    private void FixedUpdate()
    {
        if (isStart)
        {
            startUpSound -= Time.deltaTime;
            if (startUpSound > 0)
                return;
            if (Input.GetAxis("Vertical") > 0 && soundTime <= 0 || Input.GetAxis("Vertical") > 0 && !isKeyDown)
            {
                isKeyDown = true;
                if (speed >= low && speed < med && audioSource.clip != lowOn)
                    audioSource.clip = lowOn;
                else if (speed >= med && speed < high && audioSource.clip != medOn)
                    audioSource.clip = medOn;
                else if (speed >= high && speed < limit && audioSource.clip != highOn)
                    audioSource.clip = highOn;
                else if (speed >= limit && audioSource.clip != maxRPM)
                    audioSource.clip = maxRPM;

                soundTime = audioSource.clip.length - 0.2f;

                audioSource.Play();
            }
            else if (soundTime <= 0 && Input.GetAxis("Vertical") <= 0 || Input.GetAxis("Vertical") <= 0 && isKeyDown)
            {
                isKeyDown = false;
                if (speed <= low && audioSource.clip != idle)
                    audioSource.clip = idle;
                else if (speed > low && speed < med && audioSource.clip != lowOf  )
                    audioSource.clip = lowOf;
                else if (speed >= med && speed < high && audioSource.clip != medOf)
                    audioSource.clip = medOf;
                else if (speed >= high && speed < limit && audioSource.clip != highOf)
                    audioSource.clip = highOf;
                else if (speed >= limit && audioSource.clip != maxRPM)
                    audioSource.clip = maxRPM;

                soundTime = audioSource.clip.length;

                audioSource.Play();
            }
            else if (soundTime > 0)
            {
                soundTime -= Time.deltaTime;
            }
        }
    }
}
