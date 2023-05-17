namespace WebAPI.Extensions
{
    public static class Env
    {
        public static void LoadDotEnv()
        {
            var filePath = $"{AppDomain.CurrentDomain.BaseDirectory}.env";
            if (!File.Exists(filePath))
            {
                return;
            }
            foreach (var line in File.ReadAllLines(filePath))
            {
                var parts = line.Split(
                    '=',
                    2,
                    StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length != 2)
                {
                    continue;
                }
                Environment.SetEnvironmentVariable(parts[0], parts[1]);
            }
        }
    }
}
