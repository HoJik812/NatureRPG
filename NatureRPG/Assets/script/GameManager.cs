using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region
    /*
    public static GameManager instance = null;//static : 데이터 영역 => 프로그램 꺼질때까지 유지, 이 클래스 자체의 맴버
    public int value;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);//load가 되도 파괴 안댐. 신이 이동되도 파괴 없이 유지
        }
        else
        {
            Destroy(this.gameObject);//이미 null이 아니면 나중에 생기는것들은 그냥 삭제 => 나만 남음 => 그게 싱글톤
        }
    }
    */
    #endregion
    public int score;

}
