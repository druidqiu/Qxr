using Qxr.Application.Services.Dto;
using Qxr.AutoMapper;

namespace Qxr.Services.ServiceModels
{
    [AutoMap(typeof(Qxr.Models.Entities.User))]
    public class User: IEntityDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
