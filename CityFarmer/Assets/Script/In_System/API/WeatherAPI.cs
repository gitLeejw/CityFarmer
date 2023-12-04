using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
public class WeatherAPI : MonoBehaviour
{
    public WeatherData WeatherInfo;
    public string url = "";
    public void CheckCityWeather(float lat, float lon)
    {
        StartCoroutine(GetWeather(lat, lon));
    }

    IEnumerator GetWeather(float lat, float lon)
    {

        url = "https://api.openweathermap.org/data/2.5/weather?lat=" + lat + "&lon=" + lon + "&appid=6a8b8513981cf2cdf692039790b9f16f";

        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        string json = www.downloadHandler.text;
        json = json.Replace("\"base\":", "\"basem\":");
        WeatherInfo = JsonUtility.FromJson<WeatherData>(json);

        Debug.Log(WeatherInfo.weather[0].main);

    }
}
