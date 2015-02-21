using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace SmartConnect
{
    
    public class SSID
    {
        String name;
        public String Name
        { get { return name; } }

        String profileName;
        public String ProfileName
        { get { return name; } }
        
        String role;
        public String Role
        { get { return role; } }

        String type;
        public String Type
        { get { return type; } }

        Boolean broadcast;
        
        String authentication;
        public String Authentication
        { get { return authentication; } }
        
        String encryption;
        public String Encryption
        { get { return encryption; } }
        
        Boolean useOneX;
        public Boolean OneX
        { get { return useOneX; } }
        
        String sharedKey;
        public String SharedKey
        { get { return sharedKey; } }

        String dot1XTemplateFilename;
        public String Dot1XTemplateFilename
        { get { return dot1XTemplateFilename; } }
        
        String TrustedRootCA;

        XDocument xdocProfile = null;
        public String Profile
        { get { 
            String tmpProfile = xdocProfile.ToString();
            tmpProfile = "<?xml version=\"1.0\"?>" + System.Environment.NewLine + tmpProfile.Replace(">True<", ">true<").Replace(">False<",">false<");
            return tmpProfile;
        } }

        public SSID(String name, String profileName, String role, String type, Boolean broadcast, String authentication, String encryption, Boolean useOneX, String sharedKey, String dot1XTemplateFilename, XDocument xdocProfile)
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
                if (xdocProfile.Equals(null))
                {
                    xdocProfile = new XDocument(AppDomain.CurrentDomain.BaseDirectory + dot1XTemplateFilename);
                    XNamespace xNS = xdocProfile.Root.GetDefaultNamespace();
                    xdocProfile.Element(xNS + "name").Value = name;
                    // hex string with separator or not?
                    xdocProfile.Element(xNS + "SSIDConfig").Element(xNS + "SSID").Element(xNS + "name").Value = name;
                    xdocProfile.Element(xNS + "SSIDConfig").Element(xNS + "SSID").Element(xNS + "hex").Value = Utility.String2HexStr(name);
                    XElement xSecurity = xdocProfile.Root.Element(xNS + "MSM").Element(xNS + "security");
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
                        xPSK.Element("keyMaterial").Value = Utility.String2HexStr(Utility.Bytes2String(ProtectedData.Protect(Utility.String2Bytes(sharedKey), null, DataProtectionScope.CurrentUser)));

                        xSecurity.Add(xPSK);
                    }


                }
                else
                {
                    this.xdocProfile = new XDocument(xdocProfile);
                }
            }
            catch (Exception ex)
            {
                // add logging
            }

        }

        public void setProfile()
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
                    xdocProfile.Element(xNS + "WLANProfile").Element(xNS + "SSIDConfig").Element(xNS + "SSID").Element(xNS + "hex").Value = Utility.String2HexStr(name);
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
                        xPSK.Element("keyMaterial").Value = Utility.String2HexStr(Utility.Bytes2String(ProtectedData.Protect(Utility.String2Bytes(sharedKey), null, DataProtectionScope.CurrentUser)));

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
