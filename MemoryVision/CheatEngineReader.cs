using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using Triton.Memory;

namespace MemoryVision
{
    public enum MemoryChannelType
    {
        BINARY,
        INT8,
        INT16,
        INT32,
        UINT8,
        UINT16,
        UINT32,
        FLOAT,
        DOUBLE,
        STRING,
        CHAR
    } ;

    public static partial class Extensions
    {
        public static string ReadAsString(this MemoryReader reader, MemoryChannel ch)
        {
            switch(ch.Type)
            {
                case MemoryChannelType.FLOAT:
                    return reader.ReadFloat(ch.Address).ToString("0.0000");
                    break;
                case MemoryChannelType.INT32:
                    return reader.ReadInt32(ch.Address).ToString();
                    break;
                default:
                    return "??";
            }
        }
    }

    public class MemoryChannel
    {
        public int ID;
        public int UI_ID;
        public string Name;
        public MemoryChannelType Type;
        public string Parameters;
        public IntPtr Address;

        public string TypeToString()
        {
            switch(Type)
            {
                case MemoryChannelType.FLOAT:
                    return "float";
                    break;
                case MemoryChannelType.INT32:
                    return "Int32";
                    break;
                default:
                    return "??";
                    break;
            }
        }
    }

    public class CheatEngineReader
    {
        public List<MemoryChannel> Channels = new List<MemoryChannel>();
        public string ProcessExe = "";
        public CheatEngineReader()
        {
            
        }

        public void Read(string file)
        {
            int i = 0;

            MemoryChannel ch = new MemoryChannel();
            ch.ID = -1;

            if(File.Exists(file))
            {
                // For table version 12
                using (XmlReader xml = XmlReader.Create(file))
                {
                    while (xml.Read())
                    {
                        if (xml.IsStartElement())
                        {
                            switch (xml.Name)
                            {
                                case "CheatTable":
                                    // YEP for version 12.
                                    break;
                                case "CheatEntries":
                                    // Yep.
                                    break;

                                case "CheatEntry":
                                    ch = new MemoryChannel();
                                    break;
                                case "ID":
                                    xml.Read();
                                    //ch.ID = Convert.ToInt32(xml.Value);
                                    ch.ID = xml.ReadContentAsInt();
                                    break;
                                case "Description":
                                    xml.Read();
                                    ch.Name = xml.ReadContentAsString();
                                    ch.Name = ch.Name.Substring(1, ch.Name.Length - 2);
                                    break;

                                case "VariableType":
                                    xml.Read();
                                    switch (xml.Value)
                                    {
                                        case "4 Bytes":
                                            ch.Type = MemoryChannelType.INT32;
                                            break;
                                        case "Float":
                                            ch.Type = MemoryChannelType.FLOAT;
                                            break;
                                    }
                                    break;

                                case "Address":
                                    xml.Read();
                                    string s = xml.Value;
                                    ProcessExe = s.Substring(0, s.IndexOf("+"));
                                    s = s.Substring(s.IndexOf("+") + 1);
                                    int.TryParse(s, NumberStyles.HexNumber, new CultureInfo("EN-US"), out i);

                                    ch.Address = new IntPtr(i);
                                    break;

                            }
                        }
                        else
                        {
                            if (xml.Name == "CheatEntry")
                                // Store it.
                                Channels.Add(ch);
                        }
                    }
                }
            }
        }

        public MemoryChannel GetByUIID(int i)
        {
            return Channels.Find(delegate(MemoryChannel mc) { return mc.UI_ID == i; });
        }
    }
}
