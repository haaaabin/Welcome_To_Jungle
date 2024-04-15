using UnityEngine;
using BackEnd;

/* BackendManager.cs
 * 뒤끝의 기본 기능을 정의
 * 뒤끝 초기화
 * 커스텀 로그인/회원가입
 * 닉네임 변경
 */
public class BackendServerManager : MonoBehaviour {
    private static BackendServerManager instance = null;

    public string loginId;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
        }
        instance = this;
        // 모든 씬에서 유지
        DontDestroyOnLoad(this.gameObject);
    }

    public static BackendServerManager Instance()
    {
        if (instance == null)
        {
            Debug.LogError("BackendManager 인스턴스가 존재하지 않습니다.");
            return null;
        }

        return instance;
    }

    void Start() {
        var bro = Backend.Initialize(true);

        if (bro.IsSuccess()) 
        {
            Debug.Log("초기화 성공 : " + bro);
        } 
        else 
        {
            Debug.LogError("초기화 실패 : " + bro);
        }
    }
    void Update()
    {
        //비동기함수 풀링
        Backend.AsyncPoll();
    }
    public bool CustomSignUp(string id, string pw)
    {
        BackendReturnObject bro = Backend.BMember.CustomSignUp(id, pw);

        if (bro.IsSuccess())
        {
            Debug.Log("커스텀 회원가입 성공: " + bro);
            return true;

        }
        else
        {
            Debug.LogError("커스텀 회원가입 실패: " + bro);
            return false;
        }
    }

    public bool CustomLogin(string id, string pw)
    {
        BackendReturnObject bro = Backend.BMember.CustomLogin(id, pw);

        if (bro.IsSuccess())
        {
            Debug.Log("커스텀 로그인 성공: " + bro);
            loginId = id;
            return true;
        }
        else
        {
            Debug.LogError("커스텀 로그인 실패: " + bro);
            return false;
        }
    }

    public bool UpdateNickname(string nickname)
    {
        BackendReturnObject bro = Backend.BMember.UpdateNickname(nickname);

        if (bro.IsSuccess())
        {
            Debug.Log("닉네임 변경 성공: " + bro);
            return true;
        }
        else
        {
            Debug.LogError("닉네임 변경 실패: " + bro);
            return false;
        }
    }
    
    //유저 정보 가져오기
    public string GetId()
    {
        return loginId;
    }
}