using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuController : MonoBehaviour
{
    public GameObject StartScreen;
    public GameObject GaraScreen;
    public GameObject rankingScreen;
    public Transform rankingPosition;
    public GameObject rankingItemPrefab;
    public GameObject[] vehicles;
    private int currentVehicleIndex;
    private UserData[] datas;
    private string isHome;

    // Use this for initialization
    void Start()
    {
        isHome = PlayerPrefs.GetString("Home", "Home");
        if (isHome == "Home")
        {
            StartScreen.SetActive(true);
            GaraScreen.SetActive(false);
        }
        else
        {
            StartScreen.SetActive(false);
            GaraScreen.SetActive(true);
        }
    }
    public void TurnOnRanking()
    {
        rankingScreen.SetActive(true);
        StartScreen.SetActive(false);
        if (datas == null)
        {
            datas = DataManager.Instance.dataCollection.datas;
            for (int i = 0; i < datas.Length; i++)
            {
                GameObject item = Instantiate(rankingItemPrefab, rankingPosition);
                item.GetComponent<RankingItem>().SetRankingItem((i + 1).ToString(), datas[i].name, datas[i].score.ToString());
            }
        }
    }
    public void TurnOffRanking()
    {
        StartScreen.SetActive(true);
        rankingScreen.SetActive(false);
    }
    public void TurnOffGara()
    {
        StartScreen.SetActive(true);
        GaraScreen.SetActive(false);
    }
    public void StartButtonClick()
    {
        StartScreen.SetActive(false);
        GaraScreen.SetActive(true);
    }
    public void OkGaraButtonClick()
    {
        DataManager.Instance.dataCollection.vehicleIndex = currentVehicleIndex;
        SceneManager.LoadScene("Main");
    }

    public void NextButtonClick(int i)
    {
        vehicles[currentVehicleIndex].SetActive(false);
        currentVehicleIndex = currentVehicleIndex + i;
        if (currentVehicleIndex > 4)
            currentVehicleIndex = 0;
        else if (currentVehicleIndex < 0)
            currentVehicleIndex = 4;
        vehicles[currentVehicleIndex].SetActive(true);
    }


}
