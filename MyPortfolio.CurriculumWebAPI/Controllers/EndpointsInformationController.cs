﻿using CurriculumWebAPI.App.Controllers.ControllersInformationModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;

namespace CurriculumWebAPI.App.Controllers
{
    [ApiController]
    [Route("V1/api/endpoints")]
    public class EndpointsInformationController : ControllerBase
    {
        private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;

        public EndpointsInformationController
            (IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
        {
            _actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
        }



        [HttpGet("htu")]
        public async Task<IActionResult> Get()
        {
            var apiInfos = new List<ApiInfo>();
            var actionDescriptors = _actionDescriptorCollectionProvider.ActionDescriptors.Items;

            foreach (var actionDescriptor in actionDescriptors)
            {
                if (actionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
                {
                    var apiInfo = new ApiInfo
                    {
                        HttpMethod = GetHttpMethod(controllerActionDescriptor),
                        Route = controllerActionDescriptor.AttributeRouteInfo.Template,
                        Parameters = new List<ParameterInfo>(),
                    };

                    foreach (var parameter in controllerActionDescriptor.MethodInfo.GetParameters())
                    {
                        var parameterInfo = new ParameterInfo
                        {
                            Name = parameter.Name,
                            Type = new TypeInfo
                            {
                                //Id = Guid.NewGuid().ToString(), // Gerar um ID único para o tipo
                                Properties = parameter.ParameterType
                                    .GetProperties()
                                    .ToDictionary(prop => prop.Name, prop => prop.PropertyType.FullName),
                            },
                        };

                        apiInfo.Parameters.Add(parameterInfo);
                    }

                    apiInfos.Add(apiInfo);
                }
            }

            return Ok(apiInfos);
        }

        private string GetHttpMethod(ControllerActionDescriptor controllerActionDescriptor)
        {
            var httpMethodAttribute = controllerActionDescriptor.MethodInfo
                .GetCustomAttributes(typeof(HttpMethodAttribute), inherit: true)
                .FirstOrDefault() as HttpMethodAttribute;

            return httpMethodAttribute?.HttpMethods.FirstOrDefault();
        }

    }
}
