using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

namespace UCL.COMPGV07{

    [Serializable]
    public class LineItem
    {
        public int Code;
        public int Quantity;
    }

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
        public List<LineItem> Order = new List<LineItem>();            
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

        public bool verbose = false;

        /// <summary>
        /// The items that the participant must collect, loaded from File;
        /// </summary>
        [HideInInspector]
        public Dictionary<int,int> orderState;

        public void Start()
        {
            orderState = new Dictionary<int, int>();
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
            foreach(var line in configuration.Order)
            {
                orderState[line.Code] = line.Quantity;
            }
        }

        public bool GiveItem(int code)
        {
            if(!orderState.ContainsKey(code))
            {
                orderState[code] = 0;
            }
            orderState[code]--;

            // some live checking
            if(verbose)
            {
                if(orderState[code] < 0)
                {
                    Debug.Log("Incorrect!");
                }
                else
                {
                    Debug.Log("Correct!");
                }
            }

            return true;
        }
    }
}