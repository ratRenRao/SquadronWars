using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.GameClasses;
using UnityEngine;
using UnityEngine.UI;
using Animator = UnityEngine.Animator; 

namespace Assets.Scripts
{
    public class ActionAnimator : MonoBehaviour
    {
        public void Animate(CharacterGameObject executioner, CharacterGameObject target, Animator executionerAnimator, Animator targetAnimator, Tile targetTile, string ability, string attackType, int damage = 0, bool crit = false)
        {
            StartCoroutine(AttackAnimation(executioner, target, executionerAnimator, targetAnimator, targetTile, ability, attackType, damage, crit));
        }

        public void Animate(CharacterGameObject executioner, CharacterGameObject target, Animator executionerAnimator, Animator targetAnimator, Tile targetTile, string ability, int damage = 0)
        {
            Debug.Log(ability);
            StartCoroutine(CastAnimation(executioner, target, executionerAnimator, targetAnimator, targetTile, ability, damage));
        }

        public void Animate(CharacterGameObject target, Animator targetAnimator, Tile targetTile, int damage = 0)
        {
            Debug.Log("Lingering Effect Damage: " + damage);
            StartCoroutine(LingeringEffectAnimation(target, targetAnimator, targetTile, damage));
        }

        public void Animate(Animator executionerAnimator, string attackType = "none")
        {
            if (attackType.Equals("none"))
                StartCoroutine(CastAnimationNothing(executionerAnimator));
            else
                StartCoroutine(AttackAnimationNothing(executionerAnimator, attackType));
        }


        IEnumerator AttackAnimation(CharacterGameObject executioner, CharacterGameObject target, Animator executionerAnimator, Animator targetAnimator, Tile targetTile, string ability, string attackType, int damage, bool crit)
        {
            yield return new WaitForSeconds(.2f);
            targetAnimator.SetBool("isAttacked", true);
            if (attackType == "isAttackingSpear")
            {
                yield return new WaitForSeconds(.55f);
            }
            if (attackType == "isAttacking")
            {
                yield return new WaitForSeconds(.3f);
            }
            if (attackType == "isAttackingBow")
            {
                yield return new WaitForSeconds(.7f);
            }
            executionerAnimator.SetBool(attackType, false);
            targetAnimator.SetBool("isAttacked", false);
            Debug.Log(ability);
            if (ability != "attack")
            {
                Debug.Log("show ability called " + ability);
                var temp = (GameObject)Resources.Load(("SpellPrefabs/" + ability), typeof(GameObject));
                Debug.Log(temp);
                var spell = GameObject.Instantiate(temp, new Vector3(targetTile.transform.position.x + 1.6f, targetTile.transform.position.y - .5f), Quaternion.identity) as GameObject;
                spell.GetComponent<SpriteRenderer>().sortingOrder = 7 + (targetTile.y * 2);
                spell.transform.parent = targetTile.transform;
                spell.transform.localScale = new Vector3(1, 1, 0.0f);
                //damage = CalculateMagicDamage(ability);
            }
            else
            {
                //damage = CalculateDamage();
            }
            var particleCanvas = GameObject.FindGameObjectWithTag("ParticleCanvas");
            var damageText = (GameObject)Resources.Load(("Prefabs/DamageText"), typeof(GameObject));
            var dmgObject = GameObject.Instantiate(damageText, new Vector3(targetTile.transform.position.x + 1.6f, targetTile.transform.position.y + 3.2f), Quaternion.identity) as GameObject;
            dmgObject.transform.parent = particleCanvas.transform;
            //damage = (damage <= 0) ? 1 : damage;
            var textObject = dmgObject.GetComponent<Text>();
            /*textObject.color = Color.red;
            textObject.fontStyle = FontStyle.Bold;*/
            textObject.text = damage.ToString();
            
            //target.CharacterClassObject.CurrentStats.CurHP -= damage;
            yield return new WaitForSeconds(.4f);
            if (target.CharacterClassObject.CurrentStats.CurHP <= 0)
            {
                //target.CharacterClassObject.CurrentStats.CurHP = 0;
                target.isDead = true;
                //myCharacters.Remove(targetCharacterGameObject.gameObject);
                targetAnimator.SetBool("isDead", true);
                yield return new WaitForSeconds(.8f);
                Debug.Log(target.CharacterClassObject.Name);
            }
            GlobalConstants.GameController.ResetData();
            StartCoroutine(InjuredAnimation(targetAnimator));
        }

