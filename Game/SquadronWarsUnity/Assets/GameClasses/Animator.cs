using System;
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
    class Animator
    {
        public GameController GameController;
        private UnityEngine.Animator _executionerAnimator;
        private UnityEngine.Animator _targetAnimator;
        private CharacterGameObject _executionerCharacterGameObject;
        private CharacterGameObject _targetCharacterGameObject;
        private AnimationManager AnimationManager = GlobalConstants.AnimationManager; 
        private ActionType _actionType = ActionType.Idle;
        private Tile _executionerTile;
        private Tile _targetTile;
        private int _damage = 0;

        public Animator(Tile executionerTile, Tile targetTile, ActionType actionType)
        {
            _executionerTile = executionerTile;
            _targetTile = targetTile;
            _actionType = actionType;

            _executionerCharacterGameObject = GameController.GetCharacterGameObject(executionerTile);
            _targetCharacterGameObject = GameController.GetCharacterGameObject(targetTile);

            _executionerAnimator = GameController.GetAnimator(executionerTile);
            _targetAnimator = GameController.GetAnimator(targetTile);
        }

        public void Cast(string ability)
        {
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
                AnimationManager.Animate(_executionerCharacterGameObject, _targetCharacterGameObject, _executionerAnimator, _targetAnimator, _targetTile, ability, _damage);
            }
            else
            {
                AnimationManager.Animate(_executionerAnimator);
            }
        }

        public void Attack(string ability, string weaponType = "")
        {
            _actionType = ActionType.Idle;
            if (_executionerCharacterGameObject.CharacterClassObject.SpriteId == 1)
            {
                weaponType = "isAttacking";
            }
            else if (_executionerCharacterGameObject.CharacterClassObject.SpriteId == 2)
            {
                weaponType = "isAttackingBow";
            }
            else
            {
                weaponType = "isAttackingSpear";
            }

            _executionerAnimator.SetBool(weaponType, true);
            float currentX = (float) (System.Math.Round(_executionerTile.transform.localPosition.x, 2));
            float currentY = (float) (System.Math.Round(_executionerTile.transform.localPosition.y, 2));
            float targetX = (float) (System.Math.Round(_targetTile.transform.localPosition.x + 1.6f, 2));
            float targetY = (float) (System.Math.Round(_targetTile.transform.localPosition.y, 2));
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
                AnimationManager.Animate(_executionerCharacterGameObject, _targetCharacterGameObject, _executionerAnimator, _targetAnimator, _targetTile, ability, weaponType, _damage);
            }
            else
            {
                AnimationManager.Animate(_executionerAnimator, weaponType); 
            }
        }
    }
}
