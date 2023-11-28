using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class GPSManager : MonoBehaviour
{
   
   
    private float _maxWaitTime = 10.0f;
    private float _resendTime = 1.0f;
    public WeatherAPI Weather;
    //위도 경도 변경
    private float _latitude = 0;
    private float _longitude = 0;
    private float _waitTime = 0;

    private bool _receiveGPS = false;

    private static GPSManager _instance;
    public static GPSManager Instance
    {
        get
        {
            // 인스턴스가 없는 경우에 접근하려 하면 인스턴스를 할당해준다.
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(GPSManager)) as GPSManager;

                if (_instance == null)
                    Debug.Log("no Singleton obj");
            }

            return _instance;
        }
    }
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        // 인스턴스가 존재하는 경우 새로생기는 인스턴스를 삭제한다.
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
        // 아래의 함수를 사용하여 씬이 전환되더라도 선언되었던 인스턴스가 파괴되지 않는다.
        DontDestroyOnLoad(gameObject);
    }


    void Start()
    {
        StartCoroutine(GPS_On());
    }

    //GPS처리 함수

    public IEnumerator GPS_On()
    {
#if UNITY_EDITOR
        //유니티 리모트 대기
        while (!UnityEditor.EditorApplication.isRemoteConnected)
        {
            yield return null;
        }
#endif

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
            Debug.Log("수신 실패");
        }

        //응답 대기 시간을 넘어가도록 수신이 없었다면 시간 초과됐음을 출력
        if (_waitTime >= _maxWaitTime)
        {
            Debug.Log("시간 초과");
        }
        Input.location.Start();
       
      
        LocationInfo li = Input.location.lastData;

        _receiveGPS = true;

      
        while (_receiveGPS)
        {
            li = Input.location.lastData;
            _latitude = li.latitude;
            _longitude = li.longitude;
            if(_latitude != 0)
            {
                break;
            }
            yield return new WaitForSeconds(_resendTime);
        }

        Weather.CheckCityWeather(_latitude, _longitude);
    }
}
