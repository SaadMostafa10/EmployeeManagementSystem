using AutoMapper;
using Microsoft.Identity.Client;
using PrimeTech.EMS.BLL.DataTransferObjects.DepartmentDTOs;
using PrimeTech.EMS.DAL.Models.DepartmentModel;
using PrimeTech.EMS.PL.Models.Department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeTech.EMS.BLL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Department Module
            // Controller
            CreateMap<DepartmentViewModel, CreatedDepartmentDTO>()
                    /*.ForMember(dest => dest.Name,conf=>conf.MapFrom(src=>src.Name))*/;
            CreateMap<DepartmentDetailsToReturnDTO, DepartmentViewModel>();
            CreateMap<DepartmentViewModel, UpdatedDepartmentDTO>();
            // Service
            CreateMap<Department, DepartmentToReturnDTO>();
            CreateMap<Department, DepartmentDetailsToReturnDTO>();
            CreateMap<CreatedDepartmentDTO, Department>();
            CreateMap<UpdatedDepartmentDTO, Department>();
            #endregion

            #region Employee Module

            #endregion
        }
    }
}
