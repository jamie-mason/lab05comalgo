using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterIKFoot : MonoBehaviour
{
    CharacterIK characterIK;
    Vector3 p_leftFoot;
    Vector3 p_rightFoot;


    void Start()
    {
        characterIK = GetComponentInParent<CharacterIK>();

    }
    
    private void OnAnimatorIK(int layerIndex)
    {
        if (characterIK.GetAnimator())
        {
            p_leftFoot = characterIK.GetAnimator().GetBoneTransform(HumanBodyBones.LeftFoot).position + characterIK.footIKOffset;
            p_rightFoot = characterIK.GetAnimator().GetBoneTransform(HumanBodyBones.RightFoot).position + characterIK.footIKOffset;
            if (characterIK.activeFootIK)
            {
                p_leftFoot = GetHitPoint(p_leftFoot + Vector3.up, p_leftFoot - Vector3.up * 0.6f);
                p_rightFoot = GetHitPoint(p_leftFoot + Vector3.up, p_leftFoot - Vector3.up * 0.6f);

                transform.localPosition = new Vector3(0f, -Mathf.Abs(p_leftFoot.y - p_rightFoot.y) / 2, 0f);


                characterIK.GetAnimator().SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1f);
                characterIK.GetAnimator().SetIKPosition(AvatarIKGoal.LeftFoot, p_leftFoot);

                characterIK.GetAnimator().SetIKPositionWeight(AvatarIKGoal.RightFoot, 1f);
                characterIK.GetAnimator().SetIKPosition(AvatarIKGoal.RightFoot, p_rightFoot);
            }
            else
            {
                characterIK.GetAnimator().SetIKPositionWeight(AvatarIKGoal.LeftFoot, 0f);
                characterIK.GetAnimator().SetIKPositionWeight(AvatarIKGoal.RightFoot, 0f);
            }
        }
    }
    private Vector3 GetHitPoint(Vector3 start, Vector3 end)
    {
        RaycastHit hit;
        var line = Physics.Linecast(start, end, out hit);
        //Debug.DrawLine(start,end, Color.blue);

        if (line)
        {

            return hit.point;
        }
        else
        {
            return end;
        }

    }
    private bool GetHitPoint(Vector3 start, Vector3 end, int useless)
    {
        RaycastHit hit;
        var line = Physics.Linecast(start, end, out hit);
        //Debug.DrawLine(start,end, Color.blue);

        return line;

    }
}
