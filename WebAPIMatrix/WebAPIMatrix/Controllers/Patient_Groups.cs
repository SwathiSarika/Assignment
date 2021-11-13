using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WebAPIMatrix.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Patient_GroupsController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "Welcome" };
        }

       
        [HttpPost("Calculate")]
        public  ActionResult Calculate()
        {
            string body = "";
            using (StreamReader stream = new StreamReader(Request.Body))
            {
                body =  stream.ReadToEnd();
               
            }
            JObject json = JObject.Parse(body);
           
            var mat =  json["matrix"];
            string json1 = JsonConvert.SerializeObject(mat, Formatting.Indented);
            int[][] patients = JsonConvert.DeserializeObject<int[][]>(json1);

            int totalNumGroups = 0;
           

            for (int x = 0; x < patients.Length; x++)
            {
                for (int y = 0; y < patients[x].Length; y++)
                {
                    if (patients[x][y] == 1)
                    {
                        
                        totalNumGroups = totalNumGroups + clearBlock(patients, x, y);
                    }
                    
                }
            }


            var result = new
            {
                numberOfGroups = totalNumGroups
               
            };
            return Ok(result);

        }
        //Make all the group of 1's as 0's
        public static int clearBlock(int[][] patients, int x, int y)
        {
            patients[x][y] = 0;
            if (withinMatrix(x - 1, y, patients.Length, patients[0].Length) == 1 && patients[x - 1][y] == 1)
            {
                clearBlock(patients, x - 1, y);
                
            }
            if (withinMatrix(x + 1, y, patients.Length, patients[0].Length) == 1 && patients[x + 1][y] == 1)
            {
                clearBlock(patients, x + 1, y);
               
            }
            if (withinMatrix(x, y - 1, patients.Length, patients[0].Length) == 1 && patients[x][y - 1] == 1)
            {
                clearBlock(patients, x, y - 1);
               
            }
            if (withinMatrix(x, y + 1, patients.Length, patients[0].Length) == 1 && patients[x][y + 1] == 1)
            {
                clearBlock(patients, x, y + 1);
               
            }
            if (withinMatrix(x - 1, y-1, patients.Length, patients[0].Length) == 1 && patients[x - 1][y-1] == 1)
            {
                clearBlock(patients, x - 1, y-1);
                
            }
            if (withinMatrix(x - 1, y+1, patients.Length, patients[0].Length) == 1 && patients[x - 1][y+1] == 1)
            {
                clearBlock(patients, x -1, y+1);
               
            }
            if (withinMatrix(x+1, y - 1, patients.Length, patients[0].Length) == 1 && patients[x+1][y - 1] == 1)
            {
                clearBlock(patients, x+1, y - 1);
               
            }
            if (withinMatrix(x+1, y + 1, patients.Length, patients[0].Length) == 1 && patients[x+1][y + 1] == 1)
            {
                clearBlock(patients, x+1, y + 1);
               
            }
            return 1;
        }
        //check position of matrix is within the boundary of matrix
        public static int withinMatrix(int x, int y, int lenX, int lenY)
        {
            if ((x >= 0 && x <= (lenX - 1)) && (y >= 0 && y <= (lenY - 1)))
                return 1;
            else
                return 0;
        }
    
    }
}
