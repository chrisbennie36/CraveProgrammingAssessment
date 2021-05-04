using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace OrderingApiIntegrationTests.Helpers
{
    [ExcludeFromCodeCoverage]
    public static class GetPostPutDelete
    {
        private static readonly IWebApiCaller Caller = new WebApiCaller();

        public static void Put<T>(string url, T data)
        {
            Caller.Put(url, data, new int[0]);
        }

        public static Task PutAsync<T>(string url, T data)
        {
            return Caller.PutAsync(url, data);
        }

        public static void Delete(string url)
        {
            Caller.Delete(url, new int[0]);
        }

        public static Task DeleteAsync(string url)
        {
            return Caller.DeleteAsync(url);
        }

        public static T Post<T>(string url, T data)
        {
            return Post<T, T>(url, data);
        }

        public static Task<T> PostAsync<T>(string url, T data)
        {
            return PostAsync<T, T>(url, data);
        }

        public static TX Post<T, TX>(string url, T data)
        {
            return Caller.Post<TX, T>(url, data);
        }

        public static Task<TX> PostAsync<T, TX>(string url, T data)
        {
            return Caller.PostAsync<TX, T>(url, data);
        }

        public static T2 PostSpecial<T, T2>(string url, T data)
        {
            return Caller.Post<T2, T>(url, data);
        }

        public static Task<T2> PostSpecialAsync<T, T2>(string url, T data)
        {
            return Caller.PostAsync<T2, T>(url, data);
        }

        public static T Get<T>(string url)
        {
            return Caller.Get<T>(url);
        }

        public static async Task<T> GetAsync<T>(string url)
        {
            return await Caller.GetAsync<T>(url).ConfigureAwait(false);
        }
    }
}
