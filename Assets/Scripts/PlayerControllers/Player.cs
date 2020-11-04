using Scripts.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.PlayerControllers
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Animator _anim;
        [SerializeField] private GameObject _swordInHand;
        [SerializeField] private GameObject _swordOnBack;
        [SerializeField] private float _health = 100;
        [SerializeField] private float _attackDamage;
        private bool _swordDrawn;



        void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                if (_swordDrawn == true)
                {
                    _anim.SetBool("SwordDrawn", false);
                    StartCoroutine(DrawTime());
                    _swordDrawn = false;
                    EventManager.Fire("onSheathSword");
                }
                else
                {
                    _anim.SetBool("SwordDrawn", true);
                    StartCoroutine(DrawTime());
                    _swordDrawn = true;
                    EventManager.Fire("onDrawSword");
                }
            }
        }

        IEnumerator DrawTime()
        {
            if (_swordDrawn == true)
            {
                yield return new WaitForSeconds(_anim.GetCurrentAnimatorStateInfo(0).length);
                _swordOnBack.SetActive(true);
                _swordInHand.SetActive(false);
            }
            else
            {
                yield return new WaitForSeconds(_anim.GetCurrentAnimatorStateInfo(0).length);
                _swordOnBack.SetActive(false);
                _swordInHand.SetActive(true);
            }
        }
    }
}


