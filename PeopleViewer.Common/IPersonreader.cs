using System.Collections.Generic;

namespace PeopleViewer.Common
{
    public interface IPersonreader
    {
        IEnumerable<Person> GetPeople();
        Person GetPerson(int id);
    }
}