        IEnumerator CastAnimation(CharacterGameObject executioner, CharacterGameObject target, Animator executionerAnimator, Animator targetAnimator, Tile targetTile, string ability, int damage)
        {
            yield return new WaitForSeconds(.5f);
            Debug.Log("Cast Animation Called");
            Debug.Log(ability);
            executionerAnimator.SetBool("isCasting", false);
            float wait = 0;
            if (ability != null)
            {
                GameObject spell;
                if(ability == "UltimateSummon")
                {
                    GlobalConstants.GameController.battlesong.mute = true;
                    GlobalConstants.GameController.funsong.Play();
                    var temp = (GameObject)Resources.Load(("SpellPrefabs/" + ability), typeof(GameObject));
                    spell = GameObject.Instantiate(temp, new Vector3(targetTile.transform.parent.transform.position.x + 18, targetTile.transform.parent.transform.position.y - 35), Quaternion.identity) as GameObject;
                    spell.GetComponent<SpriteRenderer>().sortingOrder = 75;
                    spell.transform.parent = targetTile.transform.parent.transform;
                    spell.transform.localScale = new Vector3(6, 12, 0.0f);
                }
                else if (ability == "MeteorShower")
                {
                    var temp = (GameObject)Resources.Load(("SpellPrefabs/" + ability), typeof(GameObject));
                    spell = GameObject.Instantiate(temp, new Vector3(targetTile.transform.parent.transform.position.x + 35, targetTile.transform.parent.transform.position.y - 25), Quaternion.identity) as GameObject;
                    spell.GetComponent<SpriteRenderer>().sortingOrder = 7 + (targetTile.y * 2);
                    spell.transform.parent = targetTile.transform.parent.transform;
                    spell.transform.localScale = new Vector3(10, 10, 0.0f);
                }
                else {
                    Debug.Log(ability);
                    var temp = (GameObject)Resources.Load(("SpellPrefabs/" + ability), typeof(GameObject));
                    spell = GameObject.Instantiate(temp, new Vector3(targetTile.transform.position.x + 1.6f, targetTile.transform.position.y - .5f), Quaternion.identity) as GameObject;
                    spell.GetComponent<SpriteRenderer>().sortingOrder = 7 + (targetTile.y * 2);
                    spell.transform.parent = targetTile.transform;
                    spell.transform.localScale = new Vector3(1, 1, 0.0f);
                }
                yield return new WaitForSeconds(.2f);
                if(targetAnimator != null)
                    targetAnimator.SetBool("isAttacked", true);
                yield return new WaitForSeconds(.4f);
                if(targetAnimator != null)
                    targetAnimator.SetBool("isAttacked", false);
                wait = spell.GetComponent<AutoDestroy>().animTime + .1f;
                //damage = CalculateMagicDamage(ability);

                yield return new WaitForSeconds(wait);
                if (targetAnimator != null)
                {
                    var particleCanvas = GameObject.FindGameObjectWithTag("ParticleCanvas");
                    var damageText = (GameObject) Resources.Load(("Prefabs/DamageText"), typeof (GameObject));
                    var dmgObject =
                        GameObject.Instantiate(damageText,
                            new Vector3(targetTile.transform.position.x + 1.6f, targetTile.transform.position.y + 3.2f),
                            Quaternion.identity) as GameObject;
                    dmgObject.transform.parent = particleCanvas.transform;
                    //damage = (damage <= 0) ? 1 : damage;
                    if(damage < 0)
                    {
                        dmgObject.GetComponent<Text>().color = new Color32(146, 255, 255, 255);
                        damage = Math.Abs(damage);
                    }
                    dmgObject.GetComponent<Text>().text = damage.ToString();
                    Debug.Log(target.CharacterClassObject.CurrentStats.CurHP);
                }
                var gameover = false;
                if (target.CharacterClassObject.CurrentStats.CurHP <= 0)
                {
                    //target.CharacterClassObject.CurrentStats.CurHP = 0;
                    target.isDead = true;
                    targetTile.isDead = true;
                    //myCharacters.Remove(target.gameObject);
                    if (targetAnimator != null)
                        targetAnimator.SetBool("isDead", true);
                    yield return new WaitForSeconds(.8f);
                    Debug.Log(target.CharacterClassObject.Name);
                    if(GlobalConstants.GameController.CheckGameOver()){
                        gameover = true;
                    }
                    if (GlobalConstants.GameController.CheckVictory())
                    {
                        Debug.Log("Victory");
                        gameover = true;
                    }
                }
            }
            yield return new WaitForSeconds(.5f);
            GlobalConstants.GameController.ResetData();
            GlobalConstants.GameController.battlesong.mute = false;
            if (targetAnimator != null)
                StartCoroutine(InjuredAnimation(targetAnimator));
        }

