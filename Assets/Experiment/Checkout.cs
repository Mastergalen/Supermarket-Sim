using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCL.COMPGV07 {

    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(BoxCollider))]
    public class Checkout : MonoBehaviour {

        public AudioClip scanSuccess;
        public Experiment experimentManager;

        private AudioSource speaker;

        // Use this for initialization
        void Start() {
            speaker = GetComponent<AudioSource>();
        }

        void OnTriggerEnter(Collider other)
        {
            var code = -1;
            var codeComponent = other.gameObject.GetComponent<ProductCode>();
            if (codeComponent != null)
            {
                code = codeComponent.Code;
            }

            experimentManager.GiveItem(code);

            Destroy(other.gameObject);
            speaker.PlayOneShot(scanSuccess);
        }
    }
}