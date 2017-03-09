using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CrazySorting
{
    [RequireComponent(typeof(CharacterSpawner))]
    class Stage : MonoBehaviour
    {
        public int Duration;
        public CharacterSpawner Spawner { get { return GetComponent<CharacterSpawner>(); } }
    }
}
