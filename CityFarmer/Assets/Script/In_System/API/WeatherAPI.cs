using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
public class WeatherAPI : MonoBehaviour
{
    //API аж╪р
    //===============================================
    public string APP_ID;
   
    public WeatherData weatherInfo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void CheckCityWeather(float lat,float lon)
    {
        StartCoroutine(GetWeather(lat,lon));
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator GetWeather(float lat, float lon)
    {
       
        string url = "https://api.openweathermap.org/data/2.5/weather?lat="+lat+"&lon="+lon+"&appid=6a8b8513981cf2cdf692039790b9f16f";

        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        string json = www.downloadHandler.text;
        json = json.Replace("\"base\":", "\"basem\":");
        weatherInfo = JsonUtility.FromJson<WeatherData>(json);

        Debug.Log(weatherInfo.weather[0].main);

    }
}
