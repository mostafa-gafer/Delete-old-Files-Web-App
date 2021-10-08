using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //get all files from folder and insert it to array of string
            string[] files = Directory.GetFiles(Directory.GetCurrentDirectory() + "/wwwroot/Files/");
            //declare list of file model 
            List<FileModel> result = new List<FileModel>();
            //to get all element in files 
            foreach (var file in files)
            {
                //to get file info s fileinfo object
                FileInfo fileInfo = new FileInfo(file);
                //to get end of country index
                int _Index = fileInfo.Name.IndexOf('_') > -1 ? fileInfo.Name.IndexOf('_') : 0;
                //to get date form name
                int dotIndex = fileInfo.Name.IndexOf('.');
                var date = fileInfo.Name.Substring((_Index + 1), (dotIndex - _Index) - 1);
                //to get country name from file name
                var country = fileInfo.Name.Substring(0, _Index);
                //declare object feom file model
                FileModel item = new FileModel();
                //insert data to model
                item.FileName = fileInfo.Name;
                item.CreationDate = fileInfo.CreationTime;
                item.Country = country;
                item.Extention = fileInfo.Extension;
                //add object to list
                result.Add(item);


            }

        
            return View(result);
        }
        [HttpGet]
        public IActionResult Delete()
        {
            //get all files from folder and insert it to array of string
            string[] files = Directory.GetFiles(Directory.GetCurrentDirectory() + "/wwwroot/Files/");
            //declare list of file model 
            List<FileModel> result = new List<FileModel>();
            //to get all element in files 
            foreach (var file in files)
            {
                //to get file info s fileinfo object
                FileInfo fileInfo = new FileInfo(file);
                //to get end of country index
                int _Index = fileInfo.Name.IndexOf('_') > -1 ? fileInfo.Name.IndexOf('_') : 0;
                //to get date form name
                int dotIndex = fileInfo.Name.IndexOf('.') ;
                var date = fileInfo.Name.Substring((_Index+1), (dotIndex - _Index)-1);
                //to get country name from file name
                var country = fileInfo.Name.Substring(0, _Index);
                //for loop in files
                foreach (var fileL in files)
                {
                    FileInfo fileInfoL = new FileInfo(fileL);
                    int _IndexL = fileInfoL.Name.IndexOf('_') > -1? fileInfoL.Name.IndexOf('_'): 0;
                    var countryL = fileInfoL.Name.Substring(0, _IndexL);
                    //to get date form name
                    int dotIndexL = fileInfoL.Name.IndexOf('.');
                    var dateL = fileInfoL.Name.Substring((_IndexL + 1), (dotIndexL - _IndexL) - 1);
                    //to check country name same or not
                    if (country.ToLower() == countryL.ToLower() && country !="" && countryL !="")
                    {
                        if (double.Parse(date) > double.Parse(dateL))
                        {
                            //declare object feom file model
                            FileModel item = new FileModel();
                            //insert data to model
                            item.FileName = fileInfoL.Name;
                            item.CreationDate = fileInfoL.CreationTime;
                            item.Country = countryL;
                            item.Extention = fileInfoL.Extension;
                            //add object to list
                            result.Add(item);
                            //delete file from folder
                            fileInfoL.Delete();
                        }
                        ////check which file is older creation data
                        //if(fileInfo.CreationTime > fileInfoL.CreationTime)
                        //{
                        //    //declare object feom file model
                        //    FileModel item = new FileModel();
                        //    //insert data to model
                        //    item.FileName = fileInfoL.Name;
                        //    item.CreationDate = fileInfoL.CreationTime;
                        //    item.Country = countryL;
                        //    item.Extention = fileInfoL.Extension;
                        //    //add object to list
                        //    result.Add(item);
                        //    //delete file from folder
                        //    fileInfoL.Delete();

                        //}
                        
                        
                    }

                }
               
            }
            return View(result);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
