using Core.Application.Pipellines.Transaction;
using Core.Application.Pipellines.Validation;
using Core.Application.Rules;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Application
{
    public static class ApplicationSeerviceRegistration
    {
        public static IServiceCollection ApplicationExtensionsIoC(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

                configuration.AddOpenBehavior(typeof(RequestValidationBehavior<,>));
                configuration.AddBehavior(typeof(TransactionalScopeBehavior<,>));
            });
               

                

            
            


            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddSubClassesOfType(Assembly.GetExecutingAssembly(), typeof(BaseBusinessRules));

            return services;
        }

        

        public static IServiceCollection AddSubClassesOfType(this IServiceCollection services,
            Assembly assembly, Type type, Func<IServiceCollection, Type, IServiceCollection>? addwithcycle = null)
        {
            var types = assembly.GetTypes().Where(t => t.IsSubclassOf(type) && type != t).ToList();

            foreach (var item in types) 
            {
                if (addwithcycle == null)
                    services.AddScoped(item);
                else
                    addwithcycle(services, type);
                return services;    
            }
        }
    }
}
