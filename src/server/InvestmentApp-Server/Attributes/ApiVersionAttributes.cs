using System;
using Microsoft.AspNetCore.Mvc;

namespace InvestmentApp.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class V1Attribute : ApiVersionAttribute
{
    public V1Attribute(): base(new ApiVersion(1, 0)) { }
}
