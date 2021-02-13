using UnityEngine;
using System.Collections.Generic;
using Steerings;

public class Spawner : MonoBehaviour {

	public GameObject sample;

	public int numInstances = 10;
	public float interval = 5f; // one ant every interval seconds

	private int generated = 0;
	private float elapsedTime = 0f; // time elapsed since last generation

    private List<GameObject> instances;

    void Start()
    {
        instances = new List<GameObject>();
        instances.Add(sample);
    }

	// Update is called once per frame
	void Update () {
		if (generated == numInstances)
			return;

		GameObject clone;
		if (elapsedTime >= interval) {
			// spawn creating an instance...
			clone = Instantiate(sample);
			clone.transform.position = this.transform.position;
			generated++;
			elapsedTime = 0;
            instances.Add(clone);
		} else {
			elapsedTime += Time.deltaTime;
		}

	}

    // --------------------------------------------------------------

    // listener method used in some examples. 
    public void OnSeekWeightChanged(float sw)
    {
        foreach (GameObject go in instances)
        {
            go.GetComponent<FlockingAround>().seekWeight = sw;
        }
    }

    // --------------------------------------------------------------
    // listener methods used in some other examples...

    public void OnWanderWeightChanged (float v)
    {
        foreach (GameObject go in instances)
        {
            go.GetComponent<Flocking>().wdWeight = v;
        }
    }

    public void OnCohesionWeightChanged(float v)
    {
        foreach (GameObject go in instances)
        {
            go.GetComponent<Flocking>().coWeight = v;
        }
    }

    public void OnRepulsionWeightChanged(float v)
    {
        foreach (GameObject go in instances)
        {
            go.GetComponent<Flocking>().rpWeight = v;
        }
    }

    public void OnVelocityMatchingWeightChanged(float v)
    {
        foreach (GameObject go in instances)
        {
            go.GetComponent<Flocking>().vmWeight = v;
        }
    }



    public void OnCohesionThresholdChanged(float v)
    {
        foreach (GameObject go in instances)
        {
            go.GetComponent<Flocking>().cohesionThreshold = v;
        }
    }

    public void OnRepulsionThresholdChanged(float v)
    {
        foreach (GameObject go in instances)
        {
            go.GetComponent<Flocking>().repulsionThreshold = v;
        }
    }

}

