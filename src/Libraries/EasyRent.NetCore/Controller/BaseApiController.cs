using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace EasyRent.NetCore.Controller;

public class BaseApiController : ControllerBase
{
    protected readonly MediatrControllerRequestHandler RequestHandler;
    protected readonly IMapper Mapper;

    protected BaseApiController(MediatrControllerRequestHandler requestHandler,
        IMapper mapper)
    {
        RequestHandler = requestHandler;
        Mapper = mapper;
    }
}