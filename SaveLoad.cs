using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie_ksztalty
{
    //metody refleksji        [description("etykieta)]
    internal class SaveLoad
    {
        private Dictionary<string, string> dicField = new Dictionary<string, string>();
        private Dictionary<string, MemberInfo> dicMen = new Dictionary<string, MemberInfo>();

        private void PrepareAllFields(object obj)
        {
            Type t = obj.GetType();
            MemberInfo[] members = t.GetMembers(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
            dicField.Clear();
            dicMen.Clear();

            foreach (MemberInfo member in members) 
            {
                object[] attibuttes = member.GetCustomAttributes(true);

                if(attibuttes.Length != 0 && member.MemberType.ToString() == "Field")
                {
                    string key = "";
                    foreach(object attibutte in attibuttes)
                    {
                        DescriptionAttribute da = attibutte as DescriptionAttribute;
                        if(da != null)
                        {
                            key = da.Description;
                        }
                        object o = member.ReflectedType.GetField(member.Name, BindingFlags.NonPublic| BindingFlags.Instance | BindingFlags.Public).GetValue(obj);

                        if (o != null)
                            dicField.Add(key, o.ToString());
                        else
                            dicField.Add(key, "");

                        dicMen.Add(key, member);
                    }
                }

            }
        }

        public void Save(StreamWriter filestream, object t)
        {
            PrepareAllFields(t);
            foreach(var item in dicField.Keys)
            {
                filestream.WriteLine(item + '#' + dicField[item]);
            }
        }

        public void Load(StreamReader fileRead, object t)
        {
            PrepareAllFields(t);
            string line = "";
            MemberInfo memberB;
            while(!fileRead.EndOfStream && !line.Contains("End")) 
            {
                line = fileRead.ReadLine();
                line = line.Replace("#", "#");
                string[] strTab = line.Split('#');

                if (dicField.ContainsKey(strTab[0]))
                {
                    memberB = dicMen[strTab[0]];
                    string ww = memberB.ReflectedType.GetField(memberB.Name).FieldType.Name;

                    switch(ww)
                    {
                        case "string":
                        case "String":
                            memberB.ReflectedType.GetField(memberB.Name)?.SetValue(this, strTab[1]);
                            break;
                        case "Boolean":
                            memberB.ReflectedType.GetField(memberB.Name)?.SetValue(this, Convert.ToBoolean(strTab[1]));
                            break;
                        case "Single":
                            memberB.ReflectedType.GetField(memberB.Name)?.SetValue(this, Convert.ToSingle(strTab[1]));
                            break;
                        case "Int32":
                            memberB.ReflectedType.GetField(memberB.Name)?.SetValue(this, Convert.ToInt32(strTab[1]));
                            break;
                    }
                }
            }
        }

    }
}

//dopisz do klasy jaki jest typ obiektu
//przerob to tak zeby dzialalo dla tej twojej listy ksztaltow
//na za dwa tygodnie od 16.05.2024