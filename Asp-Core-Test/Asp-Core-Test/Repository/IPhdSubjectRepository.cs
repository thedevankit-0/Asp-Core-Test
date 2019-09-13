using System;
using System.Collections.Generic;

namespace Asp_Core_Test.Models
{
    #region "Subject interface."
    public interface IPhdSubjectRepository
    {
        //To list subjects.
        IEnumerable<PhdSubject> GetSubjectList();

        //To get specific subject by id.
        PhdSubject GetSubject(Guid? Id);

        //To delete specific subject by id.
        PhdSubject DeleteSubject(Guid Id);

        //To save subject deatils.
        PhdSubject SaveSubject(PhdSubject phdSubject);
    }
    #endregion
}