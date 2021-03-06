﻿using UnityEngine;

namespace Util {
    public class IdSobj : ScriptableObject {
        /// <summary> starts from 1 </summary>
        public int id;
        public string nameid;
        public string readableName;
        public Sprite mainImage;
        [TextArea] public string mainDescription;
    }
}
