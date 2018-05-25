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
            MyKernel.Bind<ILogger, ConsoleLogger>(new ConsoleLogger());
            MyKernel.Bind<IConnectionFactory, SqlConnectionFactory>((SqlConnectionFactory)MyKernel.Get<SqlConnectionFactory>(new List<object>() { "SQL Connection", MyKernel.Inject<ILogger>() }));

            MyKernel.Bind<UserService, UserService>();
            MyKernel.Bind<QueryExecutor, QueryExecutor>();
            MyKernel.Bind<CommandExecutor, CommandExecutor>();
            MyKernel.Bind<CacheService, CacheService>();

            MyKernel.Bind<RestClient, RestClient>((RestClient)MyKernel.Get<RestClient>(new List<object>() { "API KEY" }));

            MyKernel.Bind<CreateUserHandler, CreateUserHandler>();
            var createUserHandler = MyKernel.Resolve<CreateUserHandler>();

            createUserHandler.Handle();
            Console.ReadKey();
        }

    }
}