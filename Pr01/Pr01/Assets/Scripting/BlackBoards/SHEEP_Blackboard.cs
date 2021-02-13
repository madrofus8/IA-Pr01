using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SHEEP_Blackboard : MonoBehaviour
{
    [Header ("Flock Wandering")]

    public GameObject locationA;
    public GameObject locationB;
    public GameObject locationC;
    public float intervalBetweenOuts = 3;
    public float initialSeekWeight = 0.2f;
    public float seekIncrement = 0.2f;
    public float locationReachedRadius = 0.3f;

    [Header ("Eating or Wandering")]

    public float timeToBeingHungry = 5;
    public GameObject Eater;
    public float eaterReachedRadius = 1f;
    public float timeToEat = 2f;
}
