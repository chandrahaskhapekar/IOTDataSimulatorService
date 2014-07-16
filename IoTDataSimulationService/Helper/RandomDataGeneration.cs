using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace IoTDataSimulationService.Helper
{
    public class RandomDataGeneration
    {
        public RandomDataGeneration() { }
       
        private static Random random = new Random((int)DateTime.Now.Ticks);

        public static string RandomString(int size)
        {
            try
            {
                StringBuilder builder = new StringBuilder();
                char character;
                for(int i=0;i<size;i++)
                {
                    character = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                    builder.Append(character);
                }
                return builder.ToString();
            }
            catch(Exception ex)
            {
                return "";
            }
        }

        public static int RandomNumbers()
        {
            try
            {
                return random.Next();
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}