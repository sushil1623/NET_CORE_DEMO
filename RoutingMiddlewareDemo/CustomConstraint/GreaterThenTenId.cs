
namespace RoutingMiddlewareDemo.CustomConstraint
{
    public class GreaterThenTenId : IRouteConstraint
    {
        public bool Match(HttpContext? httpContext, IRouter? route,
            string routeKey, RouteValueDictionary values,
            RouteDirection routeDirection)
        {
            if (!values.TryGetValue(routeKey, out var value))
            {
                return false;
            }
            if (int.TryParse(Convert.ToString(value), out int Idparam))
            {
                return Idparam > 10 ? true : false;

            }
            else
            {
                return false;
            }
        }
    }
}
