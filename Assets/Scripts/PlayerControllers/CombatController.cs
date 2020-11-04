using Scripts.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.PlayerControllers
{
    public class CombatController : MonoBehaviour
    {
        [SerializeField] private Animator _anim;

        private float _doubleTapTime;
        private int _combatCounter;
        private bool _doubleTap;
        //private bool _tripletap;
        //private bool _quadtap;
        //private bool _quinttap;
        private bool _resetAttack;
        private bool _block;
        private bool _swordDrawn;


        private void OnEnable()
        {
            EventManager.Listen("onSheathSword", SheathSword);
            EventManager.Listen("onDrawSword", DrawSword);
        }

        void Update()
        {
            if (_swordDrawn == false)
                return;

            //if (Input.GetKeyDown(KeyCode.Mouse0) && _quintTap == true)
            //{
            //    if (Time.time - _doubleTapTime < .5f)
            //    {
            //        _anim.SetBool("Attack4", false);
            //        Debug.Log("Attack 5");
            //        _doubleTapTime = Time.time;
            //        _anim.SetBool("Attack5", true);

            //    }

            //    _resetAttack = true;
            //}

            //if (Input.GetKeyDown(KeyCode.Mouse0) && _quadTap == true)
            //{
            //    if (Time.time - _doubleTapTime < .5f)
            //    {
            //        _anim.SetBool("Attack3", false);
            //        Debug.Log("Attack 4");
            //        _doubleTapTime = Time.time;
            //        _anim.SetBool("Attack4", true);
            //        _quintTap = true;

            //    }

            //    _resetAttack = true;
            //}

            //if (Input.GetKeyDown(KeyCode.Mouse0) && _tripleTap == true)
            //{
            //    if (Time.time - _doubleTapTime < .5f)
            //    {
            //        _anim.SetBool("Attack2", false);
            //        Debug.Log("Attack 3");
            //        _doubleTapTime = Time.time;
            //        _anim.SetBool("Attack3", true);
            //        _quadTap = true;

            //    }

            //    _resetAttack = true;
            //}

            //if (Input.GetKeyDown(KeyCode.Mouse0) && _doubleTap == true)
            //{
            //    if (Time.time - _doubleTapTime < .5f)
            //    {
            //        _anim.SetBool("Attack1", false);
            //        Debug.Log("Attack 2");
            //        _doubleTapTime = Time.time;
            //        _anim.SetBool("Attack2", true);
            //        _tripleTap = true;

            //    }

            //    _resetAttack = true;
            //}

            if (Input.GetKeyDown(KeyCode.Mouse0) && _doubleTap == true)
            {
                if (Time.time - _doubleTapTime < .5f)
                {
                    Debug.Log("Chain Attacks");
                    _anim.SetInteger("Attack", _combatCounter);
                    _anim.Play("Attack");
                    _combatCounter++;
                    _doubleTapTime = Time.time;
                }
                else
                {
                    _anim.SetTrigger("EndAttack");
                    _doubleTap = false;
                }

                _resetAttack = true;
            }

            if (Input.GetKeyDown(KeyCode.Mouse0) && _doubleTap == false)
            {
                Debug.Log("Attacking");
                _combatCounter = 1;
                _anim.SetInteger("Attack", _combatCounter);
                //_anim.Play("Attack");
                _doubleTap = true;
                _doubleTapTime = Time.time;
                _combatCounter++;
                _resetAttack = true;
            }

            //if (Input.GetKeyDown(KeyCode.Mouse0) && _doubleTap == false)
            //{
            //    //Debug.Log("Attack");
            //    //_anim.SetBool("Attack1", true);
            //    //_doubleTap = true;
            //    _doubleTapTime = Time.time;
            //    if (Input.GetKeyDown(KeyCode.Mouse0) && (Time.time - _doubleTapTime < .5f))
            //    {
            //        _doubleTapTime = Time.time;
            //        if (Input.GetKeyDown(KeyCode.Mouse0) && (Time.time - _doubleTapTime < .5f))
            //        {
            //            _doubleTapTime = Time.time;
            //            if (Input.GetKeyDown(KeyCode.Mouse0) && (Time.time - _doubleTapTime < .5f))
            //            {
            //                _doubleTapTime = Time.time;
            //                if (Input.GetKeyDown(KeyCode.Mouse0) && (Time.time - _doubleTapTime < .5f))
            //                {
            //                    Debug.Log("Attack 5");
            //                    _doubleTapTime = Time.time;
            //                    _anim.SetTrigger("Attack5");
            //                }
            //                else
            //                {
            //                    Debug.Log("Attack 4");
            //                    _doubleTapTime = Time.time;
            //                    _anim.SetTrigger("Attack4");
            //                }
            //            }
            //            else
            //            {
            //                Debug.Log("Attack 3");
            //                _doubleTapTime = Time.time;
            //                _anim.SetTrigger("Attack3");
            //            }
            //        }
            //        else
            //        {
            //            Debug.Log("Attack 2");
            //            _doubleTapTime = Time.time;
            //            _anim.SetTrigger("Attack2");
            //        }
            //    }
            //    else
            //    {
            //        Debug.Log("Attack");
            //        _anim.SetTrigger("Attack1");
            //    }

            //}

            if (_resetAttack)
            {
                _doubleTap = false;
                _resetAttack = false;
            }

            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                Debug.Log("Block");
                _block = true;
                Block();
            }

            if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                Debug.Log("End block");
                _block = false;
                Block();
            }
        }

        private void Block()
        {
            if (_block == true)
            {
                _anim.SetBool("Block", true);
            }
            else
            {
                _anim.SetBool("Block", false);
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

