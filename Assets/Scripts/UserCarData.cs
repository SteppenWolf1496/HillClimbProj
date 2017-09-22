using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeStage.AntiCheat.ObscuredTypes;


public class UserCarData
    {
        public ObscuredString key;
        public string[] PassiveModifs = new string[4];
        public string[] ActiveModifs = new string[2];
    //public ObscuredInt 

        public string GetSave()
        {
            return Utility.Format("{0}%{1}%{2}%{3}%{4}%{5}%{6}", key, PassiveModifs[0], PassiveModifs[1], PassiveModifs[2], PassiveModifs[3], ActiveModifs[0], ActiveModifs[1]);
        }

        public void SetData(string _data)
        {
            string[] data = _data.Split('%');
            key = data[0];
            PassiveModifs[0] = data[1];
            PassiveModifs[1] = data[2];
            PassiveModifs[2] = data[3];
            PassiveModifs[3] = data[4];
            ActiveModifs[0] = data[5];
            ActiveModifs[1] = data[6];
    }

}

