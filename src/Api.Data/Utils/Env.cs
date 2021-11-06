using System;

namespace Api.Data.Utils
{
    public class Env
    {
        /// <summary>
        /// Get variable value on compose file if there isn't then get value on constant values
        /// </summary>
        public static readonly Func<string, string, string> Get = (string env, string valorDefault) => Environment.GetEnvironmentVariable(env) ?? valorDefault;
    }
}
