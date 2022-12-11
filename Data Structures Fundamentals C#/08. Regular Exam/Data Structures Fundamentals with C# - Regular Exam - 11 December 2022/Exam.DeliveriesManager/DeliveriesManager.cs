using System;
using System.Collections.Generic;
using System.Linq;

namespace Exam.DeliveriesManager
{
    public class DeliveriesManager : IDeliveriesManager
    {
        private Dictionary<string, Deliverer> deliverers = new Dictionary<string, Deliverer>();

        private Dictionary<string, Package> packages = new Dictionary<string, Package>();

        public void AddDeliverer(Deliverer deliverer)
        {
            if (!Contains(deliverer))
            {
                deliverers.Add(deliverer.Id, deliverer);
            }
        }

        public void AddPackage(Package package)
        {
            if (!Contains(package))
            {
                packages.Add(package.Id, package);
            }
        }

        public void AssignPackage(Deliverer deliverer, Package package)
        {
            if(!Contains(deliverer) || !Contains(package))
            {
                throw new ArgumentException();
            }

            package.Deliverer = deliverer;
            deliverer.Packages.Add(package);
        }

        public bool Contains(Deliverer deliverer)
        {
            return deliverers.ContainsKey(deliverer.Id);
        }

        public bool Contains(Package package)
        {
            return packages.ContainsKey(package.Id);
        }

        public IEnumerable<Deliverer> GetDeliverers()
        => deliverers.Values;

        public IEnumerable<Deliverer> GetDeliverersOrderedByCountOfPackagesThenByName()
        => deliverers.Values.OrderByDescending(x => x.Packages.Count).ThenBy(x => x.Name);

        public IEnumerable<Package> GetPackages()
        => packages.Values;

        public IEnumerable<Package> GetPackagesOrderedByWeightThenByReceiver()
        => packages.Values.OrderByDescending(x => x.Weight).ThenBy(x => x.Receiver);

        public IEnumerable<Package> GetUnassignedPackages()
         => packages.Values.Where(x => x.Deliverer == null);
    }
}
