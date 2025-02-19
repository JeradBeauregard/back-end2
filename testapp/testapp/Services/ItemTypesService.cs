using testapp.Data;
using testapp.Interfaces;

namespace testapp.Services
{
	public class ItemTypesService : IItemTypesService
	{
		//database and interface context

		private readonly ApplicationDbContext _context;
	

		public ItemTypesService(ApplicationDbContext context)
		{
			_context = context;
			
		}



		


	}
}
