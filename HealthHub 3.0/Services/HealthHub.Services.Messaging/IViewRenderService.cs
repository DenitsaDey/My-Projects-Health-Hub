namespace HealthHub.Services.Messaging
{
    using System.Threading.Tasks;

    public interface IViewRenderService
    {
        Task<string> RenderToStringAsync(string viewName, object model);
    }
}

// from https://nikolay.it/Blog/2018/03/Generate-PDF-file-from-Razor-view-using-ASP-NET-Core-and-PhantomJS/37
