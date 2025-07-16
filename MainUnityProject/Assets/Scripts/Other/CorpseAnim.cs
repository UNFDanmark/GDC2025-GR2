using UnityEngine;

public class CorpseAnim : MonoBehaviour
{
    public int pose;
    private Animator anim;
    void Awake()
    {
        anim = GetComponent<Animator>();
        anim.SetInteger("Pose", pose);
        anim.Play("Pose 1");
    }
}
