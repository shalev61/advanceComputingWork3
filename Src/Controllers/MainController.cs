using Ex3.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ex3.Controllers
{
    public class MainController : Controller
    {
        // GET: Main
        public ActionResult Default()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Display(string ip, int port)
        {
            System.Net.IPAddress p;
            if (System.Net.IPAddress.TryParse(ip, out p) == false)
            {
                // Load Travel
                ViewBag.Filename = ip;
                ViewBag.Frequency = port;

                return View("LoadTravel");
            }
            else
            {
                // Display
                InfoModel infoModel = InfoModel.Instance;
                infoModel.Connect(ip, port);

                ViewBag.Coordinate = infoModel.GetCoordinate();

                infoModel.Disconnect();

                return View();
            }
        }

        [HttpGet]
        public ActionResult DisplayTravel(string ip, int port, int frequency)
        {
            ViewBag.Ip = ip;
            ViewBag.Port = port;
            ViewBag.Frequency = frequency;

            return View();
        }

        [HttpGet]
        public ActionResult SaveTravel(string ip, int port, int frequency, int time, string filename)
        {
            ViewBag.Ip = ip;
            ViewBag.Port = port;
            ViewBag.Frequency = frequency;
            ViewBag.Time = time;
            ViewBag.Filename = filename;

            return View();
        }

        public const string SCENARIO_FILE = "~/App_Data/{0}.txt";

        public string LoadFromFile(string filename, int lineNumber)
        {
            string s;
            string path = System.Web.HttpContext.Current.Server.MapPath(String.Format(SCENARIO_FILE, filename));

            using (TextReader tr = new StreamReader(path))
            {
                for (int i = 1; i < lineNumber; i++)
                    tr.ReadLine();
                s = tr.ReadLine();
                tr.Close();
            }
            if (s == null || s == "")
            {
                return "";
            }
            else {
                return s.Substring(0, s.IndexOf("+"));
            }
        }

        public void SaveToFile(string filename, string line, string ip, int port)
        {
            string path = System.Web.HttpContext.Current.Server.MapPath(String.Format(SCENARIO_FILE, filename));

            line += "+" + GetStringRudderThrottle(ip, port);

            TextWriter textWriter = new StreamWriter(path, true);
            textWriter.WriteLine(line);
            textWriter.Close();
        }

        public string GetStringCoordinate(string ip, int port)
        {
            InfoModel infoModel = InfoModel.Instance;
            infoModel.Connect(ip, port);

            Coordinate coordinate = infoModel.GetCoordinate();

            infoModel.Disconnect();

            return coordinate.Longitude.ToString() + " " + coordinate.Latitude.ToString();
        }

        public string GetStringRudderThrottle(string ip, int port)
        {
            InfoModel infoModel = InfoModel.Instance;
            infoModel.Connect(ip, port);

            Coordinate coordinate = infoModel.GetRudderThrottle();

            infoModel.Disconnect();

            return coordinate.Longitude.ToString() + " " + coordinate.Latitude.ToString();
        }
    }
}