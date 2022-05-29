using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicOthello : Logic2D
{
    // base: 부모의 생성자 호출 후 내 생성자 본문 실행
    public LogicOthello(int r, int c) : base(r, c)
    {
        setValue(1, 3, 4);
        setValue(1, 4, 3);
        setValue(2, 3, 3);
        setValue(2, 4, 4);
    }

    // false : 유의미 데이터 없음.
    // true : 유효 데이터 존재.
    public bool setData(int r, int c)
    {
        // 보드 범위 벗어남 
        if (r < 0 || r >= 8 || c < 0 || c >= 8) return false;
        // 돌 놓은곳에 돌이 이미 있음.
        if (!isEmpty(r, c)) return false;        
        if (!analyze(r, c)) return false;

        setValue(mTurn, r, c);
        nextTurn();

        return true;

    }

    // return true면 유효
    public bool ChecksetData(int r, int c)
    {
        if (!isEmpty(r, c)) return false;
        if (!analyze(r, c))
        {
            return false;
        }


        return true;
    }

    // 이전으로 데이터 복원
    public void RecoverData(int Length, int Turn, int[,] Dat)
    {
        mLength = Length;
        mTurn = Turn;
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                setValue(Dat[i, j], i, j);
            }
        }
    }

    // return true면 유효
    public override bool analyze(int r, int c)
    {
        int checkvalue = 3 - mTurn; // 오델로는 다른 값 체크
        resetLength();

        for (Direction dir = Direction.U; dir <= Direction.UL; dir++)
        {
            int beforeLength = mLength;
            if (analyzeDirection(checkvalue, (int)dir, r, c))
            {
                mLength = beforeLength;
            }
        }

        // 유효 데이터 존재
        if (mLength > 0)
        {
            for (int i = 0; i < mLength; i++)
            {
                setValue(mTurn, mPoints[i].r, mPoints[i].c);
            }
            return true;
        }
        return false;
    }

    public int WhoWins()
    {
        int WhiteWins = 0;
        int BlackWins = 0;

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (getData(i, j) == 1) WhiteWins++;
                else if (getData(i, j) == 2) BlackWins++;
            }
        }

        // 0 : Draw, 1: White Wins, 2: Black Wins
        if (WhiteWins > BlackWins) return 1;
        else if (WhiteWins < BlackWins) return 2;
        else return 0;
    }
}