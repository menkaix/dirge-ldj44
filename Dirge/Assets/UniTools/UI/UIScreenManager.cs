using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public enum DialogAnswer
{
	PENDING = 0,
	YES = 1,
	NO = 2,
	CANCEL = 3

}

public enum DialogType
{
	CANCEL,
	YES_NO,
	YES_NO_CANCEL
}

public delegate void DialogCallback(DialogAnswer userAnswer);

public class UIScreenManager : MonoBehaviour {

    private List<UIScreen> screens;
    private int currentPage = 0;
    private ReadableStack<int> history = new ReadableStack<int>();
	private GameObject currentDialog = null ;

    public int defaultPage = 0;


	// Use this for initialization
	void Awake () {
		screens = new List<UIScreen>();

		int p=0;

        for(int i=0; i< transform.childCount; i++)
        {
			UIScreen screen = transform.GetChild(i).GetComponent<UIScreen>();

			if(screen != null && screen.enabled)
			{
				screen.pageID = p++;
				screens.Add(screen);
				screen.uiManager = this;
			}

        }

        EnableScreen(defaultPage);
        history.Add(defaultPage);
        
	}
	
	// Update is called once per frame
	void Update () {
        	
	}
	public void EnableScreen(int i)
    {
        if(i<0 || i >= screens.Count)
        {
            return;
        }

		if (history.Count > 0)
		{
			if(history[history.Count - 1] == i)
			{
				HistoryBack();
				return;
			}
		}


        history.Add(currentPage);

        for(int j = 0; j<screens.Count ; j++)
        {
            if (i == j)
            {
                screens[i].gameObject.SetActive(true);
            }
            else
            {
                screens[j].gameObject.SetActive(false);
            }
        }

        currentPage = i;

    }

    public void EnableScreen(string s)
    {
        int i = -1;
        int j = -1;

        foreach (UIScreen scr in screens)
        {
            i++;

            if(scr.gameObject.name == s)
            {
                j = i;
            }
        }
        EnableScreen(j);
	}

	public void EnableScreen(UIScreen screen)
	{
		EnableScreen(screen.pageID);
	}

    public void HistoryBack()
    {
        int i = history.Depile();

        for (int j = 0; j < screens.Count; j++)
        {
            if (i == j)
            {
                screens[i].gameObject.SetActive(true);
            }
            else
            {
                screens[j].gameObject.SetActive(false);
            }
        }

        currentPage = i;
    }

    public void NextPage()
    {
        EnableScreen(currentPage + 1);
    }

    public void PreviousPage()
    {
        EnableScreen(currentPage - 1);
    }

    public void GoHome()
    {
        EnableScreen(defaultPage);
    }
		
}
