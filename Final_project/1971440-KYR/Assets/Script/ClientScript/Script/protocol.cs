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
        // 0 ���ϴ� �����ڵ��̹Ƿ� ���ӿ��� ���XX
        BEGIN = 0,

        // C -> S ���� ���� ��û
        ENTER_GAME_ROOM_REQ = 1,

        // S -> C
        ENTER_GAME_ROOM_ACK = 2,

        // S -> C ��Ī ����, �� ���� �� �ε� ����
        START_LOADING = 3,

        // ���� ������ ���� ��û/����
        CONCURRENT_USERS = 4,

        // player ���� ��û
        ENTER_PLAYER_REQ = 5,

        ENTER_PLAYER_ACK = 6,

        // C -> S ��Ƽ ��Ī ��û
        ENTER_PARTY_REQ = 7,

        // S -> C ��Ƽ ��Ī ��û�� ���� ����
        ENTER_PARTY_ACK = 8,



        // ���� �÷��� ����-------------------------------------------------------------------

        // �÷��̾� �̵� ����
        PLAYER_MOVED_REQ = 11,

        PLAYER_MOVED_ACK = 12,

        // �÷��̾� �̵� ���� ����
        PLAYER_STAY_REQ = 13,

        PLAYER_STAY_ACK = 14,
        
        //�÷��̾� ����
        PLAYER_ATK_REQ = 15,

        PLAYER_ATK_ACK = 16,



        // ä�� ����--------------------------------------------------------------------

        // �⺻ �Ϲ� ä��
        CHAT_MSG_REQ = 21,

        CHAT_MSG_ACK = 22,

        // ��Ƽ ä��
        PARTY_MSG_REQ = 23,

        PARTY_MSG_ACK = 24,

        // �÷��̾� ���� ����
        PLAYER_CLOSE_REQ = 38,

        PLAYER_CLOSE_ACK = 39,

        END
    }
}
