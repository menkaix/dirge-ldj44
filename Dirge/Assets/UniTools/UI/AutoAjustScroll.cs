using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(RectTransform))]
public class AutoAjustScroll : MonoBehaviour {

    RectTransform rectTransform;

    

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        int c = transform.childCount;

        float width = 0;
        float height = 0;
        

        for(int i = 0 ; i < c; i++)
        {
            Transform child = transform.GetChild(i);

            if (child.gameObject.activeInHierarchy)
            {
                RectTransform rtf = child.GetComponent<RectTransform>();
                
                Rect rect = rtf.rect;
                

                float tx = (rtf.anchoredPosition.x + rect.width/2) ;
                float ty = -(rtf.anchoredPosition.y - rect.height/2) ;
                
                Debug.Log(child.name+" : "+rtf.anchoredPosition.x + ";"+ rtf.anchoredPosition.y + ";"+ rect.width + ";"+ rect.height + ";");

                if (tx > width)
                {                    
                    width = tx;
                }

                if (ty > height)
                {                    
                    height = ty;
                }

            }

            gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);

        }
	}
}
