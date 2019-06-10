namespace Ilkyar.WebAPI.Helpers
{
    public static class ApiHelper
    {
        public static bool CheckKey(string data)
        {
            if (data == "886363c990c527eab81a1b5a1d9d2639")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}