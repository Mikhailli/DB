using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFDataAccessLayer;
using DapperDataAccessLayer;
using Model;
using Ninject.Modules;

namespace BLogic
{
    public class SimpleConfigModule : NinjectModule
    {
        //public UnitOfWork smth = new UnitOfWork();

        public override void Load()
        {

            //Bind<IRepository<Employee>>().To<DapperRepository<Employee>>().InSingletonScope();
            //Bind<IRepository<City>>().To<DapperRepository<City>>().InSingletonScope();

            Bind<IRepository<Employee>>().To<EntityRepository<Employee>>().InSingletonScope();
            Bind<IRepository<City>>().To<EntityRepository<City>>().InSingletonScope();


        }

    }
}
