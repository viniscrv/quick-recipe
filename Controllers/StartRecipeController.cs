using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using quick_recipe.Data;
using quick_recipe.Models;

namespace quick_recipe.Controllers;

[ApiController]
[Route("/start-recipe")]
public class StartRecipeController : ControllerBase
{
    private readonly ApplicationContext _context;

    public StartRecipeController(ApplicationContext context)
    {
        _context = context;
    }

    dsadas
}