using System.Collections;
using System.Collections.Generic;
using Malee;
using UnityEngine;

namespace Map
{
    public class SurrogateTest : MonoBehaviour
    {
        [SerializeField]
        private MyClass[] objects;

        [
            SerializeField,
            Reorderable(surrogateType = typeof(GameObject), surrogateProperty = "gameObject")
        ]
        private MyClassArray myClassArray;

        [System.Serializable]
        public class MyClass
        {
            public string name;
            public GameObject gameObject;
        }

        [System.Serializable]
        public class MyClassArray : ReorderableArray<MyClass> { }
    }
}