        IEnumerator LingeringEffectAnimation(CharacterGameObject target, Animator targetAnimator, Tile targetTile, int damage)
        {
            targetTile = GlobalConstants.GameController.tileMap.tileArray[target.X, target.Y];
            if (targetAnimator != null)
            {
                var particleCanvas = GameObject.FindGameObjectWithTag("ParticleCanvas");
                var damageText = (GameObject)Resources.Load(("Prefabs/DamageText"), typeof(GameObject));
                var dmgObject =
                    GameObject.Instantiate(damageText,
                        new Vector3(targetTile.transform.position.x + 1.6f, targetTile.transform.position.y + 3.2f),
                        Quaternion.identity) as GameObject;
                dmgObject.transform.parent = particleCanvas.transform;
                //damage = (damage <= 0) ? 1 : damage;
                if (damage < 0)
                {
                    dmgObject.GetComponent<Text>().color = new Color32(146, 255, 255, 255);
                    damage = Math.Abs(damage);
                }
                dmgObject.GetComponent<Text>().text = damage.ToString();
                Debug.Log(target.CharacterClassObject.CurrentStats.CurHP);
            }
            var gameover = false;
            if (target.CharacterClassObject.CurrentStats.CurHP <= 0)
            {
                //target.CharacterClassObject.CurrentStats.CurHP = 0;
                target.isDead = true;
                targetTile.isDead = true;
                //myCharacters.Remove(target.gameObject);
                if (targetAnimator != null)
                    targetAnimator.SetBool("isDead", true);
                yield return new WaitForSeconds(.8f);
                Debug.Log(target.CharacterClassObject.Name);
                if (GlobalConstants.GameController.CheckGameOver())
                {
                    gameover = true;
                }
                if (GlobalConstants.GameController.CheckVictory())
                {
                    Debug.Log("Victory");
                    gameover = true;
                }
            }

            yield return new WaitForSeconds(.5f);
            GlobalConstants.GameController.ResetData();
                GlobalConstants.GameController.battlesong.mute = false;
                if (targetAnimator != null)
                    StartCoroutine(InjuredAnimation(targetAnimator));
       }

        IEnumerator CastAnimationNothing(Animator executionerAnimator)
        {
            yield return new WaitForSeconds(.5f);
            executionerAnimator.SetBool("isCasting", false);
            GlobalConstants.GameController.ResetData();
        }

        IEnumerator AttackAnimationNothing(Animator executionerAnimator, string attackType)
        {
            yield return new WaitForSeconds(.5f);
            executionerAnimator.SetBool(attackType, false);
            GlobalConstants.GameController.ResetData();
        }

        IEnumerator InjuredAnimation(Animator targetAnimator)
        {
            yield return new WaitForSeconds(.4f);
            targetAnimator.SetBool("isAttacked", false);
            GlobalConstants.GameController.ResetData();
        }
    }
}
