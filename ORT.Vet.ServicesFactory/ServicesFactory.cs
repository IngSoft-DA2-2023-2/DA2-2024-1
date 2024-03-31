﻿using Microsoft.Extensions.DependencyInjection;
using ORT.Vet.BusinessLogic;
using ORT.Vet.Domain;
using ORT.Vet.IBusinessLogic;
using ORT.Vet.IDataAccess;
using ORT.Vet.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace ORT.Vet.ServicesFactory;

public class ServicesFactory
{
    public ServicesFactory() { }

    public static void RegisterServices(IServiceCollection serviceCollection){
        serviceCollection.AddTransient<IBusinessLogic<Pet>, PetLogic>();
    }
    public static void RegisterDataAccess(IServiceCollection serviceCollection){
        serviceCollection.AddDbContext<DbContext, VetContext>();
        serviceCollection.AddScoped<IGenericRepository<Pet>, PetRepository>();
    }
}
