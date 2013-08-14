using UnityEngine;
using System.Collections;

public class DailyBonus : MonoBehaviour {
	
	public static int RewardedCurrency {get; set;}
	
	public GameObject DayOne;
	public GameObject DayTwo;
	public GameObject DayThree;
	public GameObject DayFour;
	public GameObject DayFive;
	
	// Use this for initialization
	void Start () {
		
		RewardedCurrency = 0;
		PlayHaven.PlayHavenManager.instance.OnRewardGiven += DailyRewardGiven;
		
		//Has player already played the game before at all?
		if(PlayerPrefs.HasKey("DailyBonusDay") && PlayerPrefs.HasKey("DailyBonusDate"))
		{
			//Only continue if player has not played today and its not bonus day 1
			if(PlayerPrefs.GetInt("DailyBonusDate") != System.DateTime.Now.DayOfYear && PlayerPrefs.GetInt("DailyBonusDay") != 1)
			{
				//Continue if current date is one day after saved date
				if(PlayerPrefs.GetInt("DailyBonusDate") + 1 == System.DateTime.Now.DayOfYear)
				{
					if(PlayerPrefs.GetInt("DailyBonusDay") == 5)
					{
						DayFive.SendMessage("RequestPlayHavenContent");
						PlayerPrefs.DeleteKey("DailyBonusDay");
						PlayerPrefs.SetInt("DailyBonusDate", System.DateTime.Now.DayOfYear);
					}
					else if(PlayerPrefs.GetInt("DailyBonusDay") == 4)
					{
						DayFour.SendMessage("RequestPlayHavenContent");
						PlayerPrefs.SetInt("DailyBonusDay", 5);
						PlayerPrefs.SetInt("DailyBonusDate", System.DateTime.Now.DayOfYear);
					}
					else if(PlayerPrefs.GetInt("DailyBonusDay") == 3)
					{
						DayThree.SendMessage("RequestPlayHavenContent");
						PlayerPrefs.SetInt("DailyBonusDay", 4);
						PlayerPrefs.SetInt("DailyBonusDate", System.DateTime.Now.DayOfYear);
					}
					else if(PlayerPrefs.GetInt("DailyBonusDay") == 2)
					{
						DayTwo.SendMessage("RequestPlayHavenContent");
						PlayerPrefs.SetInt("DailyBonusDay", 3);
						PlayerPrefs.SetInt("DailyBonusDate", System.DateTime.Now.DayOfYear);
					}					
				}
				//if current date is not one day after saved date, reset daily bonus
				else
				{
					PlayerPrefs.SetInt("DailyBonusDay", 1);
					PlayerPrefs.SetInt("DailyBonusDate", System.DateTime.Now.DayOfYear);
				}
			}
			//Day one reward
			else if(PlayerPrefs.GetInt("DailyBonusDate") != System.DateTime.Now.DayOfYear && PlayerPrefs.GetInt("DailyBonusDay") == 1)
			{
				DayOne.SendMessage("RequestPlayHavenContent");
				PlayerPrefs.SetInt("DailyBonusDay", 2);
				PlayerPrefs.SetInt("DailyBonusDate", System.DateTime.Now.DayOfYear);
			}
		}
		//First time ever playing game
		else
		{
			PlayerPrefs.SetInt("DailyBonusDay", 1);
			PlayerPrefs.SetInt("DailyBonusDate", System.DateTime.Now.DayOfYear);
		}
		
		Debug.Log("Starting Currency is: " + RewardedCurrency);
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void DailyRewardGiven(int requestID, PlayHaven.Reward reward)
	{
		RewardedCurrency += reward.quantity;
		Debug.Log("Ending Currency is: " + RewardedCurrency);
	}
}
