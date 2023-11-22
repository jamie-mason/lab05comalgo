using DitzelGames.FastIK;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class CharacterIK : MonoBehaviour
{
    private Animator animator;
    public Vector3 footIKOffset;
    public Vector3 HandIKOffset;
    Vector3 pos;
    public bool activeHandIK;
    public bool activeFootIK;
    public float speed;
    public float distanceZ;
    public float distanceY;
    float sZ;

    public float jumpForce;
    public Transform target;


    void Awake()
    {
        animator = GetComponent<Animator>();

        activeFootIK = true;
    }

    void moveCharacter()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");


        var moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        Vector3 lookDirection = moveDirection + transform.parent.position;
        transform.parent.LookAt(lookDirection);
        transform.parent.position = transform.parent.position + moveDirection * Time.deltaTime * speed;
    }
    // Update is called once per frame
    private void Update()
    {
        GetHitPoint(transform.position + Vector3.up * 0.5f, transform.position + Vector3.up * 0.5f + Vector3.forward);
        Debug.DrawLine(transform.position + Vector3.up * 0.5f, transform.position + Vector3.up * 0.5f + Vector3.forward, Color.blue);
        distanceZ = (transform.position.z + sZ * transform.lossyScale.z / 2) - (target.position.z + sZ * target.lossyScale.z / 2 );
        distanceY = (transform.position.y + transform.lossyScale.y / 2) - (target.position.y + target.lossyScale.y / 2);

        Debug.Log(distanceY);
    }
    
    private void OnAnimatorIK(int layerIndex)
    {
        if (animator)
        {
            Vector3 p_leftHand = animator.GetBoneTransform(HumanBodyBones.LeftHand).position + HandIKOffset;
            Vector3 p_rightHand = animator.GetBoneTransform(HumanBodyBones.RightHand).position + HandIKOffset;

            if (target != null)
            {
                sZ = Mathf.Sign(target.position.z - transform.position.z);
                var leftTargetPos = new Vector3((p_leftHand.x - transform.lossyScale.x / 4), target.position.y + target.lossyScale.y / 2, (target.position.z + sZ * target.lossyScale.z / 2));
                var rightTargetPos = new Vector3(p_rightHand.x + transform.lossyScale.x / 4, target.position.y + target.lossyScale.y / 2, target.position.z + sZ * target.lossyScale.z / 2);
                if (Mathf.Abs(distanceZ) >= target.lossyScale.z + 0.3f && Mathf.Abs(distanceY) <= target.transform.position.y + target.lossyScale.y/2)
                {
                    activeHandIK = false;
                    activeFootIK = true;


                }
                else
                {
                    activeHandIK = true;
                    activeFootIK = false;

                }
                if (activeHandIK)
                {
                    animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
                    animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1f);
                    animator.SetIKPosition(AvatarIKGoal.LeftHand, leftTargetPos + HandIKOffset);
                    animator.SetIKRotation(AvatarIKGoal.LeftHand, Quaternion.Euler(target.eulerAngles.x, transform.eulerAngles.y, 0));

                    animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
                    animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);
                    animator.SetIKPosition(AvatarIKGoal.RightHand, rightTargetPos + HandIKOffset);
                    animator.SetIKRotation(AvatarIKGoal.RightHand, Quaternion.Euler(target.eulerAngles.x, transform.eulerAngles.y, 0));

                    


                    activeFootIK = true;
                    activeHandIK = false;

                }
                else
                {
                    activeHandIK = false;
                    activeFootIK = true;
                    animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0f);
                    animator.SetIKPosition(AvatarIKGoal.LeftHand, p_leftHand);
                    animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0f);
                    animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0f);
                    animator.SetIKPosition(AvatarIKGoal.LeftHand, p_rightHand);
                    animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0f);

                }

            }

        }


    }

    public Animator GetAnimator()
    {
        return animator;
    }
    private Vector3 GetHitPoint(Vector3 start, Vector3 end)
    {
        RaycastHit hit;
        var line = Physics.Linecast(start, end, out hit);
        

        if (line)
        {
            target = hit.collider.gameObject.transform;

            return hit.point;
        }
        else
        {
            return end;
        }

    }

}
