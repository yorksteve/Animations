using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.PlayerControllers
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private int _speed = 10;

        void Update()
        {
            float rotation = Input.GetAxis("Mouse Y") * Time.deltaTime * _speed;
            transform.Rotate(rotation, 0, 0);
            Mathf.Clamp(transform.eulerAngles.y, -65, 40);
        }
    }
}

