using UnityEngine;

public class Corpse : MonoBehaviour
{
    private float decayMaxTime = 20;
    private float decayTime;
    [SerializeField] CorpseAnim anim;
    /*[SerializeField] private SkinnedMeshRenderer mat1;
    [SerializeField] private MeshRenderer[] mat2;*/

    private void Awake()
    {
        decayTime = decayMaxTime;
        anim.pose = Random.Range(1, 6);
    }

    void Update()
    {
        decayTime -= Time.deltaTime;
        /*Color mat1Color = new Color(1, 1, 1, Mathf.Lerp(1, 0, decayTime / decayMaxTime));
        mat1.material.color = mat1Color;
        Color mat2Color = new Color(0.5320753f, 0.1034032f, 0.1034032f, Mathf.Lerp(1, 0, decayTime / decayMaxTime));
        for (int i = 0; i < mat2.Length; i++)
        {
            mat2[i].material.color = mat2Color;
        }
        */
        if (decayTime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
