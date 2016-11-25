using Qxr.Application.Services.Dto;
using Qxr.AutoMapper;
using Qxr.Models.Entities;

namespace Qxr.Services.ServiceModels
{
    [AutoMap(typeof(User))]
    public class UserModel: IEntityDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
