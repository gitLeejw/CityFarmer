using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
public class WeatherAPI : MonoBehaviour
{
    //API 주소
    //===============================================
    public const string API_ADDRESS = @"api.openweathermap.org/data/2.5/weather?q=Seoul&appid=e";
    //===============================================

    //날씨 데이터가 다운로드되면 CallBack으로 필요한 함수로 돌아간다
    public delegate void WeatherDataCallback(WeatherData weatherData);

    //다운로드된 날씨 데이터. 중복 다운로드를 막기위하여 저장해둔다
    private WeatherData _weatherData;

    /// <summary>
    /// API로부터 날씨 데이터를 받아온다
    /// </summary>
    public void GetWeather(WeatherDataCallback callback)
    {
        //현재의 날씨 데이터가 없다면 API로부터 받아온다
        if (_weatherData == null)
        {
            StartCoroutine(CoGetWeather(callback));
        }
        else
        {
            //현재의 날씨 데이터가 존재한다면 그 날씨데이터를 그대로 사용한다
            callback(_weatherData);
        }
    }

    /// <summary>
    /// 날씨 API로부터 정보를 받아온다
    /// </summary>
    /// <param name="callback"></param>
    /// <returns></returns>
    [System.Obsolete]
    private IEnumerator CoGetWeather(WeatherDataCallback callback)
    {
        Debug.Log("날씨 정보를 다운로드합니다");

        var webRequest = UnityWebRequest.Get(API_ADDRESS);
        yield return webRequest.SendWebRequest();

        //만약 에러가 있을 경우
        if (webRequest.isHttpError || webRequest.isNetworkError)
        {
            Debug.Log(webRequest.error);
            yield break;
        }

        //다운로드 완료
        var downloadedTxt = webRequest.downloadHandler.text;

        Debug.Log("날씨 정보가 다운로드 되었습니다! : " + downloadedTxt);

        //유니티 언어와 겹치므로 base를 사용할 수 없기때문에 Replace가 필요하다
        string weatherStr = downloadedTxt.Replace("base", "station");

        _weatherData = JsonUtility.FromJson<WeatherData>(weatherStr);
        callback(_weatherData);
    }
}
