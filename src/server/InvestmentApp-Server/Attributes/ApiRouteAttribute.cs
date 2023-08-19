using System;
using Microsoft.AspNetCore.Mvc;

namespace InvestmentApp.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class ApiRouteAttribute : RouteAttribute
{
    public ApiRouteAttribute(): base("v{version:apiVersion}") { }
}

[AttributeUsage(AttributeTargets.Class)]
public class CustomizableApiRouteAttribute : RouteAttribute
{
    public CustomizableApiRouteAttribute(): base("v{version:apiVersion}") { }
}
