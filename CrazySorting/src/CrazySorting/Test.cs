using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CrazySorting
{
    class Test : MonoBehaviour
    {
        void Awake()
        {
#if UNITY_EDITOR
            Debug.Log("Editor");
#elif UNITY_ANDROID
            Debug.Log("Android");
#endif
        }
    }
}
