using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using McGill.Web;
using McGill.Library;
using McGillWebAPI.Models;
using McGillWebAPI.Model;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.IdentityModel.Tokens.Jwt;
using System.IdentityModel.Tokens;
using System.Text;
using System.IO;
using System.Xml;
using Microsoft.EntityFrameworkCore;

namespace McGillWebAPI.Controllers
{   
    [Route("api/[controller]")]
    public class OnlineAppController : Controller
    {   
        private static string _keyString = "using ( UnitedMcGillContext umc = new UnitedMcGillContext() )";

        private static string EncryptSSN( string sSSN )
        {
            var key = Encoding.UTF8.GetBytes(_keyString);

            using (var aesAlg = Aes.Create())
            {
                using (var encryptor = aesAlg.CreateEncryptor(key, aesAlg.IV))
                {
                    using (var msEncrypt = new MemoryStream())
                    {
                        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(sSSN);
                        }

                        var iv = aesAlg.IV;

                        var decryptedContent = msEncrypt.ToArray();

                        var result = new byte[iv.Length + decryptedContent.Length];

                        Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
                        Buffer.BlockCopy(decryptedContent, 0, result, iv.Length, decryptedContent.Length);

                        return Convert.ToBase64String(result);
                    }
                }
            }          
        }

        private static string DecryptSSN( string sSSN )
        {
            var fullCipher = Convert.FromBase64String(sSSN);

            var iv = new byte[16];
            var cipher = new byte[16];

            Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, iv.Length);
            var key = Encoding.UTF8.GetBytes(_keyString);

            using (var aesAlg = Aes.Create())
            {
                using (var decryptor = aesAlg.CreateDecryptor(key, iv))
                {
                    string result;
                    using (var msDecrypt = new MemoryStream(cipher))
                    {
                        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (var srDecrypt = new StreamReader(csDecrypt))
                            {
                                result = srDecrypt.ReadToEnd();
                            }
                        }
                    }

                    return result;
                }
            }
        }

        // Get api/onlineapp/code
        [HttpGet("{code}")]
        [AllowAnonymous]
        public EmploymentApp Get(string code)
        {
            using ( UnitedMcGillContext umc = new UnitedMcGillContext() )
            {
                // Ensure code exists
                if ( umc.EmploymentApp.Where(t => t.Code == code).Count() > 0 )
                {
                    var app =  umc.EmploymentApp
                                                .Include("SectionA")
                                                .Include("SectionB")
                                                .Include("SectionC")
                                                .Include("SectionD")
                                                .Include("SectionE")
                                                .Include("SectionF")
                                                .First(t => t.Code == code);
                    // Dycrypt SSN
                    return app;
                }

                return null;
            }             
        }

        // PUT api/onlineapp/766
        [HttpPut("{id}")]
        [AllowAnonymous]
        public IActionResult Put(int id, [FromBody] EmploymentApp app)
        {
            using ( UnitedMcGillContext umc = new UnitedMcGillContext() )
            {
                app.EmploymentAppId = id;
                // Encrypt SSN

                umc.EmploymentApp.Update(app);
                umc.SaveChanges();
            }

            return Ok();
        }

        public static string GenerateXML(Dictionary<string, string> appValues)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.InsertBefore(xmlDoc.CreateXmlDeclaration("1.0", "ISO-8859-1", String.Empty), xmlDoc.DocumentElement);

            XmlElement dataset = xmlDoc.CreateElement("NewDataSet");
            xmlDoc.AppendChild(dataset);

            XmlElement application = xmlDoc.CreateElement("application");
            dataset.AppendChild(application);

            foreach (KeyValuePair<string, string> kvPair in appValues)
            {
                string sXMLName = kvPair.Key;
                string sXMLValue = kvPair.Value;
                XmlNode propNode = application.AppendChild(xmlDoc.CreateElement(sXMLName));

                if (sXMLName.Equals("agree_time", StringComparison.CurrentCultureIgnoreCase))
                {
                    double dSeconds = UMCLib.ConvertToDouble(sXMLValue);

                    if (dSeconds != 0D)
                    {
                        DateTime agreeDate = new DateTime(1970, 01, 01).AddSeconds(dSeconds);
                        propNode.AppendChild(xmlDoc.CreateTextNode(agreeDate.ToString("MMMM dd, yyyy")));
                    }
                }
                else if (sXMLName.Equals("ssn", StringComparison.CurrentCultureIgnoreCase))
                {
                    propNode.AppendChild(xmlDoc.CreateTextNode(DecryptSSN(sXMLValue)));
                }
                else if (!String.IsNullOrEmpty(sXMLValue))
                {
                    propNode.AppendChild(xmlDoc.CreateTextNode(sXMLValue));
                }
            }

            string sXMLFile = Path.ChangeExtension(Path.GetTempFileName(), ".xml");
            var outFile = System.IO.File.Create(sXMLFile);
            var saveStream = new System.IO.StreamWriter(outFile);
            xmlDoc.Save(saveStream);

            // Remove illegal report characters
            string sContents = System.IO.File.ReadAllText(sXMLFile);
            sContents = sContents.Replace("&amp;", "&");
            sContents = sContents.Replace("&lt;", String.Empty);
            sContents = sContents.Replace("&gt;", String.Empty);
            sContents = sContents.Replace(" &amp; ", " and ");
            sContents = sContents.Replace("&amp;", " and ");
            sContents = sContents.Replace(" & ", " and ");
            sContents = sContents.Replace("&", " and ");

            System.IO.File.WriteAllText(sXMLFile, sContents);

            return sXMLFile;
        }        
    }
}
