using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class GPSManager : MonoBehaviour
{
    public static GPSManager Instance;
   
    private float _maxWaitTime = 10.0f;
    private float _resendTime = 1.0f;
    public WeatherAPI Weather;
    //위도 경도 변경
    private float _latitude = 0;
    private float _longitude = 0;
    private float _waitTime = 0;

    private bool _receiveGPS = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    [System.Obsolete]
    void Start()
    {
        StartCoroutine(GPS_On());
    }

    //GPS처리 함수
    [System.Obsolete]
    public IEnumerator GPS_On()
    {
       
        //만일,GPS사용 허가를 받지 못했다면, 권한 허가 팝업을 띄움
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
            while (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
            {
                yield return null;
            }
        }

        //만일 GPS 장치가 켜져 있지 않으면 위치 정보를 수신할 수 없다고 표시
#if UNITY_EDITOR
        //Wait until Unity connects to the Unity Remote, while not connected, yield return null
        while (!UnityEditor.EditorApplication.isRemoteConnected)
        {
            yield return null;
        }
#endif
      
        if (!Input.location.isEnabledByUser)
        {
         
            yield break;
        }

        //위치 데이터를 요청 -> 수신 대기
        Input.location.Start();

        //GPS 수신 상태가 초기 상태에서 일정 시간 동안 대기함
        while (Input.location.status == LocationServiceStatus.Initializing && _waitTime < _maxWaitTime)
        {
            yield return new WaitForSeconds(1.0f);
            _waitTime++;
        }

        //수신 실패 시 수신이 실패됐다는 것을 출력
        if (Input.location.status == LocationServiceStatus.Failed)
        {
        
        }

        //응답 대기 시간을 넘어가도록 수신이 없었다면 시간 초과됐음을 출력
        if (_waitTime >= _maxWaitTime)
        {
          
        }

        //수신된 GPS 데이터를 화면에 출력/

        LocationInfo li = Input.location.lastData;
        /*latitude = li.latitude;
       longitude = li.longitude;
       latitude_text.text = "위도 : " + latitude.ToString();
       longitude_text.text = "경도 : " + longitude.ToString();
       */
        //위치 정보 수신 시작 체크
        _receiveGPS = true;
        Weather.CheckCityWeather(_latitude, _longitude);
        //위치 데이터 수신 시작 이후 resendTime 경과마다 위치 정보를 갱신하고 출력
        while (_receiveGPS)
        {
            li = Input.location.lastData;
            _latitude = li.latitude;
            _longitude = li.longitude;

            yield return new WaitForSeconds(_resendTime);
        }
      
        
    }
}
