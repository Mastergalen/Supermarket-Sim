using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace UCL.COMPGV07
{
    public class ExperimentTextAsset : Experiment
    {
        public TextAsset ConfigAsset;

        // Use this for initialization
        new void Start()
        {
            base.Start();
            Load(new MemoryStream(ConfigAsset.bytes));
        }
    }
}