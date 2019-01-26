using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

[Serializable]
public class PlayerAction : ScriptableObject {
    [SerializeField]
    public int maxDuration;
    [SerializeField]
    public float currentDuration;
    public bool aoeEffect;
    [SerializeField]
    public bool Running = false;

    public void Init(int DurationSeconds, bool AOEEffect)
    {
        currentDuration = 0;
        maxDuration = DurationSeconds;
        aoeEffect = AOEEffect;
        Running = true;
    }
    public void Update()
    {
        if (Running)
        {
            currentDuration += Time.deltaTime;
            Debug.Log(currentDuration);
            if (currentDuration >= maxDuration)
            {
                if (aoeEffect)
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().AOEEffect = false;
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().AOERange = 0;
                    Running = false;
                    currentDuration = 0;
                }
            }
        }
    }
}
