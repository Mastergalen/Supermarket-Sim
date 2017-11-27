using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

namespace UCL.COMPGV07
{
    public class ExperimentCreator : MonoBehaviour
    {

        /// <summary>
        /// A list of all the objects to be spawned on start-up. These should be GameObjects in the scene that
        /// are instantiated from Prefabs. The location to spawn will be taken from these GameObjects, so ensure
        /// they are placed correctly in the scene.
        /// </summary>
        public List<GameObject> ItemsToSpawn;

        /// <summary>
        /// A list of objects that the participant must collect and return. These should be instances of ProductCode
        /// (or any GameObjects containing a ProductCode).
        /// </summary>
        public List<GameObject> ItemsToCollect;

        public void Save(string path)
        {
            ExperimentConfiguration configuration = new ExperimentConfiguration();
            foreach(var item in ItemsToSpawn)
            {
                Spawn spawn = new Spawn();
                spawn.Product = item.GetComponent<ProductCode>().Code;
                spawn.Position = item.transform.position;
                spawn.Rotation = item.transform.rotation;
                configuration.Spawnable.Add(spawn);
            }
            foreach(var item in ItemsToCollect)
            {
                configuration.Order.Add(item.GetComponent<ProductCode>().Code);
            }

            var serialiser = new XmlSerializer(typeof(ExperimentConfiguration));
            using (var fs = new FileStream(path, FileMode.Create))
            {
                serialiser.Serialize(fs, configuration);
            }
        }

    }
}