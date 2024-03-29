﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotmgClient.Networking.Data
{
    public class StatData
    {
        public const int MAX_HP_STAT = 0;
        public const int HP_STAT = 1;
        public const int SIZE_STAT = 2;
        public const int MAX_MP_STAT = 3;
        public const int MP_STAT = 4;
        public const int NEXT_LEVEL_EXP_STAT = 5;
        public const int EXP_STAT = 6;
        public const int LEVEL_STAT = 7;
        public const int INVENTORY_0_STAT = 8;
        public const int INVENTORY_1_STAT = 9;
        public const int INVENTORY_2_STAT = 10;
        public const int INVENTORY_3_STAT = 11;
        public const int INVENTORY_4_STAT = 12;
        public const int INVENTORY_5_STAT = 13;
        public const int INVENTORY_6_STAT = 14;
        public const int INVENTORY_7_STAT = 15;
        public const int INVENTORY_8_STAT = 16;
        public const int INVENTORY_9_STAT = 17;
        public const int INVENTORY_10_STAT = 18;
        public const int INVENTORY_11_STAT = 19;
        public const int ATTACK_STAT = 20;
        public const int DEFENSE_STAT = 21;
        public const int SPEED_STAT = 22;
        public const int VITALITY_STAT = 23;
        public const int WISDOM_STAT = 24;
        public const int DEXTERITY_STAT = 25;
        public const int CONDITION_STAT = 26;
        public const int NUM_STARS_STAT = 27;
        public const int NAME_STAT = 28;
        public const int TEX1_STAT = 29;
        public const int TEX2_STAT = 30;
        public const int MERCHANDISE_TYPE_STAT = 31;
        public const int MERCHANDISE_PRICE_STAT = 32;
        public const int CREDITS_STAT = 33;
        public const int ACTIVE_STAT = 34;
        public const int ACCOUNT_ID_STAT = 35;
        public const int FAME_STAT = 36;
        public const int MERCHANDISE_CURRENCY_STAT = 37;
        public const int CONNECT_STAT = 38;
        public const int MERCHANDISE_COUNT_STAT = 39;
        public const int MERCHANDISE_MINS_LEFT_STAT = 40;
        public const int MERCHANDISE_DISCOUNT_STAT = 41;
        public const int MERCHANDISE_RANK_REQ_STAT = 42;
        public const int MAX_HP_BOOST_STAT = 43;
        public const int MAX_MP_BOOST_STAT = 44;
        public const int ATTACK_BOOST_STAT = 45;
        public const int DEFENSE_BOOST_STAT = 46;
        public const int SPEED_BOOST_STAT = 47;
        public const int VITALITY_BOOST_STAT = 48;
        public const int WISDOM_BOOST_STAT = 49;
        public const int DEXTERITY_BOOST_STAT = 50;
        public const int CHAR_FAME_STAT = 51;
        public const int NEXT_CLASS_QUEST_FAME_STAT = 52;
        public const int LEGENDARY_RANK_STAT = 53;
        public const int SINK_LEVEL_STAT = 54;
        public const int ALT_TEXTURE_STAT = 55;
        public const int GUILD_NAME_STAT = 56;
        public const int GUILD_RANK_STAT = 57;
        public const int BREATH_STAT = 58;
        public const int HEALTH_POTION_STACK_STAT = 59;
        public const int MAGIC_POTION_STACK_STAT = 60;
        public const int BACKPACK_0_STAT = 61;
        public const int BACKPACK_1_STAT = 62;
        public const int BACKPACK_2_STAT = 63;
        public const int BACKPACK_3_STAT = 64;
        public const int BACKPACK_4_STAT = 65;
        public const int BACKPACK_5_STAT = 66;
        public const int BACKPACK_6_STAT = 67;
        public const int BACKPACK_7_STAT = 68;
        public const int HASBACKPACK_STAT = 69;
        public const int TEXTURE_STAT = 70;
        public const int ITEMDATA_0_STAT = 71;
        public const int ITEMDATA_1_STAT = 72;
        public const int ITEMDATA_2_STAT = 73;
        public const int ITEMDATA_3_STAT = 74;
        public const int ITEMDATA_4_STAT = 75;
        public const int ITEMDATA_5_STAT = 76;
        public const int ITEMDATA_6_STAT = 77;
        public const int ITEMDATA_7_STAT = 78;
        public const int ITEMDATA_8_STAT = 79;
        public const int ITEMDATA_9_STAT = 80;
        public const int ITEMDATA_10_STAT = 81;
        public const int ITEMDATA_11_STAT = 82;
        public const int ITEMDATA_12_STAT = 83;
        public const int ITEMDATA_13_STAT = 84;
        public const int ITEMDATA_14_STAT = 85;
        public const int ITEMDATA_15_STAT = 86;
        public const int ITEMDATA_16_STAT = 87;
        public const int ITEMDATA_17_STAT = 88;
        public const int ITEMDATA_18_STAT = 89;
        public const int ITEMDATA_19_STAT = 90;

        public uint statType;
        public int statValue;
        public string statValueStr;

        public static string StatToName(int stat)
        {
            switch (stat)
            {
                case MAX_HP_STAT:
                    return "";
                case HP_STAT:
                    return "";
                case SIZE_STAT:
                    return "";
                case MAX_MP_STAT:
                    return "";
                case MP_STAT:
                    return "";
                case EXP_STAT:
                    return "";
                case LEVEL_STAT:
                    return "";
                case ATTACK_STAT:
                    return "";
                case DEFENSE_STAT:
                case SPEED_STAT:
                    return "";
                case VITALITY_STAT:
                    return "";
                case WISDOM_BOOST_STAT:
                    return "";
                case DEXTERITY_STAT:
                    return "";
            }
            return "Unknown";
        }

        public bool IsStringStat()
        {
            switch (statType)
            {
                case NAME_STAT:
                case GUILD_NAME_STAT:
                    return true;
                default:
                    return false;
            }
        }

        public void Read(NReader nR)
        {
            statType = nR.ReadByte();
            if (!IsStringStat())
                nR.ReadInt32();
            else
                nR.ReadUTF();
        }

        public void Write(NWriter nW)
        {
            nW.Write(statType);
            if (!IsStringStat())
                nW.Write(statValue);
            else
                nW.WriteUTF(statValueStr);
        }
    }
}
