using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DamageNumbersPro.Demo
{
    public class DNP_Enemy : MonoBehaviour
    {
        public static int count;

        public float speed = 5f;
        public float acceleration = 8f;

        //References:
        CharacterController controller;
        Animation anim;
        Transform pathPoints;
        Transform pelvis;

        //State:
        Vector3 velocity;
        Vector3 targetPosition;
        bool isWalkingToPath;
        int pathsLeft;
        float inactiveUntil;
        float walkStartTime;
        int hitsTaken;
        float nextHurtTime;
        bool firstIdle;
        bool isDead;

        void Start()
        {
            count++;

            //References:
            anim = GetComponent<Animation>();
            controller = GetComponent<CharacterController>();
            pathPoints = GameObject.Find("Special").transform.Find("Path Points");
            pelvis = transform.Find("Bip001");

            //State:
            targetPosition = pathPoints.GetChild(0).position;
            isWalkingToPath = true;
            pathsLeft = 1 + Mathf.RoundToInt(Random.value * 2f);
            inactiveUntil = 0;
            walkStartTime = Time.time;
            hitsTaken = 0;
            firstIdle = true;
            isDead = false;

            //Colors:
            SkinnedMeshRenderer meshRenderer = transform.Find("Medieve").GetComponent<SkinnedMeshRenderer>();
            Material[] materials = meshRenderer.materials;
            Color clothingColor = Color.HSVToRGB(Random.value, 0.4f + Random.value * 0.2f, 0.7f + Random.value * 0.3f);
            Color leatherColor = Color.Lerp(Color.white, clothingColor, Random.value * 0.4f);
            materials[0].SetColor("_Color", clothingColor);
            materials[1].SetColor("_Color", leatherColor);
            materials[6].SetColor("_Color", leatherColor);
            materials[2].SetColor("_Color", Color.HSVToRGB(0.02f + 0.05f * Random.value, Random.value * 0.45f, 0.8f + Random.value * 0.35f));
            materials[4].SetColor("_Color", Color.HSVToRGB(Random.value, Random.value * 0.8f, Random.value * 0.6f));
            meshRenderer.materials = materials;
        }

        void FixedUpdate()
        {
            if (Time.time < inactiveUntil) return;

            if (isDead)
            {
                if(controller.enabled)
                {
                    controller.enabled = false;
                }

                transform.position += new Vector3(0, - Time.fixedDeltaTime * 0.2f, 0);
                return;
            }

            if (isWalkingToPath)
            {
                Quaternion lookRotation = Quaternion.LookRotation(targetPosition - transform.position, Vector3.up);
                lookRotation.x = 0;
                lookRotation.z = 0;

                transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.fixedDeltaTime * 8f);
                velocity = Vector3.Lerp(velocity, transform.forward * speed, Time.fixedDeltaTime * acceleration);
                controller.Move(velocity * Time.fixedDeltaTime);

                if (Vector3.Distance(transform.position, targetPosition) < (Time.time - walkStartTime) * 0.5f)
                {
                    isWalkingToPath = false;

                    if(firstIdle)
                    {
                        firstIdle = false;
                    }
                    else
                    {
                        anim.CrossFade("Villager_Idle", 0.2f);
                        inactiveUntil = Time.time + 1f + Random.value;
                    }
                }
                else
                {
                    anim.CrossFade("Villager_Walk", 0.2f);
                }
            }
            else
            {
                velocity = Vector3.zero;

                if(pathsLeft > 0)
                {
                    pathsLeft--;
                    targetPosition = pathPoints.GetChild(Random.Range(1, 21)).position;
                    isWalkingToPath = true;
                    inactiveUntil = 0;
                    walkStartTime = Time.time;
                }
                else
                {
                    anim.CrossFade("Villager_Idle", 0.3f);
                    inactiveUntil = Time.time + 0.2f;
                }
            }
        }

        public void Hurt(int damage)
        {
            if (Time.time < nextHurtTime || isDead || transform.position.z > 8) return;

            nextHurtTime = Time.time + 0.55f;
            hitsTaken += damage;

            if(hitsTaken >= 5)
            {
                anim.CrossFade("Villager_Death", 0.05f);
                inactiveUntil = Time.time + 2f;
                isDead = true;
                Destroy(gameObject, 7f);
                count--;
            }
            else
            {
                inactiveUntil = Time.time + 1f + hitsTaken * 0.1f;
                anim.CrossFade("Villager_Hurt", 0.05f);
                anim.CrossFadeQueued("Villager_Idle", 0.5f);
            }
        }
        public Transform GetPelvis()
        {
            return pelvis;
        }
    }
}
