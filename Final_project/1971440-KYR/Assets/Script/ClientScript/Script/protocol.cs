using System;

namespace WHServer
{
    public struct Position
    {
        public Position (float x, float z)
        {
            X = x;
            Z = z;
        }

        public float X { get; }
        public float Z { get; }
    }
    public enum PROTOCOL : short
    {
        // 0 이하는 종료코드이므로 게임에서 사용XX
        BEGIN = 0,

        // C -> S 게임 입장 요청
        ENTER_GAME_ROOM_REQ = 1,

        // S -> C
        ENTER_GAME_ROOM_ACK = 2,

        // S -> C 매칭 성공, 룸 입장 후 로딩 시작
        START_LOADING = 3,

        // 동시 접속자 정보 요청/응답
        CONCURRENT_USERS = 4,

        // player 입장 요청
        ENTER_PLAYER_REQ = 5,

        ENTER_PLAYER_ACK = 6,

        // C -> S 파티 매칭 요청
        ENTER_PARTY_REQ = 7,

        // S -> C 파티 매칭 요청에 대한 응답
        ENTER_PARTY_ACK = 8,



        // 게임 플레이 로직-------------------------------------------------------------------

        // 플레이어 이동 시작
        PLAYER_MOVED_REQ = 11,

        PLAYER_MOVED_ACK = 12,

        // 플레이어 이동 하지 않음
        PLAYER_STAY_REQ = 13,

        PLAYER_STAY_ACK = 14,
        
        //플레이어 공격
        PLAYER_ATK_REQ = 15,

        PLAYER_ATK_ACK = 16,



        // 채팅 로직--------------------------------------------------------------------

        // 기본 일반 채팅
        CHAT_MSG_REQ = 21,

        CHAT_MSG_ACK = 22,

        // 파티 채팅
        PARTY_MSG_REQ = 23,

        PARTY_MSG_ACK = 24,

        // 플레이어 접속 종료
        PLAYER_CLOSE_REQ = 38,

        PLAYER_CLOSE_ACK = 39,

        END
    }
}
