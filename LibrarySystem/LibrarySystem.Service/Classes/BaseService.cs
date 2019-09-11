using LibrarySystem.Dal;

namespace LibrarySystem.Service.Classes
{
    public class BaseService //Entity inctance yapıcı method kullandım. Ve service tarafında partial class olarak miras aldım
    {
        public LibraryModel db; // entitymodel

        public BaseService()
        {
            db = new LibraryModel();            
        }
    }
}
