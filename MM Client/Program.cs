namespace MM_Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            CheckUpdate checkUpdate = new CheckUpdate();

            await checkUpdate.NewUpdate();
        }
    }
}
