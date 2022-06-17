namespace WebApplicationTemplate.Infrastructure
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Routing;

    public static class EndpointsRouteBuilderExtensions
    {
        public static void MapDefaultAreaRoute(this IEndpointRouteBuilder endpoints)
            => endpoints.MapControllerRoute(
                    name: "Areas",
                    pattern: "/{area:exists}/{controller=Home}/{action=Index}/{id?}");
        //map the route only if the area "exists", else look in the normal controller,
        //default controller is Home, default action is Index, id is optional

    }
}
