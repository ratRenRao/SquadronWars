﻿using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    public class GameCharacter : MonoBehaviour
    {

        public int x
        {
            get;
            set;
        }
        public int y
        {
            get;
            set;
        }

        public GameCharacter(int x, int y)
        {

            this.x = x;
            this.y = y;
        }
    }
}
