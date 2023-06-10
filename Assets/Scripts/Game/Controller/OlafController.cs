using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class OlafController : MonoBehaviour
    {
        public GameObject Obj { get; private set; }
        public RectTransform Tra { get; private set; }
        public Animator Ani { get; private set; }

        public void Initialize()
        {
            Obj = gameObject;
            Tra = GetComponent<RectTransform>();
            Ani = GetComponent<Animator>();
        }

        void OnTriggerStay(Collider collider)
        {

        }
    }
}
