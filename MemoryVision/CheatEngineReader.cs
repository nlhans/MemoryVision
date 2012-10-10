using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace MemoryVision
{
    public class CheatEngineWriter
    {
        public CheatEngineWriter(List<MemoryChannel> channels, IntPtr offset, string exe)
        {
            //

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
