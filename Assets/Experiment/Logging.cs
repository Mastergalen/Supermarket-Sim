using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This class monitors the environment and logs data about user behaviour to a file. */
public class Logging : MonoBehaviour
{
    /*
    #1 Completion Time
    * The time from the user first entering the environment(Start() called on data collection script) to the last item registering on the conveyer belt.

    #2 Error Rate
    * The total number of redundant or otherwise incorrect items placed on the conveyer belt

    #3 Expended Energy
    * The amount of time the user spends providing input to the system.
    * This includes button presses, joystick actuation, gesturing (estimated as the typical time to complete a gesture), or moving the feet if walking in place.
    * Button presses will measure how long the button is held down for.
    * This will be measured to begin with using simply the unity Input.GetKeyXXXX() methods, but will be extended depending on the design of the interaction techniques.

    #4 Distance Travelled (real)
    * The total distance covered by the head throughout the trial
    * Computed by finding the difference in positions between two frames and accumulating it

    #5 Distance Travelled (virtual)
    * The total distance covered by the virtual viewpoint throughout the trial.
    * Computed by finding the difference in positions between two frames and accumulating it

    #6 Comfort
    * Measured by the Simulator Sickness Questionniare, before and after
    */

    private class Trial
    {
        public int   ParticipantId; // randomly generated
        public float CompletionTime; // in seconds
        public int   ErrorRate;
        public float ExpendedEnergy; // in seconds
        public float RealDistanceTravelled; // in m
        public float VirtualDistanceTravelled; // in units
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /* These APIs should be called by the interaction techniques implementation. */

    /// <summary>
    /// Called when a key is pressed. This should be called once per key, per frame that the interaction is taking place.
    /// It must be called multiple times if multiple keys are depressed.
    /// </summary>
    public static void KeyDown()
    {

    }



}
