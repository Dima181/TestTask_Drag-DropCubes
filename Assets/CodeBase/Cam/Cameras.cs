using System.ComponentModel;
using UnityEngine;

namespace MyTask.CodeBase.Cam
{
    public class Cameras
    {
        public Camera ScreenCamera => _screenCamera;

        private Camera _screenCamera;

        public Cameras(Camera screenCamera)
        {
            _screenCamera = screenCamera;
        }
    }
}
