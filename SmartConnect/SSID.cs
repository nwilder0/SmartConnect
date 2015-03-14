using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Security.Cryptography;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace SmartConnect
{
    
    public class SSID
    {
        String name;
        public String Name
        { 
            get { return name; }
            set { name = value; }
        }

        String role;
        public String Role
        { 
            get { return role; }
            set { role = value; }
        }

        String type;
        public String Type
        { 
            get { return type; }
            set { type = value; }
        }

        Boolean broadcast;
        public Boolean Broadcast
        {
            get { return broadcast; }
            set { broadcast = value; }
        }
        
        String authentication;
        public String Authentication
        { 
            get { return authentication; }
            set { authentication = value; }
        }
        
        String encryption;
        public String Encryption
        { 
            get { return encryption; }
            set { encryption = value; }
        }
        
        Boolean useOneX;
        public Boolean UseOneX
        { 
            get { return useOneX; }
            set { useOneX = value; }
        }
        
        String sharedKey;
        public String SharedKey
        { 
            get { return sharedKey; }
            set { sharedKey = value; }
        }

        String dot1XTemplateFilename;
        public String Dot1XTemplateFilename
        { 
            get { return dot1XTemplateFilename; }
            set { dot1XTemplateFilename = value; }
        }

        Boolean defaultSSID;
        public Boolean DefaultSSID
        {
            get { return defaultSSID; }
            set { defaultSSID = value; }
        }
        
        String profileName;
        [JsonIgnore]
        public String ProfileName
        {
            get { return profileName; }
            set { profileName = value; }
        }
        
        XDocument xdocProfile = null;
        [JsonIgnore]
        public String Profile
        { get {
            if (xdocProfile != null)
            {
                String tmpProfile = xdocProfile.ToString();
                tmpProfile = "<?xml version=\"1.0\"?>" + System.Environment.NewLine + tmpProfile.Replace(">True<", ">true<").Replace(">False<", ">false<");
                return tmpProfile;
            }
            else return "";
        } }

        public SSID(String name, String profileName, String role, String type, Boolean broadcast, String authentication, String encryption, 
            Boolean useOneX, String sharedKey, String dot1XTemplateFilename, XDocument xdocProfile)
        {
            this.name = name;
            this.profileName = profileName;
            this.role = role;
            this.type = type;
            this.broadcast = broadcast;
            this.authentication = authentication;
            this.encryption = encryption;
            this.useOneX = useOneX;
            this.sharedKey = sharedKey;
            this.dot1XTemplateFilename = dot1XTemplateFilename;

            try
            {
                if (xdocProfile == null)
                {
                    String strTempXML = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + dot1XTemplateFilename);
                    xdocProfile = XDocument.Parse(strTempXML);
                    XNamespace xNS = xdocProfile.Root.GetDefaultNamespace();
                    xdocProfile.Element(xNS + "WLANProfile").Element(xNS + "name").Value = name;
                    // hex string with separator or not?
                    xdocProfile.Element(xNS + "WLANProfile").Element(xNS + "SSIDConfig").Element(xNS + "SSID").Element(xNS + "name").Value = name;
                    xdocProfile.Element(xNS + "WLANProfile").Element(xNS + "SSIDConfig").Element(xNS + "SSID").Element(xNS + "hex").Value = SCUtility.String2HexStr(name);
                    XElement xSecurity = xdocProfile.Element(xNS + "WLANProfile").Element(xNS + "MSM").Element(xNS + "security");
                    xSecurity.Element(xNS + "authEncryption").Element(xNS + "authentication").Value = authentication;
                    xSecurity.Element(xNS + "authEncryption").Element(xNS + "encryption").Value = encryption;
                    xSecurity.Element(xNS + "authEncryption").Element(xNS + "useOneX").Value = useOneX.ToString();
                    if (!useOneX)
                    {
                        XElement xPSK = new XElement("sharedKey");
                        xPSK.Add("keyType");
                        xPSK.Add("protected");
                        xPSK.Add("keyMaterial");
                        xPSK.Element("keyType").Value = "passPhrase";
                        xPSK.Element("protected").Value = "true";
                        xPSK.Element("keyMaterial").Value = SCUtility.String2HexStr(SCUtility.Bytes2String(ProtectedData.Protect(SCUtility.String2Bytes(sharedKey), null, DataProtectionScope.CurrentUser)));

                        xSecurity.Add(xPSK);
                    }

                    
                }
                this.xdocProfile = xdocProfile;

            }
            catch (Exception ex)
            {
                // add logging
            }

        }

        public void SetProfile()
        {
            try
            {
                if (xdocProfile==null)
                {
                    xdocProfile = XDocument.Load(AppDomain.CurrentDomain.BaseDirectory + dot1XTemplateFilename);
                    XNamespace xNS = xdocProfile.Root.GetDefaultNamespace();
                    xdocProfile.Element(xNS + "WLANProfile").Element(xNS + "name").Value = name;
                    // hex string with separator or not?
                    xdocProfile.Element(xNS + "WLANProfile").Element(xNS + "SSIDConfig").Element(xNS + "SSID").Element(xNS + "name").Value = name;
                    xdocProfile.Element(xNS + "WLANProfile").Element(xNS + "SSIDConfig").Element(xNS + "SSID").Element(xNS + "hex").Value = SCUtility.String2HexStr(name);
                    XElement xSecurity = xdocProfile.Element(xNS + "WLANProfile").Element(xNS + "MSM").Element(xNS + "security");
                    xSecurity.Element(xNS + "authEncryption").Element(xNS + "authentication").Value = authentication;
                    xSecurity.Element(xNS + "authEncryption").Element(xNS + "encryption").Value = encryption;
                    xSecurity.Element(xNS + "authEncryption").Element(xNS + "useOneX").Value = useOneX.ToString();
                    if (!useOneX)
                    {
                        XElement xPSK = new XElement("sharedKey");
                        xPSK.Add(new XElement("keyType"));
                        xPSK.Add(new XElement("protected"));
                        xPSK.Add(new XElement("keyMaterial"));
                        xPSK.Element("keyType").Value = "passPhrase";
                        xPSK.Element("protected").Value = "true";
                        xPSK.Element("keyMaterial").Value = SCUtility.String2HexStr(SCUtility.Bytes2String(ProtectedData.Protect(SCUtility.String2Bytes(sharedKey), null, DataProtectionScope.CurrentUser)));

                        xSecurity.Add(xPSK);
                    }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

    }
}
