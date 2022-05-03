using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OmokManager : MonoBehaviour
{
    LogicOmok lg = new LogicOmok(19, 19); // 19 x 19 
    GameObject b_stone, w_stone;
    GameObject[,] ObjArr; // 소환한 stone clone들 저장
    Vector2 origin;

    public GameObject rb; // 결과창 GameObject
    public Text res_text; // 결과창 text

    GameObject TurnStone;
    public Sprite[] StoneColor;

    bool GameEnds;

    private void Awake()
    {
        GameEnds = false; // Game 시작    
    }

    private void Start()
    {
        w_stone = Resources.Load("stone_white_p") as GameObject;
        b_stone = Resources.Load("stone_black_p") as GameObject;

        TurnStone = GameObject.Find("TurnStone");

        ObjArr = new GameObject[19, 19];
    }
    
    private void Update()
    {
        if(!GameEnds)
        {
            // 순서에 따른 Turn 돌색 변
            lg.CurrentTurn(TurnStone, StoneColor);
            if(Input.GetMouseButtonDown(0))
            {
                //CurrentTurn();
                if (Put()) GameEnds = true; 
                Render(); 
            }
        }
        else
        {
            ResultBoard();
        }
    }

    // 오목완성 : return true 
    bool Put()
    {
        origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // 정수 좌표로 변환
        lg.checkpos(ref origin);

        // 보드좌표 -> 배열좌표
        Vector2 arrpos = pos_to_arr(origin);

        // 보드판 벗어난곳 클릭시 아무변화 없이 false return 
        if ((int)arrpos.x < 0 || (int)arrpos.x > 19 || (int)arrpos.y < 0 || (int)arrpos.y > 19)
            return false;

        // true 리턴 시 오목 완성.
        return lg.setData((int)arrpos.x, (int)arrpos.y);

    }

    Vector2 pos_to_arr(Vector2 origin)
    {
        int x, y;
        x = (int)origin.x + 9;
        y = (int)origin.y + 9;

        return new Vector2(x, y);
    }

    Vector2 arr_to_pos(int i, int j)
    {
        int x, y;
        x = i - 9;
        y = j - 9;

        return new Vector2(x, y);
    }

    
    void Render()
    {
        
        for(int i = 0; i < 19; i++)
        {
            for(int j = 0; j < 19; j++)
            {
                Destroy(ObjArr[i, j]);
            }
        }
        
        for(int i = 0; i < 19; i++)
        {
            for(int j = 0; j < 19; j++)
            {
                if (lg.getData(i, j) == 1) // White
                {
                    ObjArr[i,j] = Instantiate(w_stone, arr_to_pos(i,j), Quaternion.identity);
                }
                else if (lg.getData(i, j) == 2) // Black
                {
                    ObjArr[i,j] = Instantiate(b_stone, arr_to_pos(i, j), Quaternion.identity);
                }
            }
        }
    }

    void ResultBoard()
    {
        if(lg.getTurn() == 2)
        {
            res_text.GetComponent<Text>().text = "WHITE WIN";
        }
        else
        {
            res_text.GetComponent<Text>().text = "BLACK WIN";
        }

        rb.SetActive(true);
    }


}
