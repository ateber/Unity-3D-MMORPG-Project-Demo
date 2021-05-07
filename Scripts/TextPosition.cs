using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextPosition : MonoBehaviour
{
    Transform root;
    // Start is called before the first frame update
    void Start()
    {
        root = this.transform.root ;
        Renderer[] renderers = root.transform.gameObject.GetComponentsInChildren<Renderer>();
        Bounds bounds = renderers[0].bounds;
        for (int i = 1; i < renderers.Length; ++i)
        {
            bounds.Encapsulate(renderers[i].bounds.min);
            bounds.Encapsulate(renderers[i].bounds.max);
        }
        float textPosition = -bounds.size.z/2-4;
        this.transform.localPosition= new Vector3(0,0, textPosition); 

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
