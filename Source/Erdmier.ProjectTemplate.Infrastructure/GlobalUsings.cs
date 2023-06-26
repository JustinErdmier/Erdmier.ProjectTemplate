global using System.Collections.Concurrent;

global using Erdmier.ProjectTemplate.Application.Common.Models;
global using Erdmier.ProjectTemplate.Application.EmailProvider.Interfaces;
global using Erdmier.ProjectTemplate.Application.EmailProvider.Models;
global using Erdmier.ProjectTemplate.Domain.Identity.Entities;
global using Erdmier.ProjectTemplate.Infrastructure.EmailProvider.Services;
global using Erdmier.ProjectTemplate.Infrastructure.Identity.EntityConfigurations;
global using Erdmier.ProjectTemplate.Infrastructure.Persistence.Contexts;
global using Erdmier.ProjectTemplate.Infrastructure.Persistence.EntityConfigurationModelBuilders;

global using Microsoft.AspNetCore.Authentication.Cookies;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Hosting;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Metadata;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using Microsoft.EntityFrameworkCore.Migrations;
global using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Hosting;

global using Serilog;
