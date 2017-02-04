using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CrazySorting
{
    
    class UpModifier : MonoBehaviour
    {
        public Vector3 Forward;
        
        
        void Update()
        {
            transform.rotation = Quaternion.LookRotation(transform.forward, Vector3.up);
        }
    }
}
