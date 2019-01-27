using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotbarManager : MonoBehaviour
{

    public enum HotbarAction
    {
        Begging,
        Busking,
        Beatbox,
        OneMan,
        Breakdance,
        Violin
    }
    public void EnactHotbarAction(int action)
    {
        var player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        switch ((HotbarAction)action)
        {
            case HotbarAction.Begging:
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().ReduceRateHunger = -5;
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().ReduceRateHydration = 0;
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().ReduceRateCleanliness = -5;
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().ReduceRateWarmth = 0;
                }
                break;
            case HotbarAction.Busking:
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().ReduceRateHunger = -5;
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().ReduceRateHydration = -2;
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().ReduceRateCleanliness = -2;
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().ReduceRateWarmth = 7;
                }
                break;
            case HotbarAction.Beatbox:
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().ReduceRateHunger = 0;
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().ReduceRateHydration = -5;
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().ReduceRateCleanliness = 0;
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().ReduceRateWarmth = -5;
                }
                break;
            case HotbarAction.OneMan:
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().ReduceRateHunger = 0;
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().ReduceRateHydration = 5;
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().ReduceRateCleanliness = 0;
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().ReduceRateWarmth = 0;
                }
                break;
            case HotbarAction.Breakdance:
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().ReduceRateHunger = -2;
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().ReduceRateHydration = -2;
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().ReduceRateCleanliness = -10;
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().ReduceRateWarmth = 7;
                }
                break;
            case HotbarAction.Violin:
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().ReduceRateHunger = -5;
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().ReduceRateHydration = 0;
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().ReduceRateCleanliness = 0;
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().ReduceRateWarmth = -5;
                }
                break;
            default:
                break;
        }
    }
}
