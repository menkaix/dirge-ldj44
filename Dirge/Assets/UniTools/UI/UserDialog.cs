using UnityEngine;
using System.Collections;
using System;

public class UserDialog : MonoBehaviour
{
	public UserAnswer answer = new UserAnswer();

	public void setAnswer(DialogAnswer ans)
	{
		answer.value = ans ;
	}

	public void clickYes()
	{
		setAnswer(DialogAnswer.YES);
	}

	public void clickNo()
	{
		setAnswer(DialogAnswer.NO);
	}
	public void clickCancel()
	{
		setAnswer(DialogAnswer.CANCEL);
	}


}
