using IocExample.Classes;
using Ninject;
using System;
using IocExample;
using System.Collections.Generic;


namespace IocExample
{
    class Program
    {
        //static void Main(string[] args)
        //{
        //    var logger = new ConsoleLogger();
        //    var sqlConnectionFactory = new SqlConnectionFactory("SQL Connection", logger);

        //    var createUserHandler = new CreateUserHandler(new UserService(new QueryExecutor(sqlConnectionFactory), new CommandExecutor(sqlConnectionFactory), new CacheService(logger, new RestClient("API KEY"))), logger);

        //    createUserHandler.Handle();
        //    Console.ReadKey();
        //}

        //static void Main(string[] args)
        //{
        //    IKernel kernel = new StandardKernel();

        //    kernel.Bind<ILogger>().To<ConsoleLogger>().InSingletonScope();
        //    kernel.Bind<IConnectionFactory>()
        //        .ToConstructor(k => new SqlConnectionFactory("SQL Connection", k.Inject<ILogger>()))
        //        .InSingletonScope();


        //    kernel.Bind<UserService>().To<UserService>();
        //    kernel.Bind<QueryExecutor>().To<QueryExecutor>();
        //    kernel.Bind<CommandExecutor>().To<CommandExecutor>();
        //    kernel.Bind<CacheService>().To<CacheService>();
        //    kernel.Bind<RestClient>().ToConstructor(k => new RestClient("API KEY"));

        //    kernel.Bind<CreateUserHandler>().To<CreateUserHandler>();
        //    var createUserHandler = kernel.Get<CreateUserHandler>();

        //    createUserHandler.Handle();

        //    Console.ReadKey();

        //}


        static void Main(string[] args)
        {
            var kernel = new MyKernel();

            kernel.Bind<ILogger, ConsoleLogger>(new ConsoleLogger());
            kernel.Bind<IConnectionFactory, SqlConnectionFactory>((SqlConnectionFactory)kernel.ToConstructor<SqlConnectionFactory>(
                new List<object>(){ "SQL Connection", kernel.Inject<ILogger>() }));

            kernel.Bind<UserService, UserService>();
            kernel.Bind<QueryExecutor, QueryExecutor>();
            kernel.Bind<CommandExecutor, CommandExecutor>();
            kernel.Bind<CacheService, CacheService>();

            kernel.Bind<RestClient, RestClient>((RestClient)kernel.ToConstructor<RestClient>(new List<object>() { "API KEY" }));

            kernel.Bind<CreateUserHandler, CreateUserHandler>();
            var createUserHandler = kernel.Get<CreateUserHandler>();

            createUserHandler.Handle();
            Console.ReadKey();
        }

    }
}