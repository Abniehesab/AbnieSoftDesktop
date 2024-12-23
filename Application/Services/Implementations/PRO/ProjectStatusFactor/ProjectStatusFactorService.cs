using Application.DTOS.PRO.ProjectStatusFactor;
using Application.Services.Interfaces.PRO.IProjectStatusFactor;
using Common.Utilities.Paging;
using Domain.Entities.ACC;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFCore = Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions;

namespace Application.Services.Implementations.PRO.ProjectStatusFactor
{
    public class ProjectStatusFactorService : IProjectStatusFactorService
    {
        #region constructor
        private IGenericRepository<Domain.Entities.PRO.ProjectStatusFactor> _ProjectStatusFactorRepository;
        private IGenericRepository<Domain.Entities.ACC.Moein> _MoeinRepository;
        private IGenericRepository<Domain.Entities.ACC.Kol> _KolRepository;

        public ProjectStatusFactorService(IGenericRepository<Domain.Entities.PRO.ProjectStatusFactor> ProjectStatusFactorRepository, IGenericRepository<Domain.Entities.ACC.Moein> MoeinRepository, IGenericRepository<Kol> kolRepository)
        {
            _ProjectStatusFactorRepository = ProjectStatusFactorRepository;
            _MoeinRepository = MoeinRepository;
            _KolRepository = kolRepository;
        }

        #endregion
     

        public async Task<List<Domain.Entities.PRO.ProjectStatusFactor>> GetProjectStatusFactorByBusinessId(long BusinessId)
        {
            return await EFCore.ToListAsync(_ProjectStatusFactorRepository.GetEntitiesQuery()
             .Where(projectStatusFactor => projectStatusFactor.BusinessId == BusinessId && !projectStatusFactor.IsDelete));
        }


    

        public async Task<FilterProjectStatusFactorDTO> FilterProjectStatusFactor(FilterProjectStatusFactorDTO filter)
        {
            var projectStatusFactorQuery = _ProjectStatusFactorRepository.GetEntitiesQuery()
                .Where(projectStatusFactor => projectStatusFactor.IsDelete == false && projectStatusFactor.BusinessId == filter.BusinessId)
                .Join(
                            _MoeinRepository.GetEntitiesQuery(),
                            projectStatusFactor => projectStatusFactor.MoeinId,
                            moein => moein.Id,
                            (projectStatusFactor, moein) => new { ProjectStatusFactor = projectStatusFactor, Moein = moein }
                )
                 .Join(
                            _KolRepository.GetEntitiesQuery(),
                            moein => moein.Moein.KolId,
                            kol => kol.Id,
                            (moein, kol) => new { Moein = moein, Kol = kol }
                )
                .Select(projectStatusFactorQuery => new ProjectStatusFactorDTO
                {
                    Id = projectStatusFactorQuery.Moein.ProjectStatusFactor.Id,
                    BusinessId = filter.BusinessId,
                    ProjectStatusFactorCode=projectStatusFactorQuery.Moein.ProjectStatusFactor.ProjectStatusFactorCode,
                    ProjectStatusFactorTitle=projectStatusFactorQuery.Moein.ProjectStatusFactor.ProjectStatusFactorTitle,
                    ProjectStatusFactorType=projectStatusFactorQuery.Moein.ProjectStatusFactor.ProjectStatusFactorType,
                    ProjectStatusFactorUser=projectStatusFactorQuery.Moein.ProjectStatusFactor.ProjectStatusFactorUser,
                    ProjectStatusFactorPercent=projectStatusFactorQuery.Moein.ProjectStatusFactor.ProjectStatusFactorPercent,
                    MoeinId=projectStatusFactorQuery.Moein.Moein.Id,
                    MoeinTitle=projectStatusFactorQuery.Moein.Moein.MoeinName,
                    AccMoeinCode=projectStatusFactorQuery.Moein.Moein.AccMoeinCode,
                    KolId=projectStatusFactorQuery.Kol.Id,
                    KolTitle=projectStatusFactorQuery.Kol.KolName,
                    AccKolCode=projectStatusFactorQuery.Kol.AccKolCode
                }).AsQueryable();

            switch (filter.OrderBy)
            {
                case ProjectStatusFactorOrderBy.CodeAsc:
                    projectStatusFactorQuery = projectStatusFactorQuery.OrderBy(s => s.ProjectStatusFactorCode);
                    break;
                case ProjectStatusFactorOrderBy.CodeDec:
                    projectStatusFactorQuery = projectStatusFactorQuery.OrderByDescending(s => s.ProjectStatusFactorCode);
                    break;
            }
            if (!string.IsNullOrEmpty(filter.Title))
                projectStatusFactorQuery = projectStatusFactorQuery.Where(s => s.ProjectStatusFactorTitle.Contains(filter.Title));

            if (filter.ProjectStatusFactorCode > 0 && filter.ProjectStatusFactorCode != null)

                projectStatusFactorQuery = projectStatusFactorQuery.Where(s => s.ProjectStatusFactorCode == filter.ProjectStatusFactorCode);

            if (filter.ProjectStatusFactorUser > 0 && filter.ProjectStatusFactorUser != null)

                projectStatusFactorQuery = projectStatusFactorQuery.Where(s => s.ProjectStatusFactorUser == filter.ProjectStatusFactorUser);

            if (filter.ProjectStatusFactorType > 0 && filter.ProjectStatusFactorType != null)

                projectStatusFactorQuery = projectStatusFactorQuery.Where(s => s.ProjectStatusFactorType == filter.ProjectStatusFactorType);

            if (filter.MoeinId > 0 && filter.MoeinId != null)

                projectStatusFactorQuery = projectStatusFactorQuery.Where(s => s.MoeinId == filter.MoeinId);



            var count = (int)Math.Ceiling(projectStatusFactorQuery.Count() / (double)filter.TakeEntity);

            var pager = Pager.Build(count, filter.PageId, filter.TakeEntity);

            var projectStatusFactor = await EFCore.ToListAsync(projectStatusFactorQuery.Paging(pager));

            return filter.SetProjectStatusFactor(projectStatusFactor).SetPaging(pager);

        }



        public async Task<Domain.Entities.PRO.ProjectStatusFactor> GetProjectStatusFactors(long ProjectStatusFactorId)
        {
            return await _ProjectStatusFactorRepository.GetEntitiesQuery().AsQueryable().
             SingleOrDefaultAsync(projectStatusFactor => !projectStatusFactor.IsDelete && projectStatusFactor.Id == ProjectStatusFactorId);
        }

        public async Task<int?> GetTopProjectStatusFactorByBusinessId(long BusinessId)
        {
            var ProjectStatusFactors = await GetProjectStatusFactorByBusinessId(BusinessId);
            var ProjectStatusFactor = ProjectStatusFactors.OrderByDescending(ProjectStatusFactor => ProjectStatusFactor.ProjectStatusFactorCode).FirstOrDefault();
            if (ProjectStatusFactor != null)
            {
                return ProjectStatusFactor.ProjectStatusFactorCode + 1;
            }
            return 1;
        }
    }
}
