using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ViewModels.Base.ImageServer
{
    public class ImageRemoveVM<TId,TEntityId> where TId :  IEquatable<TId> where TEntityId : IEquatable<TEntityId>
    {
        public TId Id { get; set; }
        public TEntityId EntityId { get; set; }
        public string Path { get; set; }
    }
}
