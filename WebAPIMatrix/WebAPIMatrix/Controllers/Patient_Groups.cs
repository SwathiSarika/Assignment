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
            return new string[] { "value13", "value23" };
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
            int[][] A = JsonConvert.DeserializeObject<int[][]>(json1);

            int totalNumGroups = 0;
            int curCnt = 0;

            for (int x = 0; x < A.Length; x++)
            {
                for (int y = 0; y < A[x].Length; y++)
                {
                    if (A[x][y] == 1)
                    {
                        curCnt = 0;
                        totalNumGroups = totalNumGroups + cleanBlock(A, x, y, curCnt);
                    }
                    // else (0), keep curCnt and totalNumGroups as are.
                }
            }


            var result = new
            {
                numberOfGroups = totalNumGroups
               
            };
            return Ok(result);

        }
        public static int cleanBlock(int[][] A, int x, int y, int cnt)
        {
            A[x][y] = 0;
            if (inMatrix(x - 1, y, A.Length, A[0].Length) == 1 && A[x - 1][y] == 1)
            {
                cleanBlock(A, x - 1, y, cnt);
                cnt = 1;
            }
            if (inMatrix(x + 1, y, A.Length, A[0].Length) == 1 && A[x + 1][y] == 1)
            {
                cleanBlock(A, x + 1, y, cnt);
                cnt = 1;
            }
            if (inMatrix(x, y - 1, A.Length, A[0].Length) == 1 && A[x][y - 1] == 1)
            {
                cleanBlock(A, x, y - 1, cnt);
                cnt = 1;
            }
            if (inMatrix(x, y + 1, A.Length, A[0].Length) == 1 && A[x][y + 1] == 1)
            {
                cleanBlock(A, x, y + 1, cnt);
                cnt = 1;
            }
            if (inMatrix(x - 1, y-1, A.Length, A[0].Length) == 1 && A[x - 1][y-1] == 1)
            {
                cleanBlock(A, x - 1, y-1, cnt);
                cnt = 1;
            }
            if (inMatrix(x - 1, y+1, A.Length, A[0].Length) == 1 && A[x - 1][y+1] == 1)
            {
                cleanBlock(A, x -1, y+1, cnt);
                cnt = 1;
            }
            if (inMatrix(x+1, y - 1, A.Length, A[0].Length) == 1 && A[x+1][y - 1] == 1)
            {
                cleanBlock(A, x+1, y - 1, cnt);
                cnt = 1;
            }
            if (inMatrix(x+1, y + 1, A.Length, A[0].Length) == 1 && A[x+1][y + 1] == 1)
            {
                cleanBlock(A, x+1, y + 1, cnt);
                cnt = 1;
            }
            return 1;
        }

        public static int inMatrix(int x, int y, int lenX, int lenY)
        {
            if ((x >= 0 && x <= (lenX - 1)) && (y >= 0 && y <= (lenY - 1)))
                return 1;
            else
                return 0;
        }
    
    }
}
