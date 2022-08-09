using System;

namespace IntegrationTests.Helpers
{
    public static class TestHelper
    {
        public static string RandomString() 
        {
            Random res = new Random();

            // String that contain both alphabets and numbers
            String str = "abcdefghijklmnopqrstuvwxyz0123456789";
            int size = 8;
            String result = String.Empty;
            foreach (var i in str) 
            {
                int x = res.Next(size);
                result = result + str[x];
            }

            return result;
        }
    }
}
