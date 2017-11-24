using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace UCL.COMPGV07
{
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

        /// <summary>
        /// The GameObject to which the eyes and ears are attached. This moves around with the users real head, relative to the playspace.
        /// </summary>
        public GameObject Head;

        /// <summary>
        /// The GameObject to which the head is relative in the real world. This will move around, taking the head with it, to allow the user
        /// to explore the whole virtual space.
        /// </summary>
        public GameObject Playspace;

        /// <summary>
        /// The number of the group running this study.
        /// </summary>
        public int GroupNumber;

        /* These APIs should be called by the interaction techniques implementation. */

        /// <summary>
        /// Called when a key is pressed. This should be called once per key, per frame that the interaction is taking place.
        /// It must be called multiple times if multiple keys are depressed.
        /// </summary>
        public static void KeyDown()
        {
            singleton.inputCounter++;
        }

        /* Implementation */

        [Serializable]
        protected class Trial
        {
            public int participantNumber;
            public int groupNumber;
            public DateTime session;
            public float startTime;
            public int[] itemsToCollect;        //convenience copy
            public List<Purchase> itemsCollected;
            public List<Frame> frames = new List<Frame>();
        }

        [Serializable]
        protected struct PortableVector
        {
            public float x;
            public float y;
            public float z;
        }

        [Serializable]
        protected struct Frame
        {
            public float time;
            public PortableVector headPosition;
            public PortableVector playspacePosition;
            public int inputCount;
        }
        
        private int inputCounter;
        private Trial trial;
        private static Logging singleton;
        private FileStream file;

        // Use this for initialization
        void Start()
        {
            if(Head == null)
            {
                Debug.LogError("Logging Head GameObject must be set");
            }
            if (Playspace == null)
            {
                Debug.LogError("Logging Playspace GameObject must be set");
            }
            if(GroupNumber <= 0)
            {
                Debug.LogError("Logging Group Number must be set");
            }

            inputCounter = 0;
            singleton = this;
            trial = new Trial();
            trial.participantNumber = OpenFile();
            Debug.Log("Started with Participant Number: " + trial.participantNumber);
            trial.groupNumber = GroupNumber;
            trial.session = DateTime.Now;
            trial.startTime = Time.time;           
        }

        // Update is called once per frame
        void LateUpdate()
        {
            Frame frame = new Frame();
            frame.time = Time.time;
            frame.headPosition.x = Head.transform.position.x;
            frame.headPosition.y = Head.transform.position.y;
            frame.headPosition.z = Head.transform.position.z;
            frame.playspacePosition.x = Playspace.transform.position.x;
            frame.playspacePosition.y = Playspace.transform.position.y;
            frame.playspacePosition.z = Playspace.transform.position.z;
            frame.inputCount = inputCounter;
            inputCounter = 0;
            trial.frames.Add(frame);
        }

        void OnApplicationQuit()
        {
            trial.itemsToCollect = GetComponent<Experiment>().ItemsToCollect;
            trial.itemsCollected = GetComponent<Experiment>().ItemsCollected;
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(file, trial);
            file.Close();   
        }

        int OpenFile()
        {
            string path = @"C:\COMPGV07_Experiment\Group_" + GroupNumber + "\\";
            string prefix = "participant_";
            string postfix = ".bin";

            Directory.CreateDirectory(path);

            int i = 0;
            FileInfo fi;
            do
            {
                i++;
                fi = new FileInfo(path + prefix + i + postfix);
            } while (fi.Exists);

            file = new FileStream(fi.FullName, FileMode.CreateNew, FileAccess.Write);

            return i;
        }
    }
}