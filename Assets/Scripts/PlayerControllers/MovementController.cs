using Scripts.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.PlayerControllers
{
    public class MovementController : MonoBehaviour
    {
        [SerializeField] private Animator _anim;
        [SerializeField] private int _speed = 5;
        [SerializeField] private int _rotateSpeed = 30;
        [SerializeField] private int _mouseSpeed = 50;
        private bool _swordDrawn;
        private bool _isRunning;


        private void OnEnable()
        {
            EventManager.Listen("onSheathSword", SheathSword);
            EventManager.Listen("onDrawSword", DrawSword);
        }

        void Update()
        {
            //if (Input.GetKey(KeyCode.W))
            //{
            //    _anim.SetBool("Run", true);
            //}
            //else
            //{
            //    _anim.SetBool("Run", false);
            //}

            float rotation = Input.GetAxis("Horizontal") * Time.deltaTime * _rotateSpeed;
            float movement = Input.GetAxis("Vertical") * Time.deltaTime * _speed;
            float mouseXRotation = Input.GetAxis("Mouse X") * Time.deltaTime * _mouseSpeed;
            transform.Rotate(new Vector3(0, mouseXRotation, 0));

            if (movement > 0)
            {
                _anim.SetBool("Run", true);
                transform.Translate(0, 0, movement);
                _isRunning = true;
            }
            else if (movement < 0)
            {
                _anim.SetTrigger("RunTurn");
            }
            else
            {
                _anim.SetBool("Run", false);
                _isRunning = false;
            }

            if (rotation != 0)
            {
                transform.Rotate(0, rotation, 0);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (_swordDrawn == true && _isRunning == false)
                {
                    Debug.Log("Jump with sword");
                    _anim.SetTrigger("JumpSword");
                }
                else if (_swordDrawn == false && _isRunning == false)
                {
                    _anim.SetTrigger("Jump");
                    Debug.Log("Jump");
                }

                else if (_isRunning == true)
                {
                    Debug.Log("Forward Jump");
                    _anim.SetTrigger("ForwardJump");
                }
            }

            if (Input.GetKeyDown(KeyCode.Space) && Input.GetKeyDown(KeyCode.LeftControl))
            {
                Debug.Log("Forward Jump");
                _anim.SetTrigger("ForwardJump");
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (_swordDrawn == true)
                {
                    Debug.Log("Sword Dash");
                    _anim.SetTrigger("SwordDash");
                }
                else if (_swordDrawn == false)
                {
                    Debug.Log("Dash");
                    _anim.SetTrigger("Dash");
                }
            }
        }

        private void DrawSword()
        {
            _swordDrawn = true;
        }

        private void SheathSword()
        {
            _swordDrawn = false;
        }

        private void OnDisable()
        {
            EventManager.UnsubscribeEvent("onSheathSword", SheathSword);
            EventManager.UnsubscribeEvent("onDrawSword", DrawSword);
        }
    }
}

