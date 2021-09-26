using PBC.Shared.Common;
using PBC.Shared.ListComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Server.Data.Repositories
{
    public class ListRepository : IListRepository
    {
        private readonly AbstractListFactory _listFactory;
        private readonly ApplicationDbContext _dbContext;
        private readonly IUserState _userState;

        public ListRepository(ApplicationDbContext context,IUserState userState, AbstractListFactory listFactory)
        {
            _dbContext = context;
            _userState = userState;
            _listFactory = listFactory;
        }
        public async Task CreateListAsync(IListDTO list)
        {
            if (list.ListDays == null) return;

            ListEntity listEntity = _listFactory.Make();
            var user = await _userState.CurrentUsernameAsync();

            listEntity.Days = list.Days;
            listEntity.CreatedBy = user;
            listEntity.CreatedOn = DateTime.Now;

            foreach(var listDayDTO in list.ListDays)
            {
                var listDay = _listFactory.MakeDay();
                listDay.Date = listDayDTO.Date;
                listDay.SequenceNumber = listDayDTO.SequenceNumber;
                listDay.BreakfastRecipeId = listDayDTO.Breakfast.RecipeId;
                listDay.LunchRecipeId = listDayDTO.Lunch.RecipeId;
                listDay.DinnerRecipeId = listDayDTO.Dinner.RecipeId;

                listEntity.ListDays.Add(listDay);
            }

            await _dbContext.Lists.AddAsync(listEntity);
            await _dbContext.SaveChangesAsync();
            return;
        }
    }
}
