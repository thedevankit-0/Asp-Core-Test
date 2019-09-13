using Asp_Core_Test.Models;
using MvcFirstApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Asp_Core_Test.Repository
{
    public class PhdSubjectRepository : IPhdSubjectRepository
    {
        //Variable Declaration
        private readonly AppDBContext context;

        #region "PhdSubjectRepository() constructor"
        //PhdSubjectRepository() constructor
        public PhdSubjectRepository(AppDBContext context) => this.context = context;
        #endregion

        #region "GetSubjectList() lists all subjects"
        //GetSubjectList() lists all subjects from Subject Table
        public IEnumerable<PhdSubject> GetSubjectList() => context.PhdSubject.ToList();
        #endregion

        #region "GetSubject() returns the Subject Deatils"
        //GetSubject() returns the Subject Deatils
        public PhdSubject GetSubject(Guid? Id) => context.PhdSubject.SingleOrDefault(e => e.Id == Id);
        #endregion

        #region "SaveSubject() Add/Edit Subject"
        // SaveSubject() Add/Edit Subject into Subject table
        public PhdSubject SaveSubject(PhdSubject phdSubject)
        {
            if (phdSubject != null)
            {
                if (phdSubject.Id == Guid.Empty)
                    context.PhdSubject.Add(phdSubject);
                else
                {
                    PhdSubject savePhdSubject = context.PhdSubject.FirstOrDefault(e => e.Id == phdSubject.Id);
                    savePhdSubject.Name = phdSubject.Name;
                    savePhdSubject.Description = phdSubject.Description;
                }
                context.SaveChanges();
            }
            return phdSubject;
        }
        #endregion

        #region "DeleteSubject() method deletes subject"
        //DeleteSubject() method deletes subject from subject table
        public PhdSubject DeleteSubject(Guid Id)
        {
            PhdSubject phdSubject = context.PhdSubject.Find(Id);
            if (phdSubject != null)
            {
                context.PhdSubject.Remove(phdSubject);
                context.SaveChanges();
            }
            return phdSubject;
        }
        #endregion
    }
}
