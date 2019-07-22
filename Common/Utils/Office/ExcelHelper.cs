using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utils.Office
{
    public static class ExcelHelper
    {
        /// <summary>
        /// 获取列值字母
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetColumnLetter(int value)
        {
            string columnName = string.Empty;
            switch (value)
            {
                case 1:
                    columnName = "A";
                    break;
                case 2:
                    columnName = "B";
                    break;
                case 3:
                    columnName = "C";
                    break;
                case 4:
                    columnName = "D";
                    break;
                case 5:
                    columnName = "E";
                    break;
                case 6:
                    columnName = "F";
                    break;
                case 7:
                    columnName = "G";
                    break;
                case 8:
                    columnName = "H";
                    break;
                case 9:
                    columnName = "I";
                    break;
                case 10:
                    columnName = "J";
                    break;
                case 11:
                    columnName = "K";
                    break;
                case 12:
                    columnName = "L";
                    break;
                case 13:
                    columnName = "M";
                    break;
                case 14:
                    columnName = "N";
                    break;
                case 15:
                    columnName = "O";
                    break;
                case 16:
                    columnName = "P";
                    break;
                case 17:
                    columnName = "Q";
                    break;
                case 18:
                    columnName = "R";
                    break;
                case 19:
                    columnName = "S";
                    break;
                case 20:
                    columnName = "T";
                    break;
                case 21:
                    columnName = "U";
                    break;
                case 22:
                    columnName = "V";
                    break;
                case 23:
                    columnName = "W";
                    break;
                case 24:
                    columnName = "X";
                    break;
                case 25:
                    columnName = "Y";
                    break;
                case 26:
                    columnName = "Z";
                    break;
                case 27:
                    columnName = "AA";
                    break;
                case 28:
                    columnName = "AB";
                    break;
                case 29:
                    columnName = "AC";
                    break;
                case 30:
                    columnName = "AD";
                    break;
                case 31:
                    columnName = "AE";
                    break;
                case 32:
                    columnName = "AF";
                    break;
                case 33:
                    columnName = "AG";
                    break;
                case 34:
                    columnName = "AH";
                    break;
                case 35:
                    columnName = "AI";
                    break;
                case 36:
                    columnName = "AJ";
                    break;
                case 37:
                    columnName = "AK";
                    break;
                case 38:
                    columnName = "AL";
                    break;
                case 39:
                    columnName = "AM";
                    break;
                case 40:
                    columnName = "AN";
                    break;
                case 41:
                    columnName = "AO";
                    break;
                case 42:
                    columnName = "AP";
                    break;
                case 43:
                    columnName = "AQ";
                    break;
                case 44:
                    columnName = "AR";
                    break;
                case 45:
                    columnName = "AS";
                    break;
                case 46:
                    columnName = "AT";
                    break;
                case 47:
                    columnName = "AU";
                    break;
                case 48:
                    columnName = "AV";
                    break;
                case 49:
                    columnName = "AW";
                    break;
                case 50:
                    columnName = "AX";
                    break;
                case 51:
                    columnName = "AY";
                    break;
                case 52:
                    columnName = "AZ";
                    break;
                case 53:
                    columnName = "BA";
                    break;
                case 54:
                    columnName = "BB";
                    break;
                case 55:
                    columnName = "BC";
                    break;
                case 56:
                    columnName = "BD";
                    break;
                case 57:
                    columnName = "BE";
                    break;
                case 58:
                    columnName = "BF";
                    break;
                case 59:
                    columnName = "BG";
                    break;
                case 60:
                    columnName = "BH";
                    break;
                case 61:
                    columnName = "BI";
                    break;
                case 62:
                    columnName = "BJ";
                    break;
                case 63:
                    columnName = "BK";
                    break;
                case 64:
                    columnName = "BL";
                    break;
                case 65:
                    columnName = "BM";
                    break;
                case 66:
                    columnName = "BN";
                    break;
                case 67:
                    columnName = "BO";
                    break;
                case 68:
                    columnName = "BP";
                    break;
                case 69:
                    columnName = "BQ";
                    break;
                case 70:
                    columnName = "BR";
                    break;
                case 71:
                    columnName = "BS";
                    break;
                case 72:
                    columnName = "BT";
                    break;
                case 73:
                    columnName = "BU";
                    break;
                case 74:
                    columnName = "BV";
                    break;
                case 75:
                    columnName = "BW";
                    break;
                case 76:
                    columnName = "BX";
                    break;
                case 77:
                    columnName = "BY";
                    break;
                case 78:
                    columnName = "BZ";
                    break;
                case 79:
                    columnName = "CA";
                    break;
                case 80:
                    columnName = "CB";
                    break;
                case 81:
                    columnName = "CC";
                    break;
                case 82:
                    columnName = "CD";
                    break;
                case 83:
                    columnName = "CE";
                    break;
                case 84:
                    columnName = "CF";
                    break;
                case 85:
                    columnName = "CG";
                    break;
                case 86:
                    columnName = "CH";
                    break;
                case 87:
                    columnName = "CI";
                    break;
                case 88:
                    columnName = "CJ";
                    break;
                case 89:
                    columnName = "CK";
                    break;
                case 90:
                    columnName = "CL";
                    break;
                case 91:
                    columnName = "CM";
                    break;
                case 92:
                    columnName = "CN";
                    break;
                case 93:
                    columnName = "CO";
                    break;
                case 94:
                    columnName = "CP";
                    break;
                case 95:
                    columnName = "CQ";
                    break;
                case 96:
                    columnName = "CR";
                    break;
                case 97:
                    columnName = "CS";
                    break;
                case 98:
                    columnName = "CT";
                    break;
                case 99:
                    columnName = "CU";
                    break;
                case 100:
                    columnName = "CV";
                    break;
                case 101:
                    columnName = "CW";
                    break;
                case 102:
                    columnName = "CX";
                    break;
                case 103:
                    columnName = "CY";
                    break;
                case 104:
                    columnName = "CZ";
                    break;
                case 105:
                    columnName = "DA";
                    break;
                case 106:
                    columnName = "DB";
                    break;
                case 107:
                    columnName = "DC";
                    break;
                case 108:
                    columnName = "DD";
                    break;
                case 109:
                    columnName = "DE";
                    break;
                case 110:
                    columnName = "DF";
                    break;
                case 111:
                    columnName = "DG";
                    break;
                case 112:
                    columnName = "DH";
                    break;
                case 113:
                    columnName = "DI";
                    break;
                case 114:
                    columnName = "DJ";
                    break;
                case 115:
                    columnName = "DK";
                    break;
                case 116:
                    columnName = "DL";
                    break;
                case 117:
                    columnName = "DM";
                    break;
                case 118:
                    columnName = "DN";
                    break;
                case 119:
                    columnName = "DO";
                    break;
                case 120:
                    columnName = "DP";
                    break;
                case 121:
                    columnName = "DQ";
                    break;
                case 122:
                    columnName = "DR";
                    break;
                case 123:
                    columnName = "DS";
                    break;
                case 124:
                    columnName = "DT";
                    break;
                case 125:
                    columnName = "DU";
                    break;
                case 126:
                    columnName = "DV";
                    break;
                case 127:
                    columnName = "DW";
                    break;
                case 128:
                    columnName = "DX";
                    break;
                case 129:
                    columnName = "DY";
                    break;
                case 130:
                    columnName = "DZ";
                    break;
            }
            return columnName;
        }

        /// <summary>
        /// 获取字母代表列值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int GetColumnValue(string value)
        {
            int columnValue = 1;
            switch (value)
            {
                case "A":
                    columnValue = 1;
                    break;
                case "B":
                    columnValue = 2;
                    break;
                case "C":
                    columnValue = 3;
                    break;
                case "D":
                    columnValue = 4;
                    break;
                case "E":
                    columnValue = 5;
                    break;
                case "F":
                    columnValue = 6;
                    break;
                case "G":
                    columnValue = 7;
                    break;
                case "H":
                    columnValue = 8;
                    break;
                case "I":
                    columnValue = 9;
                    break;
                case "J":
                    columnValue = 10;
                    break;
                case "K":
                    columnValue = 11;
                    break;
                case "L":
                    columnValue = 12;
                    break;
                case "M":
                    columnValue = 13;
                    break;
                case "N":
                    columnValue = 14;
                    break;
                case "O":
                    columnValue = 15;
                    break;
                case "P":
                    columnValue = 16;
                    break;
                case "Q":
                    columnValue = 17;
                    break;
                case "R":
                    columnValue = 18;
                    break;
                case "S":
                    columnValue = 19;
                    break;
                case "T":
                    columnValue = 20;
                    break;
                case "U":
                    columnValue = 21;
                    break;
                case "V":
                    columnValue = 22;
                    break;
                case "W":
                    columnValue = 23;
                    break;
                case "X":
                    columnValue = 24;
                    break;
                case "Y":
                    columnValue = 25;
                    break;
                case "Z":
                    columnValue = 26;
                    break;
                case "AA":
                    columnValue = 27;
                    break;
                case "AB":
                    columnValue = 28;
                    break;
                case "AC":
                    columnValue = 29;
                    break;
                case "AD":
                    columnValue = 30;
                    break;
                case "AE":
                    columnValue = 31;
                    break;
                case "AF":
                    columnValue = 32;
                    break;
                case "AG":
                    columnValue = 33;
                    break;
                case "AH":
                    columnValue = 34;
                    break;
                case "AI":
                    columnValue = 35;
                    break;
                case "AJ":
                    columnValue = 36;
                    break;
                case "AK":
                    columnValue = 37;
                    break;
                case "AL":
                    columnValue = 38;
                    break;
                case "AM":
                    columnValue = 39;
                    break;
                case "AN":
                    columnValue = 40;
                    break;
                case "AO":
                    columnValue = 41;
                    break;
                case "AP":
                    columnValue = 42;
                    break;
                case "AQ":
                    columnValue = 43;
                    break;
                case "AR":
                    columnValue = 44;
                    break;
                case "AS":
                    columnValue = 45;
                    break;
                case "AT":
                    columnValue = 46;
                    break;
                case "AU":
                    columnValue = 47;
                    break;
                case "AV":
                    columnValue = 48;
                    break;
                case "AW":
                    columnValue = 49;
                    break;
                case "AX":
                    columnValue = 50;
                    break;
                case "AY":
                    columnValue = 51;
                    break;
                case "AZ":
                    columnValue = 52;
                    break;
                case "BA":
                    columnValue = 53;
                    break;
                case "BB":
                    columnValue = 54;
                    break;
                case "BC":
                    columnValue = 55;
                    break;
                case "BD":
                    columnValue = 56;
                    break;
                case "BE":
                    columnValue = 57;
                    break;
                case "BF":
                    columnValue = 58;
                    break;
                case "BG":
                    columnValue = 59;
                    break;
                case "BH":
                    columnValue = 60;
                    break;
                case "BI":
                    columnValue = 61;
                    break;
                case "BJ":
                    columnValue = 62;
                    break;
                case "BK":
                    columnValue = 63;
                    break;
                case "BL":
                    columnValue = 64;
                    break;
                case "BM":
                    columnValue = 65;
                    break;
                case "BN":
                    columnValue = 66;
                    break;
                case "BO":
                    columnValue = 67;
                    break;
                case "BP":
                    columnValue = 68;
                    break;
                case "BQ":
                    columnValue = 69;
                    break;
                case "BR":
                    columnValue = 70;
                    break;
                case "BS":
                    columnValue = 71;
                    break;
                case "BT":
                    columnValue = 72;
                    break;
                case "BU":
                    columnValue = 73;
                    break;
                case "BV":
                    columnValue = 74;
                    break;
                case "BW":
                    columnValue = 75;
                    break;
                case "BX":
                    columnValue = 76;
                    break;
                case "BY":
                    columnValue = 77;
                    break;
                case "BZ":
                    columnValue = 78;
                    break;
                case "CA":
                    columnValue = 79;
                    break;
                case "CB":
                    columnValue = 80;
                    break;
                case "CC":
                    columnValue = 81;
                    break;
                case "CD":
                    columnValue = 82;
                    break;
                case "CE":
                    columnValue = 83;
                    break;
                case "CF":
                    columnValue = 84;
                    break;
                case "CG":
                    columnValue = 85;
                    break;
                case "CH":
                    columnValue = 86;
                    break;
                case "CI":
                    columnValue = 87;
                    break;
                case "CJ":
                    columnValue = 88;
                    break;
                case "CK":
                    columnValue = 89;
                    break;
                case "CL":
                    columnValue = 90;
                    break;
                case "CM":
                    columnValue = 91;
                    break;
                case "CN":
                    columnValue = 92;
                    break;
                case "CO":
                    columnValue = 93;
                    break;
                case "CP":
                    columnValue = 94;
                    break;
                case "CQ":
                    columnValue = 95;
                    break;
                case "CR":
                    columnValue = 96;
                    break;
                case "CS":
                    columnValue = 97;
                    break;
                case "CT":
                    columnValue = 98;
                    break;
                case "CU":
                    columnValue = 99;
                    break;
                case "CV":
                    columnValue = 100;
                    break;
                case "CW":
                    columnValue = 101;
                    break;
                case "CX":
                    columnValue = 102;
                    break;
                case "CY":
                    columnValue = 103;
                    break;
                case "CZ":
                    columnValue = 104;
                    break;
                case "DA":
                    columnValue = 105;
                    break;
                case "DB":
                    columnValue = 106;
                    break;
                case "DC":
                    columnValue = 107;
                    break;
                case "DD":
                    columnValue = 108;
                    break;
                case "DE":
                    columnValue = 119;
                    break;
                case "DF":
                    columnValue = 110;
                    break;
                case "DG":
                    columnValue = 111;
                    break;
                case "DH":
                    columnValue = 112;
                    break;
                case "DI":
                    columnValue = 113;
                    break;
                case "DJ":
                    columnValue = 114;
                    break;
                case "DK":
                    columnValue = 115;
                    break;
                case "DL":
                    columnValue = 116;
                    break;
                case "DM":
                    columnValue = 117;
                    break;
                case "DN":
                    columnValue = 118;
                    break;
                case "DO":
                    columnValue = 119;
                    break;
                case "DP":
                    columnValue = 120;
                    break;
                case "DQ":
                    columnValue = 121;
                    break;
                case "DR":
                    columnValue = 122;
                    break;
                case "DS":
                    columnValue = 123;
                    break;
                case "DT":
                    columnValue = 124;
                    break;
                case "DU":
                    columnValue = 125;
                    break;
                case "DV":
                    columnValue = 126;
                    break;
                case "DW":
                    columnValue = 127;
                    break;
                case "DX":
                    columnValue = 128;
                    break;
                case "DY":
                    columnValue = 129;
                    break;
                case "DZ":
                    columnValue = 130;
                    break;
            }
            return columnValue;
        }
    }
}
