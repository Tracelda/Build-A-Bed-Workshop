using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayRandomDogSounds : MonoBehaviour
{
    private AudioManager AudioManager;
    public float Timer;
    public float TimerTarget;
    public bool TimerUp;
    public bool TimerRunning;
    // Start is called before the first frame update
    void Start()
    {
        AudioManager = GameObject.Find("GameManager").GetComponent<AudioManager>();
        TimerRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        RunTimer();

        if (TimerUp)
        {
            AudioManager.PlayRandomSoundFromRange(3, 6);
            TimerUp = false;
        }
    }

    public void RunTimer()
    {
        if (TimerRunning)
        {
            if (Timer < TimerTarget)
            {
                Timer += Time.deltaTime;
            }
            else if (Timer >= TimerTarget)
            {
                TimerUp = true;
                TimerTarget = Random.Range(2, 4);
                Timer = 0;
            }
        }
    }
}
