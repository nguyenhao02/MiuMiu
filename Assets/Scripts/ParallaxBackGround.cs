using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackGround : MonoBehaviour
{
    Material mat;
    float distance;
    [Range(0f, 0.5f)]
    public float speed = 0.2f;
    public bool isRightToLeft = true;

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        distance += Time.deltaTime * speed;
        if(isRightToLeft)
        {
            mat.SetTextureOffset("_MainTex", Vector2.right * distance); 
        }
        else 
        {
            mat.SetTextureOffset("_MainTex", Vector2.left * distance); 
        }
    }
}
