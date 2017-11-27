using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;

namespace UCL.COMPGV07{

    [Serializable]
    public class Spawn
    {
        public int Product;
        public Vector3 Position;
        public Quaternion Rotation;
    }

    [Serializable]
    public class ExperimentConfiguration
    {
        public List<Spawn> Spawnable = new List<Spawn>();
        public List<int> Order = new List<int>();            
    }

    [Serializable]
    public struct Purchase
    {
        public int Code;
        public float Time;
    }

    /// <summary>
    /// The Catalogue stores the experiment configuration. It spawns items based on the provided Configuration File.
    /// It also provides the Order (the items the user must collect) to the component that runs the experiment.
    /// </summary>
    public class Experiment : MonoBehaviour {

        /// <summary>
        /// The list of available items that may be spawned. All items are identified by their ProductCode.
        /// </summary>
        public GameObject[] Inventory;

        public GameObject CameraGUI;

        /// <summary>
        /// The items to collect, loaded from the file
        /// </summary>
        public int[] ItemsToCollect { get; private set; }
        public List<Purchase> ItemsCollected { get; private set; }

        public List<int> itemsOutstanding;

        public void Start()
        {

        }

        public void Load(Stream stream)
        {
            var serialiser = new XmlSerializer(typeof(ExperimentConfiguration));
            var configuration = serialiser.Deserialize(stream) as ExperimentConfiguration;

            var catalogue = new Dictionary<int, GameObject>();
            foreach (var item in Inventory)
            {
                catalogue.Add(item.GetComponent<ProductCode>().Code, item);
            }

            // spawn items
            foreach (var spawnable in configuration.Spawnable)
            {
                Instantiate(catalogue[spawnable.Product], spawnable.Position, spawnable.Rotation);
            }

            // order
            ItemsToCollect = configuration.Order.ToArray();
            ItemsCollected = new List<Purchase>();

            itemsOutstanding = new List<int>(ItemsToCollect);
        }

        public bool GiveItem(int code)
        {
            ItemsCollected.Add(new Purchase()
            {
                Code = code,
                Time = Time.time                
            });

            // do some live checking
            if (itemsOutstanding.Contains(code))
            {
                itemsOutstanding.Remove(code);
                Debug.Log("Correct!");
                SendNotification("Correct!");
            }
            else
            {
                Debug.Log("Incorrect!");
                SendNotification("Incorrect");
            }

            if(itemsOutstanding.Count == 0)
            {
                Debug.Log("Experiment Complete!");
                SendNotification("Experiment Complete!");
            }

            return true;
        }

        private void SendNotification(string msg)
        {
            CameraGUI.GetComponent<Notifications>().DisplayMessage(msg);
        }
    }
}