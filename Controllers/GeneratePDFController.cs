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
using System.Reflection;
using System.Diagnostics;

namespace McGillWebAPI.Controllers
{   
    [Route("api/[controller]")]
    public class GeneratePDFController : Controller
    {   
        // POST api/applicant
        [HttpPut]
        [AllowAnonymous]
        public IActionResult Put([FromBody] EmploymentApp app )
        {
            Type empAppType = app.GetType();
            Type sectionAType = app.SectionA.GetType();
            Type sectionBType = app.SectionB.GetType();
            Type sectionCType = app.SectionC.GetType();
            Type sectionDType = app.SectionD.GetType();
            Type sectionEType = app.SectionE.GetType();
            Type sectionFType = app.SectionF.GetType(); 

            HashSet<string> empAppProps = new HashSet<string>();
            HashSet<string> sectionAProps = new HashSet<string>();
            HashSet<string> sectionBProps = new HashSet<string>();
            HashSet<string> sectionCProps = new HashSet<string>();
            HashSet<string> sectionDProps = new HashSet<string>();
            HashSet<string> sectionEProps = new HashSet<string>();
            HashSet<string> sectionFProps = new HashSet<string>();

            Dictionary<string, string> xmlValues = new Dictionary<string, string>();

            foreach (PropertyInfo oneProp in empAppType.GetProperties())
            {
                empAppProps.Add(oneProp.Name);
            }
            foreach (PropertyInfo oneProp in sectionAType.GetProperties())
            {
                sectionAProps.Add(oneProp.Name);
            }
            foreach (PropertyInfo oneProp in sectionBType.GetProperties())
            {
                sectionBProps.Add(oneProp.Name);
            }
            foreach (PropertyInfo oneProp in sectionCType.GetProperties())
            {
                sectionCProps.Add(oneProp.Name);
            }
            foreach (PropertyInfo oneProp in sectionDType.GetProperties())
            {
                sectionDProps.Add(oneProp.Name);
            }
            foreach (PropertyInfo oneProp in sectionEType.GetProperties())
            {
                sectionEProps.Add(oneProp.Name);
            }
            foreach (PropertyInfo oneProp in sectionFType.GetProperties())
            {
                sectionFProps.Add(oneProp.Name);
            }

            // Remove fields not wanted in xml file
            empAppProps.Remove("SectionA");
            empAppProps.Remove("SectionB");
            empAppProps.Remove("SectionC");
            empAppProps.Remove("SectionD");
            empAppProps.Remove("SectionE");
            empAppProps.Remove("SectionF");
            empAppProps.Remove("EmploymentAppId");
            sectionAProps.Remove("EmploymentApp");
            sectionAProps.Remove("EmploymentAppId");
            sectionBProps.Remove("EmploymentApp");
            sectionBProps.Remove("EmploymentAppId");
            sectionCProps.Remove("EmploymentApp");
            sectionCProps.Remove("EmploymentAppId");
            sectionDProps.Remove("EmploymentApp");
            sectionDProps.Remove("EmploymentAppId");
            sectionEProps.Remove("EmploymentApp");
            sectionEProps.Remove("EmploymentAppId");
            sectionFProps.Remove("EmploymentApp");
            sectionFProps.Remove("EmploymentAppId");

            xmlValues.Add("id", app.EmploymentAppId.ToString());

            foreach( string prop in empAppProps )
            {
                PropertyInfo pi = empAppType.GetProperty(prop);
                xmlValues.Add(JavascriptNameToXML(prop),safeExtract(app,pi));
            }

            foreach( string prop in sectionAProps )
            {
                PropertyInfo pi = sectionAType.GetProperty(prop);
                xmlValues.Add(JavascriptNameToXML(prop),pi.GetValue(app.SectionA).ToString());
            }

            foreach( string prop in sectionBProps )
            {
                PropertyInfo pi = sectionBType.GetProperty(prop);
                xmlValues.Add(JavascriptNameToXML(prop),pi.GetValue(app.SectionB).ToString());
            }

            foreach( string prop in sectionCProps )
            {
                PropertyInfo pi = sectionCType.GetProperty(prop);
                xmlValues.Add(JavascriptNameToXML(prop),pi.GetValue(app.SectionC).ToString());
            }

            foreach( string prop in sectionDProps )
            {
                PropertyInfo pi = sectionDType.GetProperty(prop);
                xmlValues.Add(JavascriptNameToXML(prop),pi.GetValue(app.SectionD).ToString());
            }

            foreach( string prop in sectionEProps )
            {
                PropertyInfo pi = sectionEType.GetProperty(prop);
                xmlValues.Add(JavascriptNameToXML(prop),pi.GetValue(app.SectionE).ToString());
            }

            foreach( string prop in sectionFProps )
            {
                PropertyInfo pi = sectionFType.GetProperty(prop);
                xmlValues.Add(JavascriptNameToXML(prop),pi.GetValue(app.SectionF).ToString());
            }

            string sXML = GenerateXML(xmlValues);
            string sOutput = "";
            
            using ( Process p = new Process() )
            {
                p.StartInfo.FileName = @"c:\Program Files (x86)\PHP\v5.3\php-cgi.exe";
                p.StartInfo.Arguments = @"-f C:\unitedmcgill\wwwroot\content\reports\viewapp.php XML=" + sXML;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.UseShellExecute = false;

                p.Start();
                p.WaitForExit();

                sOutput = p.StandardOutput.ReadToEnd();
            }
            Values value = new Values();
            value.Value = sOutput;
            return Ok(value);
        }

        private string safeExtract(EmploymentApp app, PropertyInfo pi)
        {
            if ( pi.GetValue(app) != null)
            {
                return pi.GetValue(app).ToString();
            }

            return "";
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
                // else if (sXMLName.Equals("ssn", StringComparison.CurrentCultureIgnoreCase))
                // {
                //     propNode.AppendChild(xmlDoc.CreateTextNode(OnlineAppController.DecryptSSN(sXMLValue)));
                // }
                else if (!String.IsNullOrEmpty(sXMLValue))
                {
                    propNode.AppendChild(xmlDoc.CreateTextNode(sXMLValue));
                }
            }

            string sXMLFile = Path.ChangeExtension(Path.GetTempFileName(), ".xml");
            var outFile = System.IO.File.Create(sXMLFile);
            using ( var saveStream = new System.IO.StreamWriter(outFile) )
            {
                xmlDoc.Save(saveStream);
            }
            
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

        private static string JavascriptNameToXML(string sPropertyName)
        {
            StringBuilder sbName = new StringBuilder();
            sbName.Append(sPropertyName[0]);

            for (int i = 1; i < sPropertyName.Length; i++)
            {
                if (Char.IsUpper(sPropertyName[i]))
                {
                    sbName.Append('_');
                }

                sbName.Append(sPropertyName[i]);
            }

            string sXML = sbName.ToString().ToLower();

            return sXML.Replace("mc_gill", "mcgill");
        }

    }
}
