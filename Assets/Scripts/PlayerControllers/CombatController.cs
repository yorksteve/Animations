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

            if (Input.GetMouseButtonDown(0) && _doubleTap == true)
            {
                if (Time.time - _doubleTapTime < .5f)
                {
                    Debug.Log("Chain Attacks");
                    _combatCounter++;
                    _anim.SetInteger("Attack", _combatCounter);
                    _anim.SetTrigger("Attack" + _combatCounter);
                    _doubleTapTime = Time.time;
                }
                else
                {
                    _doubleTap = false;
                }
            }

            if (Input.GetMouseButtonDown(0) && _doubleTap == false)
            {
                Debug.Log("Attack");
                _combatCounter = 1;
                _anim.SetInteger("Attack", _combatCounter);
                _anim.SetTrigger("Attack" + _combatCounter);
                _doubleTapTime = Time.time;
                _doubleTap = true;
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

