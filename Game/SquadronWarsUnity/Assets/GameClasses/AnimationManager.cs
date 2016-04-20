﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;
using ActionType = Assets.GameClasses.Action.ActionType;

namespace Assets.GameClasses
{
    class AnimationManager
    {
        public GameController GameController;
        private UnityEngine.Animator _executionerAnimator;
        private UnityEngine.Animator _targetAnimator;
        private CharacterGameObject _executionerCharacterGameObject;
        private CharacterGameObject _targetCharacterGameObject;
        private ActionAnimator _actionAnimator = GlobalConstants.ActionAnimator;
        private ActionType _actionType = ActionType.Idle;
        private Tile _executionerTile;
        private Tile _targetTile;
        private int _damage = 0;

        public AnimationManager(CharacterGameObject executioner, CharacterGameObject target, Tile executionerTile, Tile targetTile, ActionType actionType, int damage)
        {
            _executionerTile = executionerTile;
            _targetTile = targetTile;
            _actionType = actionType;
            _damage = damage;

            _executionerCharacterGameObject = executioner;
            _targetCharacterGameObject = target;

            _executionerAnimator = GlobalConstants.GameController.GetAnimator(executionerTile);
            _targetAnimator = GlobalConstants.GameController.GetAnimator(targetTile);
        }

        public void SetDamage(int damage)
        {
            _damage = damage;
        }

        public void ExecuteLingeringEffect()
        {
            _actionAnimator.Animate(_targetCharacterGameObject, _targetAnimator, _targetTile, _damage);
        }

        public void Cast(string ability)
        {
            Debug.Log("Cast Method: " + ability);
            //action = ActionType.Idle;
            _executionerAnimator.SetBool("isCasting", true);
            float currentX = (float) (Math.Round(_executionerTile.transform.localPosition.x, 2));
            float currentY = (float) (Math.Round(_executionerTile.transform.localPosition.y, 2));
            float targetX = (float) (Math.Round(_targetTile.transform.localPosition.x + 1.6f, 2));
            float targetY = (float) (Math.Round(_targetTile.transform.localPosition.y, 2));
            //  Transform targetLocation = _targetTile.transform;
            if (currentX - targetX > 0)
            {
                _executionerAnimator.SetFloat("x", -1);
                _executionerAnimator.SetFloat("y", 0);
            }
            if (currentX - targetX < 0)
            {
                _executionerAnimator.SetFloat("x", 1);
                _executionerAnimator.SetFloat("y", 0);
            }
            if (currentY - targetY > 0)
            {
                _executionerAnimator.SetFloat("x", 0);
                _executionerAnimator.SetFloat("y", -1);
            }

            if (currentY - targetY < 0)
            {
                _executionerAnimator.SetFloat("x", 0);
                _executionerAnimator.SetFloat("y", 1);
            }
            if (_targetTile.isOccupied)
            {
                _actionAnimator.Animate(_executionerCharacterGameObject, _targetCharacterGameObject, _executionerAnimator, _targetAnimator, _targetTile, ability, _damage);
            }
            else
            {
                _actionAnimator.Animate(_executionerAnimator);
            }
        }

        public void Attack(string ability, bool crit)
        {
            _actionType = ActionType.Idle;
            int weaponId = 0;
            if (_executionerCharacterGameObject.CharacterClassObject.Equipment.Weapon != null)
                weaponId = _executionerCharacterGameObject.CharacterClassObject.Equipment.Weapon.ItemId;

            var attackType = "";

            if (weaponId == 0 || (weaponId >= 8000 && weaponId < 9000))
            {
                attackType = "isAttacking";
            }
            else if (weaponId >= 10000 && weaponId < 11000)
            {
                attackType = "isAttackingBow";
            }
            else if (weaponId >= 9000 && weaponId < 10000)
            {
                attackType = "isAttackingSpear";
            }

            _executionerAnimator.SetBool(attackType, true);
            var currentX = (float) (Math.Round(_executionerTile.transform.localPosition.x, 2));
            var currentY = (float) (Math.Round(_executionerTile.transform.localPosition.y, 2));
            var targetX = (float) (Math.Round(_targetTile.transform.localPosition.x + 1.6f, 2));
            var targetY = (float) (Math.Round(_targetTile.transform.localPosition.y, 2));
            //  Transform targetLocation = _targetTile.transform;
            if (currentX - targetX > 0)
            {
                _executionerAnimator.SetFloat("x", -1);
                _executionerAnimator.SetFloat("y", 0);
            }
            if (currentX - targetX < 0)
            {
                _executionerAnimator.SetFloat("x", 1);
                _executionerAnimator.SetFloat("y", 0);
            }
            if (currentY - targetY > 0)
            {
                _executionerAnimator.SetFloat("x", 0);
                _executionerAnimator.SetFloat("y", -1);
            }

            if (currentY - targetY < 0)
            {
                _executionerAnimator.SetFloat("x", 0);
                _executionerAnimator.SetFloat("y", 1);
            }
            if (_targetTile.isOccupied)
            {
                _actionAnimator.Animate(_executionerCharacterGameObject, _targetCharacterGameObject, _executionerAnimator, _targetAnimator, _targetTile, ability, attackType, _damage, crit);
            }
            else
            {
                _actionAnimator.Animate(_executionerAnimator, attackType); 
            }
        }
    }
}
